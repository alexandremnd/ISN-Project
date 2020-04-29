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

    private void Start()
    {
        m_keyToWatch = Settings.Instance.GetKey(m_keybindKey);

        if (m_keyToWatch == KeyCode.None)
        {
            Settings.Instance.SetKey(m_keybindKey, m_defaultKey);
            m_keyToWatch = m_defaultKey;
        }
    }

    private void Update()
    {
        int index = SceneManager.GetActiveScene().buildIndex;

        // Index 0 = Main menu
        if (Input.GetKeyUp(m_keyToWatch) && index != 0)
        {
            Toggle();
        }
    }

    public void Toggle()
    {
        m_objetToToggle.SetActive(!m_objetToToggle.activeSelf);
    }

    public void OpenMenu()
    {
        m_objetToToggle.SetActive(true);
    }

    public void CloseMenu()
    {
        m_objetToToggle.SetActive(false);
    }
}
