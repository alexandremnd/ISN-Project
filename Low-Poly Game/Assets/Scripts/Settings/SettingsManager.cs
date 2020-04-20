using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    private static SettingsManager Instance;
    private Dictionary<string, dynamic> m_settings = new Dictionary<string, dynamic>();

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
}
