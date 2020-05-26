using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToggleMenu : MonoBehaviour
{
    [Header("Toggle Settings")]
    [SerializeField] private KeyCode m_defaultKey;
    [SerializeField] private string m_keybindKey;

    [Header("References")]
    [SerializeField] private GameObject m_objetToToggle;

    private KeyCode m_keyToWatch;

    /// <summary>
    /// On récupère la touche pour ouvrir et fermer le menu dans les paramètres
    /// Si aucune touche existe, on assigne une touche par défaut.
    /// </summary>
    private void Start()
    {
        m_keyToWatch = Settings.Instance.GetKey(m_keybindKey);

        if (m_keyToWatch == KeyCode.None)
        {
            Settings.Instance.SetKey(m_keybindKey, m_defaultKey);
            m_keyToWatch = m_defaultKey;
        }
    }

    /// <summary>
    /// Selon la scène dans laquelle on se trouve, on permet au joueur le fait d'ouvrir ou non le menu.
    /// </summary>
    private void Update()
    {
        int index = SceneManager.GetActiveScene().buildIndex;

        // Index 0 = Main menu
        if (Input.GetKeyUp(m_keyToWatch) && index != 0)
        {
            Toggle();
        }
    }

    /// <summary>
    /// Inverse l'état du menu
    /// Si le menu est ouvert, on le ferme, et inversement
    /// </summary>
    public void Toggle()
    {
        m_objetToToggle.SetActive(!m_objetToToggle.activeSelf);
    }

    /// <summary>
    /// Ouvre le menu
    /// </summary>
    public void OpenMenu()
    {
        m_objetToToggle.SetActive(true);
    }

    /// <summary>
    /// Ferme le menu
    /// </summary>
    public void CloseMenu()
    {
        m_objetToToggle.SetActive(false);
    }
}
