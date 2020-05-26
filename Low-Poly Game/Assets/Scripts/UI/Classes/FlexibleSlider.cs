using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class FlexibleSlider : MonoBehaviour
{
    [SerializeField] private bool m_shouldShowSliderValue = true;
    [SerializeField] private TextMeshProUGUI m_textComponent;

    [SerializeField] private bool m_shouldUseAssociatedSettingKey;
    [SerializeField] private string m_settingKey;

    private Slider m_sliderComponent;

    /// <summary>
    /// On récupère le composant "Slider", et si le slider est associé à un paramètre, on récupère la valeur dans
    /// les paramètres.
    /// </summary>
    private void Start()
    {
        m_sliderComponent = this.GetComponent<Slider>();

        if (m_shouldUseAssociatedSettingKey)
        {
            m_sliderComponent.value = (float)Settings.Instance.GetSettings(m_settingKey);
        }
    }

    /// <summary>
    /// A chaque image, on affiche la valeur du slider sur le texte associé.
    /// </summary>
    void Update()
    {
        if (m_textComponent != null)
        {
            m_textComponent.SetText(Mathf.RoundToInt(m_sliderComponent.value).ToString());
        }
    }

    /// <summary>
    /// Enregistre la valeur via la classe Settings dans un fichier
    /// La fonction est appelée à chaque changement de valeur du Slider.
    /// </summary>
    /// <param name="value">Valeur du slider</param>
    public void UpdateSetting(float value)
    {
        Settings.Instance.SetSettings<float>(m_settingKey, value);
    }
}
