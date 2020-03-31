using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class TextHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Color settings")]
    [SerializeField] private Color m_textOnIdle;
    [SerializeField] private Color m_textOnClick;
    [SerializeField] private Color m_textOnHover;

    [ReadOnly] private TextMeshProUGUI m_child;

    public void OnPointerEnter(PointerEventData eventData)
    {
        m_child.color = m_textOnHover;
        if (Input.GetMouseButton(0))
        {
            m_child.color = m_textOnHover;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            m_child.color = m_textOnClick;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        m_child.color = m_textOnIdle;
    }

    private void Awake()
    {
        m_child = GetComponentInChildren<TextMeshProUGUI>();
        m_child.color = m_textOnIdle;
    }
}
