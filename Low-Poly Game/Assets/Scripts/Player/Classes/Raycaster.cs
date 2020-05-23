using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycaster : MonoBehaviour
{
    [SerializeField] private float m_maxInteractionDistance = 2f;

    private Transform m_cameraTransform;
    private RaycastHit m_hit;
    private Interactive m_interactiveItem;

    // Start is called before the first frame update
    void Start()
    {
        m_cameraTransform = Camera.main.transform;   
    }

    // Update is called once per frame
    void Update()
    {
        if (Settings.Instance.GetButtonDown("primaryAttack"))
        {
            bool raycastState = Physics.Raycast(m_cameraTransform.position, m_cameraTransform.forward, out m_hit, m_maxInteractionDistance);
            if (raycastState && m_hit.transform.CompareTag("Interactive"))
            {
                m_interactiveItem = m_hit.transform.GetComponent<Interactive>();
                if (m_interactiveItem != null)
                {
                    m_interactiveItem.Interact(this.transform, InteractionType.Attack);
                }
            }
        }
    }
}
