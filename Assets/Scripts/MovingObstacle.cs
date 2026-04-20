using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [Header("Movement")]
    public float minSpeed = 0.5f;
    public float maxSpeed = 1.5f;
    private float currentSpeed;

    [Header("Z Movement")]
    public float resetZ = 5f;
    public float startZ = -5f;

    [Header("X Range (based on plane size)")]
    public float minX = -2.2f;
    public float maxX = 2.2f;

    [Header("Side Movement")]
    public float sideAmplitude = 1f;   // how far it moves left/right
    public float sideFrequency = 2f;   // how fast it oscillates

    private float baseX;
    private float timeOffset;

    void Start()
    {
        ResetObstacle();
    }

    void Update()
    {
        float dt = Mathf.Min(Time.deltaTime, 0.004f);
        transform.Translate(Vector3.back * currentSpeed * dt);

        // Side-to-side movement (zig-zag)
        float offsetX = Mathf.Sin(Time.time * sideFrequency + timeOffset) * sideAmplitude;
        float newX = Mathf.Clamp(baseX + offsetX, minX, maxX);

        // Clamp so it never leaves the plane
        newX = Mathf.Clamp(newX, minX, maxX);

        transform.localPosition = new Vector3(
            newX,
            transform.localPosition.y,
            transform.localPosition.z
        );

        // Reset when passed player
        if (transform.localPosition.z < startZ)
        {
            ResetObstacle();
        }
    }

    void ResetObstacle()
    {
        // Random X spawn within bounds
        baseX = Random.Range(minX, maxX);

        // Random speed
        currentSpeed = Random.Range(minSpeed, maxSpeed);

        // Random phase for different zig-zag motion
        timeOffset = Random.Range(0f, 10f);

        // Reset position
        transform.localPosition = new Vector3(
            baseX,
            transform.localPosition.y,
            resetZ
        );
    }
}