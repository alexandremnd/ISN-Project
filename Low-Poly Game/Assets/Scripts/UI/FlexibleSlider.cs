using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Rendering.HighDefinition;
public class FlexibleSlider : MonoBehaviour
{
    [SerializeField] private bool m_shouldShowSliderValue = true;
    [SerializeField] private TextMeshProUGUI m_textComponent;
    private Slider m_sliderComponent;

    private void Awake()
    {
        m_sliderComponent = this.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_textComponent != null)
        {
            m_textComponent.SetText(Mathf.RoundToInt(m_sliderComponent.value).ToString());
        }
    }
}
