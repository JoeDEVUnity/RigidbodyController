using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Vector2 moveInput;

    public Vector2 mouseInput;

    public float setVelocity;   
    
    PlayerControls playerControls;

    Rigidbody rb;

    public Camera cam;

    public Transform groundCheck;
    public LayerMask groundLayer;

    public float checkRadius;
    public float jumpVelocity;
    public bool isGrounded;

    public bool canJump;
    public bool jumpTimer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.transform.position, checkRadius, groundLayer);

        if (canJump)
        {
            Jump();
        }

    }

    private void FixedUpdate()
    {
        HandleMovement();
    }
    



    // CUSTOM FUNCTIONS
    void HandleMovement()
    {
        Vector3 movement = transform.forward * moveInput.y + transform.right * moveInput.x;
        movement.y = 0;
        

        rb.AddForce(movement * setVelocity, ForceMode.Acceleration);

    }

    void Jump()
    {

    }



    private void OnEnable()
    {
        if(playerControls == null)
        {
            playerControls = new PlayerControls();

            playerControls.Controls.Mouse.performed += i => mouseInput = i.ReadValue<Vector2>();
            playerControls.Controls.Movement.performed += i => moveInput = i.ReadValue<Vector2>();

            playerControls.Enable();
        }
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }


}
