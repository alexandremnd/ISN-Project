using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    [SerializeField] private float walkSpeed = 12f;
    [SerializeField] private float sprintSpeed = 20f;
    [SerializeField] private float flySpeed = 5f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private bool noclipActive = true;

    public Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    float iWalkSpeed;

    // Start is called before the first frame update
    void Start()
    {
        iWalkSpeed = walkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        walkSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : iWalkSpeed;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (noclipActive)
        {
            velocity.y = 0;
            float mouseY = Input.GetAxis("Vertical")* Mathf.Sin((Camera.main.transform.eulerAngles.x * Mathf.PI) / -180);
            Vector3 move = transform.right * x + transform.forward * z + transform.up * mouseY;
            controller.Move(move * flySpeed * walkSpeed * Time.deltaTime);
            Physics.IgnoreLayerCollision(0, 9, true);
        }
        else
        {
            Vector3 move = transform.right * x + transform.forward * z;
            controller.Move(move * walkSpeed * Time.deltaTime);
            Physics.IgnoreLayerCollision(0, 9, false);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        if (!noclipActive)
        {
            velocity.y += gravity * Time.deltaTime;
        }

        controller.Move(velocity * Time.deltaTime);
    }

    void NoclipToggle()
    {
        noclipActive = !noclipActive;
    }
}