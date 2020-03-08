using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudsSpawner : MonoBehaviour
{
    [Header("Resources")]
    [SerializeField] private WeatherPreset m_weatherResource;

    [Header("Clouds Settings")]
    [SerializeField] private Transform m_cloudLayer;
    [SerializeField, Tooltip("The distance is calculated from cloud layer origin")] private float m_maxSpawnDistance;

    private void Start()
    {
        for (int x = 0; x < 256; x++)
        {
            for (int y = 0; y < 256; y++)
            {
                float perlin = Mathf.PerlinNoise(x / 2000 * 20, y / 2000 * 20);
            }
        }
    }
}
