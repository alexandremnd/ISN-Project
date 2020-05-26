using System.Collections.Generic;
using UnityEngine;

public class Benchmark : MonoBehaviour
{
    private List<float> m_framePerSecond = new List<float>();

    float deltaTime;
    float fps;

    /// <summary>
    /// Ici, on ajoute à la liste "m_framePerSecond" le nombre d'images par seconde moyenne.
    /// </summary>
    private void Update()
    {
        deltaTime = Time.deltaTime;
        deltaTime /= 2;
        fps = 1 / deltaTime;

        m_framePerSecond.Add(fps);
    }

    /// <summary>
    /// On effectue la moyenne du nombre d'images par seconde sur le temps d'exécution et on l'affiche dans la console.
    /// </summary>
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
