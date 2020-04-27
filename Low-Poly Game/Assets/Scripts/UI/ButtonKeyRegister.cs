using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FlexibleButton))]
public class ButtonKeyRegister : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private KeyCode m_defaultKeyCode;
    [SerializeField, Tooltip("Entrez la clé que l'on doit utiliser pour intéragir avec le script Keybinds.cs")]
    private string m_associatedKey;

    private FlexibleButton m_button;
    public KeyCode RegisteredKey { get; private set; } = KeyCode.None;

    private void Start()
    {
        m_button = GetComponent<FlexibleButton>();
        RegisteredKey = Settings.Instance.GetKey(m_associatedKey);
        if (RegisteredKey == KeyCode.None)
        {
            Settings.Instance.SetKey(m_associatedKey, m_defaultKeyCode);
        }
        m_button.SetText(RegisteredKey.ToString());
    }

    public void SetKey(KeyCode keyCode)
    {
        RegisteredKey = keyCode;
        m_button.SetText(RegisteredKey.ToString());
    }

    private void Update()
    {
        if (m_button.IsActive)
        {
            foreach (KeyCode vkey in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(vkey))
                {
                    RegisteredKey = vkey;
                    Settings.Instance.SetKey(m_associatedKey, RegisteredKey);
                    m_button.IsActive = false;
                    m_button.SetText(RegisteredKey.ToString());
                }
            }
        }
    }
}
