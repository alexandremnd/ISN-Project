using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycaster : MonoBehaviour
{
    [SerializeField] private float m_maxInteractionDistance = 2f;

    private Transform m_cameraTransform;
    private RaycastHit m_hit;
    private Interactive m_interactiveItem;

    private Transform m_holdedGameObject;

    /// <summary>
    /// On récupère une référence vers la caméra principale = caméra du joueur.
    /// </summary>
    void Start()
    {
        m_cameraTransform = Camera.main.transform;   
    }

    /// <summary>
    /// Si le joueur attaque, on envoie un rayon pour voir ce qu'il touche, si il touche un certain type d'objet
    /// on intéragis avec.
    /// </summary>
    void Update()
    {
        if (Settings.Instance.GetButtonDown("primaryAttack"))
        {
            switch (t_BasicInventory.Instance.GetItem().itemInternalName)
            {
                case "pickaxe":
                    bool raycastState = Physics.Raycast(m_cameraTransform.position, m_cameraTransform.forward, out m_hit, m_maxInteractionDistance);
                    if (raycastState && m_hit.transform.CompareTag("Interactive"))
                    {
                        m_interactiveItem = m_hit.transform.GetComponent<Interactive>();
                        if (m_interactiveItem != null)
                        {
                            m_interactiveItem.Interact(this.transform, InteractionType.Attack);
                        }
                    }
                    break;
            }
        }
    }
}
