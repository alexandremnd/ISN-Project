﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button), typeof(Image)), ExecuteInEditMode]
public class FlexibleButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [Header("Button Settings")]
    [SerializeField] private bool m_useButtonNameForText = false;
    [SerializeField] private bool m_isPartOfAButtonGroup = false;
    [SerializeField] private bool m_isActiveByDefault = false;
    [SerializeField] private bool m_shouldOpenPanelOnActive = false;

    [Header("Button Style Settings")]
    [SerializeField] private Sprite m_sourceImage;
    [SerializeField] private Color m_colorOnIdle = new Color(0, 0, 0, 255);
    [SerializeField] private Color m_colorOnHover = new Color(0, 0, 0, 255);
    [SerializeField] private Color m_colorOnClick = new Color(0, 0, 0, 255);
    [SerializeField] private Color m_colorOnActive = new Color(0, 0, 0, 255);
    [SerializeField, Range(0f, 1f)] private float m_fadeDuration = 0f; 
    [SerializeField, Tooltip("Quand le bouton est actif, les couleurs de survol/clic ne sont pas appliqués quand la variable est sur \"True\"")] 
    private bool m_constantColorOnActive = false;

    [Header("Button Text Style Settings")]
    [SerializeField] private TMP_FontAsset m_font;
    [SerializeField] private float m_fontSize = 35;
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
    [SerializeField, ReadOnly] private bool m_isActive = false;
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
            if (m_isActive)
            {
                m_button.colors = m_colorWhenActive;
                m_text.color = m_textColorOnActive;
                if (m_shouldOpenPanelOnActive && PanelInitialized) m_panelToOpen.SetActive(true);
            }
            else
            {
                m_button.colors = m_colorWhenIdle;
                m_text.color = m_textColorOnIdle;
                if (m_shouldOpenPanelOnActive && PanelInitialized) m_panelToOpen.SetActive(false);
            }
        }
    }

    public bool TextInitialized
    {
        get {
            return m_text != null ? true : false;
        }
    }

    public bool PanelInitialized
    {
        get
        {
            return m_panelToOpen != null ? true : false;
        }
    }
    #endregion

    public void Awake()
    {
        // On récupère des references vers des objets.
        this.GetComponent<RectTransform>().pivot = new Vector2(0, 0);

        m_text = this.GetComponentInChildren<TextMeshProUGUI>();
        m_image = this.GetComponent<Image>();
        m_button = this.GetComponent<Button>();

        // On règle l'image du bouton
        m_image.sprite = m_sourceImage;

        // Si on veut que le texte du bouton ai le même nom que le nom du bouton, on le fait.
        if (m_useButtonNameForText && TextInitialized)
        {
            m_text.SetText(this.name);
        }

        // Si le bouton doit être actif par défaut, on active le bouton, sinon, non
        if (m_isPartOfAButtonGroup && m_isActiveByDefault)
        {
            m_menu.ToggleActive(this);
            IsActive = true;
        }
        else
        {
            IsActive = m_isActiveByDefault;
        }

        // On crée une sorte de palette de couleur pour Unity.
        m_colorWhenIdle.normalColor = m_colorOnIdle;
        m_colorWhenIdle.highlightedColor = m_colorOnHover;
        m_colorWhenIdle.pressedColor = m_colorOnClick;
        m_colorWhenIdle.selectedColor = m_colorOnActive;
        m_colorWhenIdle.colorMultiplier = 1;
        m_colorWhenIdle.fadeDuration = m_fadeDuration;

        if (m_constantColorOnActive)
        {
            m_colorWhenActive.normalColor = m_colorOnActive;
            m_colorWhenActive.highlightedColor = m_colorOnActive;
            m_colorWhenActive.pressedColor = m_colorOnActive;
            m_colorWhenActive.selectedColor = m_colorOnActive;
            m_colorWhenActive.colorMultiplier = 1;
            m_colorWhenActive.fadeDuration = m_fadeDuration;
        }
        else
        {
            m_colorWhenActive.normalColor = m_colorOnActive;
            m_colorWhenActive.highlightedColor = m_colorOnHover;
            m_colorWhenActive.pressedColor = m_colorOnClick;
            m_colorWhenActive.selectedColor = m_colorOnIdle;
            m_colorWhenActive.colorMultiplier = 1;
            m_colorWhenActive.fadeDuration = m_fadeDuration;
        }

        // On règle la couleur du bouton, du texte, la police d'écriture, la taille de la police.
        if (IsActive)
        {
            m_button.colors = m_colorWhenActive;
            m_text.color = m_textColorOnActive;
        }
        else
        {
            m_button.colors = m_colorWhenIdle;
            m_text.color = m_textColorOnIdle;
        }

        m_text.font = m_font;
        m_text.fontSize = m_fontSize;
    }

    /// <summary>
    /// Quand on change une valeur dans l'éditeur Unity, on appelle Awake.
    /// </summary>
    private void OnValidate()
    {
        Awake();
    }

    /// <summary>
    /// Applique le texte spécifié en argument au bouton.
    /// </summary>
    /// <param name="text">Texte que l'on souhaite appliquer au bouton</param>
    public void SetText(string text)
    {
        if (TextInitialized)
        {
            m_text.SetText(text);
        }
    }

    #region Events|Pointer
    /// <summary>
    /// Quand la souris survole le bouton, on met la couleur associé au survol du bouton
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (m_constantColorOnActive && IsActive) return;
        m_text.color = m_textColorOnHover;
    }

    /// <summary>
    /// Quand la souris ne survole pas le bouton, on remet la couleur d'origine du bouton (sauf si le bouton est actif).
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        if (m_constantColorOnActive && IsActive) return;
        if (IsActive)
        {
            m_text.color = m_textColorOnActive;
            return;
        }
        m_text.color = m_textColorOnIdle;
    }

    /// <summary>
    /// Quand on appuye sur le bouton, on change la couleur du bouton
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        if (m_constantColorOnActive && IsActive) return;
        m_text.color = m_textColorOnClick;
    }

    /// <summary>
    /// Quand on relève le clic gauche, on inverse l'état du bouton (On/Off)
    /// </summary>
    /// <param name="eventData"></param>
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
    #endregion
}