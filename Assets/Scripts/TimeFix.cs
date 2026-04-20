using UnityEngine;

public class TimeFix : MonoBehaviour
{
    void Start()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
    }
}