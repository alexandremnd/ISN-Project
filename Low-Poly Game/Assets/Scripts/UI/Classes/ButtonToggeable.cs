using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(FlexibleButton))]
public class ButtonToggeable : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private string m_textOnIdle;
    [SerializeField] private string m_textOnActive;

    [Space]
    [SerializeField] private bool m_useAssociatedSettingKey;
    [SerializeField] private string m_settingKey;

    private FlexibleButton m_button;

    private bool m_actualState;

    private void Start()
    {
        m_button = GetComponent<FlexibleButton>();
        m_actualState = Settings.Instance.GetSettings(m_settingKey);

        m_button.IsActive = m_actualState;
    }

    /// <summary>
    /// On règle le texte du bouton selon l'état du bouton
    /// Si l'on change la valeur du bouton, on enregistre la nouvelle valeur dans les paramètres.
    /// </summary>
    private void Update()
    {
        if (m_button.IsActive)
        {
            m_button.SetText(m_textOnActive);
        }
        else
        {
            m_button.SetText(m_textOnIdle);
        }

        if (m_button.IsActive != m_actualState)
        {
            m_actualState = m_button.IsActive;
            Settings.Instance.SetSettings<bool>(m_settingKey, m_actualState);
        }
    }
}
