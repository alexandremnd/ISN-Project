using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
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

    public static Settings Instance;

    [Header("Default settings and keybinds")]
    [SerializeField] private KeyCodeSetting[] m_defaultKeybind;
    [SerializeField] private BooleanSetting[] m_defaultSetting;
    [SerializeField] private FloatSetting[] m_defaultFloat;

    [Header("References")]
    [SerializeField] private Camera m_camera;

    private Dictionary<string, dynamic> m_settings;
    private Dictionary<string, KeyCode> m_keybinds;

    private float m_horizontalValue = 0;
    private float m_verticalValue = 0;

    // Ici, on vérifie que cette classe est instancié une seule fois dans la scène pour éviter des doublons.
    // Ensuite, on indique au moteur de ne jamais supprimer l'objet.
    // On charge les paramètres déjà existants, on charge la camera, et on applique les paramètres graphiques et audio.
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

    // Quand une nouvelle scène charge, on récupère la nouvelle caméra principale.
    private void OnLevelWasLoaded(int level)
    {
        LoadCamera();
    }

    // On charge la caméra principale.
    // Par convention dans notre projet, la caméra principale représente la caméra du joueur.
    // Les autres caméras seront donc des caméras annexes non référencés.
    private void LoadCamera()
    {
        m_camera = Camera.main;
    }

    // Les paramètres du joueurs sont appliqués au moteur (qualité des textures, rafraichissement par seconde de l'écran ...)
    public void ApplySettings()
    {
        SaveSettings();

        // Paramètres graphiques
        QualitySettings.vSyncCount = m_settings["enableVsync"] ? 1 : 0;
        QualitySettings.SetQualityLevel((int)m_settings["renderQuality"]);

        int resolutionX;
        int resolutionY;
        switch (m_settings["screenResolution"])
        {
            case 0:
                resolutionX = 1280;
                resolutionY = 720;
                break;
            default:
                resolutionX = 1920;
                resolutionY = 1080;
                break;
        }

        FullScreenMode fullScreenMode;
        switch (m_settings["windowType"])
        {
            case 0:
                fullScreenMode = FullScreenMode.Windowed;
                break;
            case 1:
                fullScreenMode = FullScreenMode.FullScreenWindow;
                break;
            default:
                fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                break;
        }

        int frameRate;
        switch (m_settings["frameRate"])
        {
            case 0:
                frameRate = 60;
                break;
            case 1:
                frameRate = 75;
                break;
            default:
                frameRate = 144;
                break;
        }

        Screen.SetResolution(resolutionX, resolutionY, fullScreenMode, frameRate);

        if (m_camera != null)
        {
            m_camera.farClipPlane = (float)m_settings["renderDistance"];
            m_camera.fieldOfView = (float)m_settings["fieldOfView"];
        }
            

        // Paramètres audio
        AudioListener.volume = (float)m_settings["audioLevel"]/100;

    }

    // Sauvegarde les paramètres sous la forme d'un fichier JSON.
    private void SaveSettings()
    {
        string settingJson = JsonConvert.SerializeObject(m_settings);
        string keybindJson = JsonConvert.SerializeObject(m_keybinds);

        GameFile.Instance.WriteFile("keybinds.json", keybindJson);
        GameFile.Instance.WriteFile("settings.json", settingJson);
    }

    // Chargement des paramètres depuis un fichier JSON.
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

    // Quand l'application se ferme, cette fonction est appellée.
    private void OnApplicationQuit()
    {
        SaveSettings();
    }

    #region Get/Set|Methods
    // Permet au script externe d'enregistrer des paramètres dans la liste existante.
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

    // Permet au script externe de récupérer des paramètres dans la liste existante.
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

    // Permet au script externe d'enregistrer des raccourcis claviers dans la liste.
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

    // Permet au script externe de récupérer un raccourci enregistrer.
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

    // Permet de vérifier si la touche à la clé key est appuyé.
    public bool GetButton(string key)
    {
        return Input.GetKey(m_keybinds[key]);
    }

    // Permet de vérifier si la touche à la clé key est descendu.
    public bool GetButtonDown(string key)
    {
        return Input.GetKeyDown(m_keybinds[key]);
    }

    // Permet de vérifier si la touche à la clé key viens d'être relevée.
    public bool GetButtonUp(string key)
    {
        return Input.GetKeyUp(m_keybinds[key]);
    }

    // Permet d'obtenir la valeur d'un axe. 
    // Les touches Z et S permettent d'avancer et reculer, ces deux touches forment l'axe vertical.
    // Les touches Q et D permettent d'aller à gauche et à droite, ces deux touches forment l'axe horizontal.
    public float GetAxis(string axis)
    {
        if (GetButton("forward"))
        {
            m_verticalValue = 1f;
        }
        else if (GetButton("backward"))
        {
            m_verticalValue = -1f;
        }
        else
        {
            m_verticalValue = 0;
        }

        if (GetButton("right"))
        {
            m_horizontalValue = 1f;
        }
        else if (GetButton("left"))
        {
            m_horizontalValue = -1f;
        }
        else
        {
            m_horizontalValue = 0;
        }

        switch (axis)
        {
            case "Horizontal":
                return m_horizontalValue;
            case "Vertical":
                return m_verticalValue;
        }
        return 0f;
    }
    #endregion
}
