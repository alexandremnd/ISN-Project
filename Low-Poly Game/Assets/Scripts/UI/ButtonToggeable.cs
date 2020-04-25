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

    private FlexibleButton m_button;

    private void Awake()
    {
        m_button = GetComponent<FlexibleButton>();
    }

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
    }
}
