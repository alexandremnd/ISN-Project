using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ShaderData : MonoBehaviour

{
    [Header("Settings")]
    [SerializeField] private Transform m_lightTransform;
    [SerializeField] private Light m_lightComponent;

    // Update is called once per frame
    void Update()
    {
        Shader.SetGlobalVector("_LightDir", m_lightTransform.forward);
        Shader.SetGlobalVector("_LightColor", m_lightComponent.color);
        Shader.SetGlobalFloat("_LightIntensity", m_lightComponent.intensity);
    }

    private void OnApplicationQuit()
    {
        Shader.SetGlobalVector("_LightDir", m_lightTransform.forward);
        Shader.SetGlobalVector("_LightColor", m_lightComponent.color);
        Shader.SetGlobalFloat("_LightIntensity", m_lightComponent.intensity);
    }
}
