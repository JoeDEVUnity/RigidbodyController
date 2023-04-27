using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Vector2 moveInput;

    public Vector2 mouseInput;

    public Vector3 slopeMovement;

    private Vector3 movement;

    public float dragForce = 6f;

    public float airDrag = 2f;

    public float setVelocity;
    public float sprintValue = 14f;
    public float walkSpeed = 9f;
    public float accel = 3f;


    public float movementMultiplier;
    public float airMultiplier = .04f;
    public float wallRunGravity = 3f;

    private float isSprinting;

    PlayerControls playerControls;

    public Transform orientation;

    Rigidbody rb;

    public Transform groundCheck;
    public LayerMask groundLayer, wallLayer;

    public float checkRadius;
    public float jumpVelocity;
    public bool isGrounded;

    public bool canJump;

    private float playerHeight;

    public bool wallLeft, wallRight;
    public float wallDistance;
    public float forceMultiplier, wallJumpForce, jumpWallForce;


    public int wallJump;

    Ray wallLeftRay;
    Ray wallRightRay;
    RaycastHit slopeHit;

    RaycastHit wallLeftRayOut;
    RaycastHit wallRightRayOut;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        playerHeight = transform.localScale.y;

        
        Detections();
        HandleDrag();
        SpeedControl();


    }

    private void FixedUpdate()
    {
        HandleMovement();
    }
    
    


    // CUSTOM FUNCTIONS
    void HandleMovement()
    {

        movement = orientation.transform.forward * moveInput.y + orientation.transform.right * moveInput.x;
        movement.y = 0;
        

        if(!isGrounded && SlopeCheck())
        {
            rb.AddForce((slopeMovement * setVelocity) * movementMultiplier, ForceMode.Acceleration);
        }
        else if (!isGrounded)
        {
            rb.AddForce((movement * setVelocity) * movementMultiplier * airMultiplier, ForceMode.Acceleration);
        }
        else
        {
            rb.AddForce((movement * setVelocity) * movementMultiplier, ForceMode.Acceleration);
        }

        if (wallLeft && !isGrounded)
        {
            wallJump = 1;
            rb.useGravity = false;
            Debug.Log("touching Left wall");
            rb.AddForce(Vector3.down * wallRunGravity, ForceMode.Force);
            if (moveInput.y > 0)
            {
                rb.AddForce(orientation.transform.forward * 2 * forceMultiplier, ForceMode.Acceleration);
            }
            if (playerControls.Controls.Jumping.triggered)
            {
                Debug.Log("JUMPING LEFT WALL");

                rb.AddForce(wallLeftRayOut.normal * wallJumpForce, ForceMode.Force);
                rb.AddForce(Vector3.up * jumpWallForce, ForceMode.Impulse);
            }
        }
        else if (wallRight && !isGrounded)
        {
            wallJump = 1;
            rb.useGravity = false;
            Debug.Log("touching Right wall");
            rb.AddForce(Vector3.down * wallRunGravity, ForceMode.Force);
            if (moveInput.y > 0)
            {
                rb.AddForce(orientation.transform.forward * 2 * forceMultiplier, ForceMode.Acceleration);
            }

            // Handle wall jumping force jump

            if (playerControls.Controls.Jumping.triggered)
            {
                Debug.Log("JUMPING RIGHT WALL ");

                rb.AddForce(wallRightRayOut.normal * wallJumpForce, ForceMode.Force);
                rb.AddForce(Vector3.up * jumpWallForce, ForceMode.Impulse);
            }

        } 
        else
        {
            rb.useGravity = true;
        }

    }


    void HandleDrag()
    {

        if (isGrounded || SlopeCheck())
        {
            rb.drag = dragForce;
        }
        else
        {
            rb.drag = airDrag; 
        }


    }

    void SpeedControl()
    {
        if(isSprinting > 0.9)
        {
            setVelocity = Mathf.Lerp(setVelocity, sprintValue, accel * Time.deltaTime);
        }
        else
        {
            setVelocity = Mathf.Lerp(setVelocity, walkSpeed, accel * Time.deltaTime);
        }
    }

    bool SlopeCheck()
    {

        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight / 2 + 0.8f)) // setting slope cast to player position, negative y, returns slopeHit, and playerHeight /2 + (distance)
        {
            if(slopeHit.normal != Vector3.up) // slope normal is at an angle (at a slope)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;

    }


    void Detections()
    {
        // Jumping
        isGrounded = Physics.CheckSphere(groundCheck.transform.position, checkRadius, groundLayer);
        
        if(playerControls.Controls.Jumping.triggered && (isGrounded || SlopeCheck())) // Only jump if grounded
        {
            canJump = true;
        }
        else
        {
            canJump = false;
        }
                
        if (canJump)
        {
            Jump();
        }

        // Slope
        slopeMovement = Vector3.ProjectOnPlane(movement, slopeHit.normal);


        // Wall Check

        wallLeftRay = new Ray(orientation.transform.position, -orientation.transform.right);

        wallRightRay = new Ray(orientation.transform.position, orientation.transform.right);


        wallLeft = Physics.Raycast(wallLeftRay, out wallLeftRayOut, wallDistance, wallLayer);
        wallRight = Physics.Raycast(wallRightRay, out wallRightRayOut, wallDistance, wallLayer);

    }


    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, jumpVelocity, rb.velocity.z);
    }



    private void OnEnable()
    {
        if(playerControls == null)
        {
            playerControls = new PlayerControls();

            playerControls.Controls.Mouse.performed += i => mouseInput = i.ReadValue<Vector2>();
            playerControls.Controls.Movement.performed += i => moveInput = i.ReadValue<Vector2>();
            playerControls.Controls.Sprinting.performed += i => isSprinting = i.ReadValue<float>();


            playerControls.Enable();
        }
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
        Gizmos.DrawRay(wallLeftRay.origin, wallLeftRay.direction * wallDistance);
        Gizmos.DrawRay(wallRightRay.origin, wallRightRay.direction * wallDistance);
    }

}
