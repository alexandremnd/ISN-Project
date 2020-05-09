using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private CharacterController m_controller;

    [Header("Movement settings")]
    [SerializeField] private float m_walkSpeed = 12f;
    [SerializeField] private float m_sprintSpeed = 20f;
    [SerializeField] private float m_flySpeed = 5f;
    [SerializeField] private float m_gravity = -9.81f;
    [SerializeField] private float m_jumpHeight = 3f;

    [Header("Read Only Variables")]
    [SerializeField, ReadOnly] private bool m_noclipActive = true;

    private Transform m_groundCheck;
    private LayerMask m_groundMask;
    [SerializeField] private float m_groundDistance = 0.4f;

    Vector3 m_velocity;
    bool m_isGrounded;

    float m_iWalkSpeed;

    // Start is called before the first frame update
    void Start()
    {
        m_iWalkSpeed = m_walkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        m_isGrounded = Physics.CheckSphere(m_groundCheck.position, m_groundDistance, m_groundMask);

        if (m_isGrounded && m_velocity.y < 0)
        {
            m_velocity.y = -2f;
        }

        m_walkSpeed = Input.GetKey(KeyCode.LeftShift) ? m_sprintSpeed : m_iWalkSpeed;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (m_noclipActive)
        {
            m_velocity.y = 0;
            float mouseY = Input.GetAxis("Vertical")* Mathf.Sin((Camera.main.transform.eulerAngles.x * Mathf.PI) / -180);
            Vector3 move = transform.right * x + transform.forward * z + transform.up * mouseY;
            m_controller.Move(move * m_flySpeed * m_walkSpeed * Time.deltaTime);
            Physics.IgnoreLayerCollision(0, 9, true);
        }
        else
        {
            Vector3 move = transform.right * x + transform.forward * z;
            m_controller.Move(move * m_walkSpeed * Time.deltaTime);
            Physics.IgnoreLayerCollision(0, 9, false);
        }

        if (Input.GetButtonDown("Jump") && m_isGrounded)
        {
            m_velocity.y = Mathf.Sqrt(m_jumpHeight * -2 * m_gravity);
        }

        if (!m_noclipActive)
        {
            m_velocity.y += m_gravity * Time.deltaTime;
        }

        m_controller.Move(m_velocity * Time.deltaTime);
    }

    void NoclipToggle()
    {
        m_noclipActive = !m_noclipActive;
    }
}