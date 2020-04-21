using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class SettingsManager : MonoBehaviour
{
    [HideInInspector] public static SettingsManager Instance;
    public Dictionary<string, dynamic> Settings
    {
        get
        {
            return m_settings;
        }
    }

    private Dictionary<string, dynamic> m_settings = new Dictionary<string, dynamic>();
    private string m_documentGamePath;

    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }

        m_documentGamePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
        m_documentGamePath += "\\My Games\\";
        if (!Directory.Exists(m_documentGamePath))
        {
            Directory.CreateDirectory(m_documentGamePath);
        }
        m_documentGamePath += Application.productName;
        if (!Directory.Exists(m_documentGamePath))
        {
            Directory.CreateDirectory(m_documentGamePath);
        }

        if (File.Exists(m_documentGamePath + "\\settings.json"))
        {
            string json = File.ReadAllText(m_documentGamePath + "\\settings.json");
            m_settings = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(json);
        }
        else
        {
            SetSettings<float>("fov", Mathf.RoundToInt(80));
            SetSettings<bool>("compass", true);
            SetSettings<bool>("hud", true);
            SetSettings<float>("draw_distance", Mathf.RoundToInt(800));
            SetSettings<bool>("volumetric_fog", true);
            SetSettings<bool>("ambient_occlusion", true);
            SetSettings<bool>("screen_space_reflection", true);
            SetSettings<bool>("anti_aliasing", true);
            SetSettings<bool>("depth_of_field", false);
            SetSettings<bool>("motion_blur", true);
            SetSettings<bool>("vignette", true);
        }
    }

    public void SetSettings<T>(string key, T value)
    {
        try
        {
            m_settings[key] = value;
        }
        catch
        {
            m_settings.Add(key, value);
        }
    }

    public void SetFOV(float value)
    {
        SetSettings<float>("fov", Mathf.RoundToInt(value));
    }

    public void SetCompass(FlexibleButton button)
    {
        SetSettings<bool>("compass", button.IsActive);
    }

    public void SetHUD(FlexibleButton button)
    {
        SetSettings<bool>("hud", button.IsActive);
    }

    public void SetDrawDistance(float value)
    {
        SetSettings<float>("draw_distance", Mathf.RoundToInt(value));
    }

    public void SetVolumetricFog(FlexibleButton button)
    {
        SetSettings<bool>("volumetric_fog", button.IsActive);
    }
    public void SetAmbientOcclusion(FlexibleButton button)
    {
        SetSettings<bool>("ambient_occlusion", button.IsActive);
    }

    public void SetSSR(FlexibleButton button)
    {
        SetSettings<bool>("screen_space_reflection", button.IsActive);
    }

    public void SetAA(FlexibleButton button)
    {
        SetSettings<bool>("anti_aliasing", button.IsActive);
    }

    public void SetDepthOfField(FlexibleButton button)
    {
        SetSettings<bool>("depth_of_field", button.IsActive);
    }

    public void SetMotionBlur(FlexibleButton button)
    {
        SetSettings<bool>("motion_blur", button.IsActive);
    }

    public void SetVignette(FlexibleButton button)
    {
        SetSettings<bool>("vignette", button.IsActive);
    }

    private void OnApplicationQuit()
    {
        string json = JsonConvert.SerializeObject(m_settings);
        File.WriteAllText(m_documentGamePath + "\\settings.json", json);
    }
}
