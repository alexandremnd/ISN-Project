using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FlexibleButton))]
public class ButtonMultipleChoice : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField, Tooltip("Ici, insérer l'ensemble des textes à afficher sur le bouton.")] 
    private string[] m_choice;
    [SerializeField] private string m_settingKey;

    private FlexibleButton m_button;
    private int m_choiceLength;

    #region Properties
    /// <summary>
    /// Donne l'accès au choix fait par l'utilisateur.
    /// </summary>
    public int ActualChoice { get; private set; } = 0;
    #endregion

    private void Start()
    {
        m_button = GetComponent<FlexibleButton>();

        if (m_choice != null)
        {
            m_choiceLength = m_choice.Length;
        }

        ActualChoice = (int)Settings.Instance.GetSettings(m_settingKey);

        m_button.SetText(m_choice[ActualChoice]);
    }

    public void NextChoice()
    {
        ActualChoice++;
        if (ActualChoice == m_choiceLength)
        {
            ActualChoice = 0;
        }
        m_button.SetText(m_choice[ActualChoice]);
        UpdateSetting();
    }

    public void PreviousChoice()
    {
        ActualChoice--;
        if (ActualChoice < 0)
        {
            ActualChoice = m_choiceLength - 1;
        }
        m_button.SetText(m_choice[ActualChoice]);
        UpdateSetting();
    }

    public void SetChoice(int index)
    {
        ActualChoice = index;
        m_button.SetText(m_choice[ActualChoice]);
    }

    private void UpdateSetting()
    {
        Settings.Instance.SetSettings<int>(m_settingKey, ActualChoice);
    }
}
