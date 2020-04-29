using System.Collections.Generic;
using UnityEngine;

public class Benchmark : MonoBehaviour
{
    private List<float> m_framePerSecond = new List<float>();

    float deltaTime;
    float fps;
    private void Update()
    {
        deltaTime = Time.deltaTime;
        deltaTime /= 2;
        fps = 1 / deltaTime;

        m_framePerSecond.Add(fps);
    }

    private void OnApplicationQuit()
    {
        float averageFps = 0;
        foreach (float fps in m_framePerSecond)
        {
            averageFps += (float)fps;
        }
        averageFps /= m_framePerSecond.Count;

        UnityEngine.Debug.Log("Average FPS : " + averageFps);
    }
}
