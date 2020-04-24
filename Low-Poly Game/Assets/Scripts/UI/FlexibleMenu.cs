using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlexibleMenu : MonoBehaviour
{
    private ButtonTMP m_activeButton = null;

    public void ToggleActive(ButtonTMP button)
    {
        if (m_activeButton != null)
        {
            m_activeButton.IsActive = false;
        }
        m_activeButton = button;
        m_activeButton.IsActive = true;
    }
}
