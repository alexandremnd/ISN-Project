using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class SettingsManager : MonoBehaviour
{
    [Header("Object references")]
    [SerializeField] private Camera m_camera;
    [SerializeField] private HDAdditionalCameraData m_moreCamera;
    [SerializeField] private Volume m_skyFogVolume;
    [SerializeField] private Volume m_postProcessVolume;

    [HideInInspector] public static SettingsManager Instance;

    private Dictionary<string, dynamic> m_settings = new Dictionary<string, dynamic>();
    private string m_documentGamePath;

    [SerializeField] private DepthOfField m_dof;
    [SerializeField] private Fog m_fog;
    [SerializeField] private AmbientOcclusion m_ao;
    [SerializeField] private ScreenSpaceReflection m_ssr;
    [SerializeField] private Vignette m_vignette;
    [SerializeField] private MotionBlur m_motionBlur;

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

        LoadVolume();

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
            ReloadGraphics();
        }
        else
        {
            SetSettings<double>("fov", 80);
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
            SetSettings<bool>("window_type", true);
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

    public dynamic GetSettings(string key)
    {
        try
        {
            return m_settings[key];
        }
        catch
        {
            return null;
        }
    }

    public void SetFOV(double value)
    {
        SetSettings<double>("fov", value);
        m_camera.fieldOfView = m_settings["fov"];
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
        m_camera.farClipPlane = m_settings["draw_distance"];
    }

    public void SetVolumetricFog(FlexibleButton button)
    {
        SetSettings<bool>("volumetric_fog", button.IsActive);
        m_fog.active = m_settings["volumetric_fog"];
    }
    public void SetAmbientOcclusion(FlexibleButton button)
    {
        SetSettings<bool>("ambient_occlusion", button.IsActive);
        m_ao.active = m_settings["ambient_occlusion"];
    }

    public void SetSSR(FlexibleButton button)
    {
        SetSettings<bool>("screen_space_reflection", button.IsActive);
        m_ssr.active = m_settings["screen_space_reflection"];
    }

    public void SetAA(FlexibleButton button)
    {
        SetSettings<bool>("anti_aliasing", button.IsActive);
        if (m_settings["anti_aliasing"] == true)
        {
            m_moreCamera.antialiasing = HDAdditionalCameraData.AntialiasingMode.FastApproximateAntialiasing;
        }
        else
        {
            m_moreCamera.antialiasing = HDAdditionalCameraData.AntialiasingMode.None;
        }
    }

    public void SetDepthOfField(FlexibleButton button)
    {
        SetSettings<bool>("depth_of_field", button.IsActive);
        m_dof.active = m_settings["depth_of_field"];
    }

    public void SetMotionBlur(FlexibleButton button)
    {
        SetSettings<bool>("motion_blur", button.IsActive);
        m_motionBlur.active = m_settings["motion_blur"];
    }

    public void SetVignette(FlexibleButton button)
    {
        SetSettings<bool>("vignette", button.IsActive);
        m_vignette.active = m_settings["vignette"];
    }

    public void SetAudioLevel(float value)
    {
        SetSettings<float>("audio_level", value);
        AudioListener.volume = value;
    }

    public void SetWindowSettings(FlexibleButton button)
    {
        SetSettings<bool>("window_type", button.IsActive);
        if (m_settings["window_type"])
        {
            Screen.SetResolution(1920, 1080, true);
        }
        else
        {
            Screen.SetResolution(1280, 720, false);
        }
    }

    private void OnApplicationQuit()
    {
        string json = JsonConvert.SerializeObject(m_settings);
        File.WriteAllText(m_documentGamePath + "\\settings.json", json);
    }

    private void LoadVolume()
    {
        DepthOfField tmpDOF;
        if(m_postProcessVolume.profile.TryGet<DepthOfField>(out tmpDOF))
        {
            m_dof = tmpDOF;
        }

        Fog tmpFog;
        if(m_skyFogVolume.profile.TryGet<Fog>(out tmpFog))
        {
            m_fog = tmpFog;
        }

        AmbientOcclusion tmpAO;
        if (m_postProcessVolume.profile.TryGet<AmbientOcclusion>(out tmpAO))
        {
            m_ao = tmpAO;
        }

        ScreenSpaceReflection tmpSSR;
        if (m_postProcessVolume.profile.TryGet<ScreenSpaceReflection>(out tmpSSR))
        {
            m_ssr = tmpSSR;
        }

        Vignette tmpVignette;
        if (m_postProcessVolume.profile.TryGet<Vignette>(out tmpVignette))
        {
            m_vignette = tmpVignette;
        }

        MotionBlur tmpMotionBlur;
        if (m_postProcessVolume.profile.TryGet<MotionBlur>(out tmpMotionBlur))
        {
            m_motionBlur = tmpMotionBlur;
        }
    }

    private void ReloadGraphics()
    {
        m_camera.fieldOfView = (int)Mathf.RoundToInt(m_settings["fov"]);
        m_camera.farClipPlane = m_settings["draw_distance"];

        if (m_settings["anti_aliasing"] == true)
        {
            m_moreCamera.antialiasing = HDAdditionalCameraData.AntialiasingMode.FastApproximateAntialiasing;
        }
        else
        {
            m_moreCamera.antialiasing = HDAdditionalCameraData.AntialiasingMode.None;
        }

        m_dof.active = m_settings["depth_of_field"];
        m_fog.enableVolumetricFog = m_settings["volumetric_fog"];
        m_ao.active = m_settings["ambient_occlusion"];
        m_ssr.active = m_settings["screen_space_reflection"];
        m_vignette.active = m_settings["vignette"];
        m_motionBlur.active = m_settings["motion_blur"];

        if (m_settings["window_type"])
        {
            Screen.SetResolution(1920, 1080, true);
        }
        else
        {
            Screen.SetResolution(1280, 720, false);
        }

        AudioListener.volume = m_settings["audio_level"];
    }
}
