using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlexibleMenu : MonoBehaviour
{
    private FlexibleButton m_activeButton = null;

    // Informe le bouton actuellement actif qu'il doit ne plus être actif, et que le nouveau bouton doit devenir actif
    public void ToggleActive(FlexibleButton button)
    {
        if (m_activeButton != null)
        {
            m_activeButton.IsActive = false;
        }
        m_activeButton = button;
        m_activeButton.IsActive = true;
    }

    /// <summary>
    /// Permet de fermer le jeu.
    /// </summary>
    public void CloseGame()
    {
        Application.Quit();
    }
}
