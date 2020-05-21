using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{

    [Header("General settings")]
    [SerializeField] private LayerMask groundMask;

    [Header("Movement settings")]
    [SerializeField] private float m_walkSpeed = 12f;
    [SerializeField] private float m_sprintSpeed = 20f;
    [SerializeField] private float m_gravity = -9.81f;
    [SerializeField] private float m_jumpHeight = 3f;
    [SerializeField] private float m_groundCheckDistanceError = 0.3f;

    [Header("No clip settings")]
    [SerializeField] private float m_flySpeed = 5f;
    [SerializeField] private float m_flySpeedYreducer = 2f;

    [Header("Read Only Variables")]
    [SerializeField, ReadOnly] private bool m_noclipActive = true;

    private Transform m_cameraTransform;
    private CharacterController m_controller;

    private Vector3 m_velocity;
    private Vector3 m_jumpVelocity;
    private bool m_isGrounded;
    private float m_iWalkSpeed;

    // Start is called before the first frame update
    // On prend quelque références vers des objets dans la scène.
    void Start()
    {
        m_iWalkSpeed = m_walkSpeed;
        m_cameraTransform = Camera.main.transform;
        m_controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    // Ici, on transcrit les touches du clavier et mouvement de la souris en mouvement dans le jeu.
    // On traite le déplacement naturel (physique), et le déplacement sans physique (si le mode vol est activé).
    void Update()
    {
        // "m_controller.center + transform.position" = Position du centre du Character Controller en World Space.
        // "-transform.up", le vecteur qui se dirige vers le bas, mais dépendant de la rotation du controller
        // "m_controller.height * 0.5f + 0.1f", la distance max entre le sol et le centre, c'est la hauteur divisé par 2, et on rajoute 0.1 en marge d'erreur. 
        m_isGrounded = Physics.Raycast(m_controller.center + transform.position, -transform.up, m_controller.height * 0.5f + m_groundCheckDistanceError, groundMask);

        // Beau opérateur ternaire sah quel plaisir.
        m_velocity.y = (m_isGrounded && m_velocity.y < 0) ? -2f : m_velocity.y;

        m_walkSpeed = Settings.Instance.GetButton("sprint") ? m_sprintSpeed : m_iWalkSpeed;

        float x = Settings.Instance.GetAxis("Horizontal");
        float z = Settings.Instance.GetAxis("Vertical");
        float y = Settings.Instance.GetButton("jump") ? 1 : 0;

        if (m_noclipActive)
        {
            m_jumpVelocity = Vector3.zero;
            m_velocity.y = 0;

            // Tu avais remis Input.GetAxis("Vertical"), alors que tu avais enregistrer sa valeur dans la variable z :) 
            // Attention à ne pas call plusieurs fois des fonctions si tu les enregistres dans une variable.
            // Original : float mouseY = -Input.GetAxis("Vertical") * Mathf.Sin((m_cameraTransform.eulerAngles.x)*Mathf.Deg2Rad);
            float mouseY = -z * Mathf.Sin((m_cameraTransform.eulerAngles.x) * Mathf.Deg2Rad);
            Vector3 move = transform.right * x + transform.forward * z + transform.up * mouseY + transform.up * y * 1 / m_flySpeedYreducer;
            m_controller.Move(move * m_flySpeed * m_walkSpeed * Time.deltaTime);
        }
        else
        {
            // Ton code permettait le joueur de changer de direction en saut, ce qui n'est pas très réaliste :)
            // La solution était d'enregistrer la vélocité juste avant de quitter le sol, et de maintenir la même vélocité au cours du saut.
            Vector3 move;
            if (m_isGrounded)
            {
                move = transform.right * x + transform.forward * z;
                m_controller.Move(move * m_walkSpeed * Time.deltaTime);
            }
            else
            {
                move = m_jumpVelocity;
                m_controller.Move(move * Time.deltaTime);
            }
        }

        if (Settings.Instance.GetButton("jump") && m_isGrounded)
        {
            m_jumpVelocity = m_controller.velocity;
            m_velocity.y = Mathf.Sqrt(m_jumpHeight * -2 * m_gravity);
        }

        if (!m_noclipActive && !m_isGrounded)
        {
            m_velocity.y += m_gravity * Time.deltaTime;
        }

#if UNITY_EDITOR
        if(Input.GetKeyDown("v"))
        {
            NoclipToggle();
        }
#endif

        m_controller.Move(m_velocity * Time.deltaTime);
    }

    // On inverse l'état de la variable
    // Si A = false, alors !A = true
    public void NoclipToggle()
    {
        m_noclipActive = !m_noclipActive;
    }
}