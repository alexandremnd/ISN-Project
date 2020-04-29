using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.SceneManagement;

[System.Serializable]
public class KeyCodeSetting
{
    public string settingKey;
    public KeyCode key;
}

[System.Serializable]
public class BooleanSetting
{
    public string settingKey;
    public bool state;
}

[System.Serializable]
public class FloatSetting
{
    public string settingKey;
    public float value;
}


public class Settings : MonoBehaviour
{
    public static Settings Instance;

    [Header("Default settings and keybinds")]
    [SerializeField] private KeyCodeSetting[] m_defaultKeybind;
    [SerializeField] private BooleanSetting[] m_defaultSetting;
    [SerializeField] private FloatSetting[] m_defaultFloat;

    [Header("References")]
    [SerializeField] private Camera m_camera;

    private Dictionary<string, dynamic> m_settings;
    private Dictionary<string, KeyCode> m_keybinds;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(this);
        LoadSettings();
        LoadCamera();
        ApplySettings();
    }

    private void OnLevelWasLoaded(int level)
    {
        LoadCamera();
    }

    private void LoadCamera()
    {
        m_camera = Camera.main;
    }

    public void ApplySettings()
    {
        SaveSettings();

        // Paramètres graphiques
        QualitySettings.vSyncCount = m_settings["enableVsync"] ? 1 : 0;
        QualitySettings.SetQualityLevel((int)m_settings["renderQuality"]);
        
        if (m_camera != null)
        {
            m_camera.farClipPlane = (float)m_settings["renderDistance"];
            m_camera.fieldOfView = (float)m_settings["fieldOfView"];
        }
            

        // Paramètres audio
        AudioListener.volume = (float)m_settings["audioLevel"]/100;

    }

    private void SaveSettings()
    {
        string settingJson = JsonConvert.SerializeObject(m_settings);
        string keybindJson = JsonConvert.SerializeObject(m_keybinds);

        GameFile.Instance.WriteFile("keybinds.json", keybindJson);
        GameFile.Instance.WriteFile("settings.json", settingJson);
    }

    private void LoadSettings()
    {
        string settingJson = GameFile.Instance.ReadFile("settings.json");
        string keybindJson = GameFile.Instance.ReadFile("keybinds.json");

        m_settings = new Dictionary<string, dynamic>();
        foreach (var item in m_defaultSetting)
        {
            m_settings.Add(item.settingKey, item.state);
        }
        foreach (var item in m_defaultFloat)
        {
            m_settings.Add(item.settingKey, item.value);
        }

        m_keybinds = new Dictionary<string, KeyCode>();
        foreach (var item in m_defaultKeybind)
        {
            m_keybinds.Add(item.settingKey, item.key);
        }

        if (settingJson != null)
        {
            var tempSettings = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(settingJson);
            foreach (var item in m_settings)
            {
                if (!tempSettings.ContainsKey(item.Key))
                {
                    tempSettings.Add(item.Key, item.Value);
                }
            }
            m_settings = tempSettings;
        }

        if (keybindJson != null)
        {
            var tempKeybinds = JsonConvert.DeserializeObject<Dictionary<string, KeyCode>>(keybindJson);
            foreach (var item in m_keybinds)
            {
                if (!tempKeybinds.ContainsKey(item.Key))
                {
                    tempKeybinds.Add(item.Key, item.Value);
                }
            }
            m_keybinds = tempKeybinds;
        }
    }

    private void OnApplicationQuit()
    {
        SaveSettings();
    }

    #region Get/Set|Methods
    public void SetSettings<T>(string key, T value)
    {
        if (m_settings.ContainsKey(key))
        {
            m_settings[key] = value;
        }
        else
        {
            m_settings.Add(key, value);
        }
        ApplySettings();
    }

    public dynamic GetSettings(string key)
    {
        if (m_settings.ContainsKey(key))
        {
            return m_settings[key];
        }
        else
        {
            return null;
        }
    }

    public void SetKey(string key, KeyCode keyCode)
    {
        if (m_keybinds.ContainsKey(key))
        {
            m_keybinds[key] = keyCode;
        }
        else
        {
            m_keybinds.Add(key, keyCode);
        }
    }

    public KeyCode GetKey(string key)
    {
        try
        {
            return m_keybinds[key];
        }
        catch
        {
            return KeyCode.None;
        }
    }
    #endregion
}
