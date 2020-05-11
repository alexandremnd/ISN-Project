using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [Header("Camera Settings")]
    [SerializeField] private float m_mouseSensitivity = 100f;
    [SerializeField] private float m_cameraClamping = 70f;

    [Header("References")]
    [SerializeField] private Transform m_camera;

    private float m_xRotation = 0f;

    // Start is called before the first frame update
    // On verrouile le curseur, on règle l'angle de la camera.
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        m_camera.localRotation = Quaternion.Euler(m_xRotation, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        // Les conventions sont bien respectés, parfait ;)
        float mouseX = Input.GetAxis("Mouse X") * m_mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * m_mouseSensitivity * Time.deltaTime;

        m_xRotation -= mouseY;
        m_xRotation = Mathf.Clamp(m_xRotation, -m_cameraClamping, m_cameraClamping);

        // Le script contrôlant le joueur se trouve sur le joueur.
        // Inutile de définir un Transform pour accéder au joueur vu que l'on est le joueur :)
        // On fait donc une rotation sur le joueur.
        this.transform.Rotate(new Vector3(0, mouseX));
        
        m_camera.localRotation = Quaternion.Euler(m_xRotation, 0f, 0f);
    }
}
