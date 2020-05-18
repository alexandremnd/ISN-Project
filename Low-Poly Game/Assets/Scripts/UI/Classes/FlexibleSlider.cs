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

    private void Start()
    {
        m_sliderComponent = this.GetComponent<Slider>();

        if (m_shouldUseAssociatedSettingKey)
        {
            m_sliderComponent.value = (float)Settings.Instance.GetSettings(m_settingKey);
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
