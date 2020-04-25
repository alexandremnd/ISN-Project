using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlexibleMenu : MonoBehaviour
{
    private FlexibleButton m_activeButton = null;

    public void ToggleActive(FlexibleButton button)
    {
        if (m_activeButton != null)
        {
            m_activeButton.IsActive = false;
        }
        m_activeButton = button;
        m_activeButton.IsActive = true;
    }
}
