using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudsSpawner : MonoBehaviour
{
    [Header("Resources")]
    [SerializeField] private CloudsData m_resources;

    [Header("Clouds Settings")]
    [SerializeField] private Transform m_cloudLayer01;
    [SerializeField] private Transform m_cloudLayer02;
    [SerializeField] private Transform m_cloudLayer03;
    [SerializeField] private float m_cloudLayer01Altitude;
    [SerializeField] private float m_cloudLayer02Altitude;
    [SerializeField] private float m_cloudLayer03Altitude;

    [Space]
    [SerializeField] private float m_cloudsPerLayer;
    [SerializeField, Tooltip("Distance is based on the cloud origin. (from origin to extremum)")] private float m_maxSpawnDistance;
    [SerializeField] private bool m_randomizeCloudScale;
    [SerializeField, Range(0.5f, 2f)] private float m_scaleMin;
    [SerializeField, Range(0.5f, 2f)] private float m_scaleMax;

    // Start is called before the first frame update
    void Start()
    {
        if (m_randomizeCloudScale)
        {
            if (m_scaleMin > m_scaleMax)
            {
                float tempScale = m_scaleMin;
                m_scaleMin = m_scaleMax;
                m_scaleMax = tempScale;
            }
            else if (m_scaleMin == m_scaleMax)
            {
                m_randomizeCloudScale = false;
            }
        }

        m_cloudLayer01.position = new Vector3(0, m_cloudLayer01Altitude, 0);
        m_cloudLayer02.position = new Vector3(0, m_cloudLayer02Altitude, 0);
        m_cloudLayer03.position = new Vector3(0, m_cloudLayer03Altitude, 0);

        SpawnClouds();
    }

    private void SpawnClouds()
    {
        for (int i = 0; i < m_cloudsPerLayer; i++)
        {
            SpawnCloud(m_cloudLayer01);
        }
        for (int i = 0; i < m_cloudsPerLayer; i++)
        {
            SpawnCloud(m_cloudLayer02);
        }
        for (int i = 0; i < m_cloudsPerLayer; i++)
        {
            SpawnCloud(m_cloudLayer03);
        }
    }

    private void SpawnCloud(Transform layer)
    {
        float x = Random.Range(-m_maxSpawnDistance, m_maxSpawnDistance);
        float z = Random.Range(-m_maxSpawnDistance, m_maxSpawnDistance);

        GameObject cloud = Instantiate(m_resources.clouds[Mathf.FloorToInt(Random.Range(0, m_resources.clouds.Length))], new Vector3(x, layer.position.y, z), layer.rotation);
        cloud.transform.SetParent(layer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
