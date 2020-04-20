using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

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
            m_settings = (Dictionary<string, dynamic>)JsonConvert.DeserializeObject(json);
        }
        else
        {

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

    private void OnApplicationQuit()
    {
        string json = JsonConvert.SerializeObject(m_settings);
        File.WriteAllText(m_documentGamePath + "\\settings.json", json);
    }
}
