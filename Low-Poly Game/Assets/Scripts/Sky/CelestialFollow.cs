using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.Math;

[ExecuteInEditMode]
public class CelestialFollow : MonoBehaviour
{
    [Header("Object References")]
    [SerializeField] private Transform m_sun;
    [SerializeField] private Transform m_moon;
    [SerializeField] private Transform m_sunLight;
    [SerializeField] private Transform m_moonLight;
    private Transform m_camera;

    [Header("Parameters")]
    [SerializeField] private float m_distanceBetweenCameraCelestial;

    private void Start()
    {
        m_camera = Camera.main.transform;
    }

    void Update()
    {
        m_sun.position = m_camera.position - (m_sunLight.forward * m_distanceBetweenCameraCelestial);
        m_moon.position = m_camera.position - (m_moonLight.forward * m_distanceBetweenCameraCelestial);
    }

    public bool MoonBellowHorizon
    {
        get
        {
            return CheckBellowHorizon(m_moonLight.forward, 5f);
        }
    }

    public bool SunBellowHorizon
    {
        get
        {
            return CheckBellowHorizon(m_sunLight.forward, 5f);
        }
    }

    public bool CheckBellowHorizon(Vector3 dir, float angleThreshold)
    {
        return dir.y < 0 ? false : (90 - Mathf.Acos(dir.y) * Mathf.Rad2Deg) > angleThreshold ? true : false;
    }
}
