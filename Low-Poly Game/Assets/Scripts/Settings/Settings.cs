using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public class Settings : MonoBehaviour
{
    public static Settings Instance;

    private Dictionary<string, dynamic> m_settings = new Dictionary<string, dynamic>();
    private Dictionary<string, KeyCode> m_keybinds = new Dictionary<string, KeyCode>();

    private void Awake()
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

        LoadSettings();
    }

    private void ApplySettings()
    {
        SaveSettings();
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

        m_settings = settingJson == null ? m_settings : JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(settingJson);
        m_keybinds = keybindJson == null ? m_keybinds : JsonConvert.DeserializeObject<Dictionary<string, KeyCode>>(keybindJson);
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
