using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FlexibleButton))]
public class ButtonKeyRegister : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField, Tooltip("Entrez la clé que l'on doit utiliser pour intéragir avec le script Keybinds.cs")]
    private string m_associatedKey;

    private FlexibleButton m_button;
    public KeyCode RegisteredKey { get; private set; } = KeyCode.None;

    private void Start()
    {
        m_button = GetComponent<FlexibleButton>();
        RegisteredKey = Settings.Instance.GetKey(m_associatedKey);
        m_button.SetText(RegisteredKey.ToString());
    }

    public void SetKey(KeyCode keyCode)
    {
        RegisteredKey = keyCode;
        m_button.SetText(RegisteredKey.ToString());
    }

    /// <summary>
    /// On parcoure l'ensemble des touches existantes d'un clavier et l'on vérifie si l'on appuye dessus.
    /// Si on appuye sur une touche, on enregistre la touche dans les paramètres.
    /// </summary>
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
