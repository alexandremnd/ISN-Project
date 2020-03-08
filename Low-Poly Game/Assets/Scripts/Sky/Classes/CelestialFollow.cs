using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using Utility.Math;

[ExecuteInEditMode]
public class CelestialFollow : MonoBehaviour
{
    [Header("Object References")]
    [SerializeField] private Transform m_sun;
    [SerializeField] private Transform m_moon;
    [SerializeField] private Transform m_sunLight;
    [SerializeField] private Transform m_moonLight;
    [SerializeField] private HDAdditionalLightData m_sunLightData;
    [SerializeField] private HDAdditionalLightData m_moonLightData;
    [SerializeField] private Transform m_camera;

    [Header("Parameters")]
    [SerializeField] private float m_distanceBetweenCameraCelestial;

    void FixedUpdate()
    {
        m_sun.position = m_camera.position - (m_sunLight.forward * m_distanceBetweenCameraCelestial);
        m_moon.position = m_camera.position - (m_moonLight.forward * m_distanceBetweenCameraCelestial);
        m_moon.LookAt(m_sunLight);

        // Le RP provoque des erreurs quand deux lumières volumétriques se rencontrent.
        if (SunBellowHorizon)
        {
            m_sunLightData.EnableShadows(false);
            m_sunLightData.affectsVolumetric = false;
            m_moonLightData.EnableShadows(true);
            m_moonLightData.affectsVolumetric = true;
        }
        else
        {
            m_sunLightData.EnableShadows(true);
            m_sunLightData.affectsVolumetric = true;
            m_moonLightData.EnableShadows(false);
            m_moonLightData.affectsVolumetric = false;
        }
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
