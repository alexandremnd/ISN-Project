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

    /// <summary>
    /// On met à jour la rotation du soleil/lune.
    /// La lune et le soleil étant strictement symétrique par rapport à l'origine du monde, si le soleil
    /// est sous l'horizon, la lune est au dessus l'horizon.
    /// Ainsi, si le soleil est sous l'horizon, on enlève des effets graphiques au soleil
    /// Inversement avec la lune.
    /// </summary>
    void Update()
    {
        UpdateRotation();
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

    /// <summary>
    /// Permet de maintenir la distance entre le joueur et les objets célestes constant.
    /// </summary>
    public void UpdateRotation()
    {
        m_sun.position = m_camera.position - (m_sunLight.forward * m_distanceBetweenCameraCelestial);
        m_moon.position = m_camera.position - (m_moonLight.forward * m_distanceBetweenCameraCelestial);
        m_moon.LookAt(m_sunLight);
    }

    /// <summary>
    /// Retourne vrai si la lune est sous l'horizon
    /// </summary>
    public bool MoonBellowHorizon
    {
        get
        {
            return CheckBellowHorizon(m_moonLight.forward, 5f);
        }
    }

    /// <summary>
    /// Retourne vrai si le soleil est sous l'horizon
    /// </summary>
    public bool SunBellowHorizon
    {
        get
        {
            return CheckBellowHorizon(m_sunLight.forward, 5f);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dir">Direction de l'axe z local de l'objet</param>
    /// <param name="angleThreshold">Seuil toléré avant de considérer que l'on se situe bien en dessous l'horizon.</param>
    /// <returns></returns>
    public bool CheckBellowHorizon(Vector3 dir, float angleThreshold)
    {
        return dir.y < 0 ? false : (90 - Mathf.Acos(dir.y) * Mathf.Rad2Deg) > angleThreshold ? true : false;
    }
}
