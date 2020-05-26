using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractionType
{
    Use,
    HoldUse,
    Attack,
}

// Cette classe permet de faire interface entre le joueur, et les scripts où le joueur peut intéragir.
public class Interactive : MonoBehaviour
{
    // Les trois variables permettent au script externes de "s'inscrire" dans une liste afin de recevoir les actions du joueur.
    public Action<Transform> m_actionOnUse;
    public Action<Transform> m_actionOnHoldUse;
    public Action<Transform> m_actionOnAttack;

    /// <summary>
    /// Ici, selon le type d'interaction, on appelle les fonctions qui se sont enregistrés à ce script pour recevoir 
    /// une "notification" quand le joueur intéragis avec l'objet où l'on est.
    /// </summary>
    /// <param name="playerTransform">Classe contenant la position, rotation, taille du joueur</param>
    /// <param name="type">Le type d'interaction (attaque, utilisation classique)</param>
    public void Interact(Transform playerTransform, InteractionType type)
    {
        Debug.Log(m_actionOnAttack);
        switch (type)
        {
            case InteractionType.Use:
                if (m_actionOnUse != null)
                    m_actionOnUse(playerTransform);
                break;
            case InteractionType.HoldUse:
                if (m_actionOnHoldUse != null)
                    m_actionOnHoldUse(playerTransform);
                break;
            case InteractionType.Attack:
                if (m_actionOnAttack != null)
                    m_actionOnAttack(playerTransform);
                break;
            default:
                Debug.LogError("<color=red>Attention !</color> Le type d'interaction est inconnue, rien ne sera executée.");
                break;
        }
    }
}
