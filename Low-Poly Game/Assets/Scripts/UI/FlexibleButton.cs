using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[ExecuteInEditMode]
public class FlexibleButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("General Settings")]
    [SerializeField] private bool m_isPartOfAButtonGroup = false;
    [SerializeField] private bool m_isActiveByDefault = false;
    [SerializeField] private FlexibleMenu m_menuComponent = null;
    [SerializeField] private bool m_shouldOpenPanel = false;
    [SerializeField] private GameObject m_panel;

    [Header("Button color settings")]
    [SerializeField] private Sprite m_sourceImage;
    [SerializeField] private Color m_colorOnIdle = new Color(0, 0, 0, 255);
    [SerializeField] private Color m_colorOnHover = new Color(0, 0, 0, 255);
    [SerializeField] private Color m_colorOnClick = new Color(0, 0, 0, 255);
    [SerializeField] private Color m_colorOnActive = new Color(0, 0, 0, 255);
    [SerializeField, Tooltip("Should we apply hover/click color even if the button is active")] 
    private bool m_constantColorWhenActive = true;
    [SerializeField, Range(0f, 1f)] private float m_fadeDuration;

    [Header("Button text color settings")]
    [SerializeField] private TMP_FontAsset m_font;
    [SerializeField] private int m_fontSize;
    [SerializeField] private Color m_textColorOnIdle = new Color(0, 0, 0, 255);
    [SerializeField] private Color m_textColorOnHover = new Color(0, 0, 0, 255);
    [SerializeField] private Color m_textColorOnClick = new Color(0, 0, 0, 255);
    [SerializeField] private Color m_textColorOnActive = new Color(0, 0, 0, 255);

    private Image m_imageComponent;
    private Button m_buttonComponent;
    private TextMeshProUGUI m_textComponent;

    private ColorBlock m_colorWhenActive;
    private ColorBlock m_colorWhenIdle;

    [SerializeField] private bool m_isActive;

    public bool IsActive
    {
        get
        {
            return m_isActive;
        }
        set
        {
            m_isActive = value;
            if (m_isActive)
            {
                m_buttonComponent.colors = m_colorWhenActive;
                m_textComponent.color = m_textColorOnActive;
            }
            else
            {
                m_buttonComponent.colors = m_colorWhenIdle;
                m_textComponent.color = m_textColorOnIdle;
            }
            if (m_shouldOpenPanel)
            {
                m_panel.SetActive(m_isActive);
            }
        }
    }

    private void Awake()
    {
        m_imageComponent = GetComponent<Image>();
        m_buttonComponent = GetComponent<Button>();
        m_textComponent = GetComponentInChildren<TextMeshProUGUI>();

        if (m_imageComponent == null)
        {
            m_imageComponent = gameObject.AddComponent(typeof(Image)) as Image;
        }
        if (m_buttonComponent == null)
        {
            m_buttonComponent = gameObject.AddComponent(typeof(Button)) as Button;
        }
        if (m_textComponent == null)
        {
            GameObject child = new GameObject("Text (TMP)");
            child.transform.parent = this.transform;
            child.transform.position = Vector3.zero;
            m_textComponent = child.AddComponent(typeof(TextMeshProUGUI)) as TextMeshProUGUI;
        }

        m_imageComponent.sprite = m_sourceImage;
        m_imageComponent.color = new Color(255,255,255,255);

        if (m_constantColorWhenActive)
        {
            m_colorWhenActive.normalColor = m_colorOnActive;
            m_colorWhenActive.pressedColor = m_colorOnActive;
            m_colorWhenActive.highlightedColor = m_colorOnActive;
            m_colorWhenActive.selectedColor = m_colorOnActive;
            m_colorWhenActive.colorMultiplier = 1;
            m_colorWhenActive.fadeDuration = m_fadeDuration;
        }
        else
        {
            m_colorWhenActive.normalColor = m_colorOnActive;
            m_colorWhenActive.pressedColor = m_colorOnClick;
            m_colorWhenActive.highlightedColor = m_colorOnHover;
            m_colorWhenActive.selectedColor = m_colorOnActive;
            m_colorWhenActive.colorMultiplier = 1;
            m_colorWhenActive.fadeDuration = m_fadeDuration;
        }

        m_colorWhenIdle.normalColor = m_colorOnIdle;
        m_colorWhenIdle.pressedColor = m_colorOnClick;
        m_colorWhenIdle.highlightedColor = m_colorOnHover;
        m_colorWhenIdle.selectedColor = m_colorOnActive;
        m_colorWhenIdle.colorMultiplier = 1;
        m_colorWhenIdle.fadeDuration = m_fadeDuration;

        m_buttonComponent.colors = m_colorWhenIdle;


        m_textComponent.color = m_textColorOnIdle;
        m_textComponent.font = m_font;
        m_textComponent.fontSize = m_fontSize;

        if (m_isPartOfAButtonGroup && m_isActiveByDefault)
        {
            m_menuComponent.SetActive(this);
        }
        else
        {
            IsActive = m_isActiveByDefault;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (IsActive) return;
        m_textComponent.color = m_textColorOnClick;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        m_textComponent.color = m_textColorOnIdle;
        if (m_isPartOfAButtonGroup)
        {
            m_menuComponent.SetActive(this);
        }
        else
        {
            IsActive = !IsActive;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (IsActive) return;
        m_textComponent.color = m_textColorOnHover;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (IsActive) return;
        m_textComponent.color = m_textColorOnIdle;
    }

    private void OnValidate()
    {
        Awake();
    }

    private void Update()
    {
        // Unity shit ...
    }
}
