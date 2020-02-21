using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class Keybinds : MonoBehaviour
{
    [ReadOnly] public static Keybinds Instance;
      
    private Dictionary<string, KeyCode> m_keybinds = new Dictionary<string, KeyCode>();
    private string m_documentGamePath;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this);
        }
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        string documentPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
        m_documentGamePath = documentPath + "\\My Games\\" + Application.productName;

        if (!Directory.Exists(documentPath + "\\My Games"))
        {
            Directory.CreateDirectory(documentPath + "\\My Games");
        }
        if (!Directory.Exists(documentPath + "\\My Games\\" + Application.productName))
        {
            Directory.CreateDirectory(documentPath + "\\My Games\\" + Application.productName);
        }

        m_keybinds.Add("Forward", KeyCode.Z);
        m_keybinds.Add("Backward", KeyCode.S);
        m_keybinds.Add("Left", KeyCode.Q);
        m_keybinds.Add("Right", KeyCode.D);
        m_keybinds.Add("Jump", KeyCode.Space);
        m_keybinds.Add("Crouch", KeyCode.X);
        m_keybinds.Add("Inventory", KeyCode.I);

        if (File.Exists(m_documentGamePath + "\\keybinds.json"))
        {
            string json = File.ReadAllText(m_documentGamePath + "\\keybinds.json");
            Dictionary<string, KeyCode> tmpDict = JsonConvert.DeserializeObject<Dictionary<string, KeyCode>>(json);

            foreach (var item in tmpDict)
            {
                if (m_keybinds.ContainsKey(item.Key))
                {
                    m_keybinds[item.Key] = item.Value;
                }
            }
        }  
    }

    private void OnApplicationQuit()
    {

        string json = JsonConvert.SerializeObject(m_keybinds);
        File.WriteAllText(m_documentGamePath + "\\keybinds.json", json);
    }

    public void SetKey(string key, KeyCode keyCode)
    {
        m_keybinds[key] = keyCode;
    }

    public KeyCode GetKey(string key)
    {
        return m_keybinds[key];
    }
}