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
    [SerializeField] private float m_defaultValue;

    private Slider m_sliderComponent;

    private void Awake()
    {
        m_sliderComponent = this.GetComponent<Slider>();

        if (m_shouldUseAssociatedSettingKey)
        {
            var value = Settings.Instance.GetSettings(m_settingKey);
            if (value == null)
            {
                Settings.Instance.SetSettings<int>(m_settingKey, (int)m_sliderComponent.value);
                m_sliderComponent.value = m_defaultValue;
            }
            else
            {
                m_sliderComponent.value = (int)value;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (m_textComponent != null)
        {
            m_textComponent.SetText(Mathf.RoundToInt(m_sliderComponent.value).ToString());
        }
    }

    public void UpdateSetting(float value)
    {
        Settings.Instance.SetSettings<float>(m_settingKey, value);
    }
}
