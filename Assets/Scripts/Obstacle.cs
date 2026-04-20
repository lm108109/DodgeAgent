using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float speed = 10f;  // Speed at which the obstacle moves toward the agent (adjust as needed)

    void Update()
    {
        // Move the obstacle backward (toward negative Z, assuming agent is at Z=-3.46)
        transform.Translate(Vector3.back * speed * Time.deltaTime);

        // Destroy if past the agent to prevent accumulation
        if (transform.position.z < -10f)
        {
            Destroy(gameObject);
        }
    }
}