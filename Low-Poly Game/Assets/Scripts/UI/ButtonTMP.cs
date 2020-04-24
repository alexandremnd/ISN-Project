using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ButtonTMP : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [Header("Button Settings")]
    [SerializeField] private bool m_useButtonNameForText = false;
    [SerializeField] private bool m_isPartOfAButtonGroup = false;
    [SerializeField] private bool m_isActiveByDefault = false;
    [SerializeField] private bool m_shouldOpenPanelOnActive = false;

    [Header("Button Style Settings")]
    [SerializeField] private Image m_sourceImage;
    [SerializeField] private Color m_colorOnIdle = new Color(0, 0, 0, 255);
    [SerializeField] private Color m_colorOnHover = new Color(0, 0, 0, 255);
    [SerializeField] private Color m_colorOnClick = new Color(0, 0, 0, 255);
    [SerializeField] private Color m_colorOnActive = new Color(0, 0, 0, 255);
    [SerializeField, Range(0f, 1f)] private float m_fadeDuration = 0f; 
    [SerializeField, Tooltip("Quand le bouton est actif, les couleurs de survol/clic ne sont pas appliqués quand la variable est sur \"True\"")] 
    private bool m_constantColorOnActive = false;

    [Header("Button Text Style Settings")]
    [SerializeField] private TMP_FontAsset m_font;
    [SerializeField] private int m_fontSize;
    [SerializeField] private Color m_textColorOnIdle = new Color(0, 0, 0, 255);
    [SerializeField] private Color m_textColorOnHover = new Color(0, 0, 0, 255);
    [SerializeField] private Color m_textColorOnClick = new Color(0, 0, 0, 255);
    [SerializeField] private Color m_textColorOnActive = new Color(0, 0, 0, 255);


    [Header("References")]
    [SerializeField] private FlexibleMenu m_menu;
    [SerializeField] private GameObject m_panelToOpen;

    #region Fields
    private TextMeshProUGUI m_text;
    private Image m_image;
    private Button m_button;

    private ColorBlock m_colorWhenIdle;
    private ColorBlock m_colorWhenActive;
    #endregion

    #region Properties|State
    public bool IsActive
    {
        get
        {
            return m_isActive;
        }
        set
        {
            m_isActive = value;
        }
    }

    public bool TextInitialized
    {
        get {
            return m_text != null ? true : false;
        }
    }
    #endregion

    private bool m_isActive = false;

    public void Awake()
    {
        m_text = this.GetComponent<TextMeshProUGUI>();
        m_image = this.GetComponent<Image>();
        m_button = this.GetComponent<Button>();

        
    }

    /// <summary>
    /// Apply the specified string on the button. 
    /// </summary>
    /// <param name="text">Text you want to apply to the button text.</param>
    public void SetText(string text)
    {
        if (TextInitialized)
        {
            m_text.SetText(text);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (m_constantColorOnActive && IsActive) return;
        m_text.color = m_textColorOnHover;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (m_constantColorOnActive && IsActive) return;
        m_text.color = m_textColorOnIdle;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (m_constantColorOnActive && IsActive) return;
        m_text.color = m_textColorOnClick;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (m_isPartOfAButtonGroup)
        {
            m_menu.ToggleActive(this);
        }
        else
        {
            IsActive = !IsActive;
        }
        
        if (IsActive)
        {
            m_text.color = m_textColorOnActive;
        }
        else
        {
            m_text.color = m_textColorOnIdle;
        }
    }
}
