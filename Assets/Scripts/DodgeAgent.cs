using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class DodgeAgent : Agent
{
    public float moveSpeed = 5f;
    public float maxX = 2.2f;
    public GameObject obstaclePrefab;  // Reference to the obstacle prefab
    public float spawnInterval = 3f;   // Time between spawning obstacles (adjust for difficulty)
    public float minObstacleSpeed = 3f;
    public float maxObstacleSpeed = 6f;

    public override void OnEpisodeBegin()
    {
        // Reset agent position (adjusted to match your scene setup)
        transform.localPosition = new Vector3(0f, 0.31f, -3.46f);

        // Clean up any existing obstacles from previous episodes
        GameObject[] existingObstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach (GameObject obs in existingObstacles)
        {
            Destroy(obs);
        }

        // Start spawning obstacles continuously
        CancelInvoke("SpawnObstacle");  // Cancel any previous spawning
        InvokeRepeating("SpawnObstacle", 0f, spawnInterval);

        Debug.Log("DodgeAgent: Episode begin");
    }

    private void SpawnObstacle()
    {
        // Random X position relative to the agent's position
        float randomX = Random.Range(-maxX, maxX);
        Vector3 spawnPos = transform.position + new Vector3(randomX, 0f, 15f);  // Offset Z to start ahead of agent (agent at Z=-3.46, so 20 units ahead)

        // Instantiate the prefab
        GameObject newObstacle = Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);

        // Randomize speed for variety, but keep it slower for training
        Obstacle obsScript = newObstacle.GetComponent<Obstacle>();
        if (obsScript != null)
        {
            obsScript.speed = Random.Range(minObstacleSpeed, maxObstacleSpeed);
        }
    }

    private void FixedUpdate()
    {
        RequestDecision();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition.x / maxX);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        Debug.Log($"DodgeAgent: Action received = {moveX}");

        float newX = transform.localPosition.x + moveX * moveSpeed * Time.deltaTime;

        newX = Mathf.Clamp(newX, -maxX, maxX);

        // Keep the agent at the same Z and correct Y height for the plane
        transform.localPosition = new Vector3(newX, 0.31f, transform.localPosition.z);

        // reward for surviving
        AddReward(0.01f);
    }

    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var a = actionsOut.ContinuousActions;
        a[0] = Input.GetAxis("Horizontal");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Debug.Log("DodgeAgent: Hit obstacle, ending episode");
            AddReward(-1f);
            EndEpisode();
        }
    }
}