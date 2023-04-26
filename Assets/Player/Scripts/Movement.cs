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

    RaycastHit slopeHit;


    public float wallRayDist, wallRadius;
    private bool collisionLeftWall, collisionRightWall;

    public Transform wallLeft, wallRight;

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
        
        if(isGrounded && !SlopeCheck())
        {
            rb.AddForce((movement * setVelocity) * movementMultiplier, ForceMode.Acceleration);
        }
        else if(!isGrounded && SlopeCheck())
        {
            rb.AddForce((slopeMovement * setVelocity) * movementMultiplier, ForceMode.Acceleration);
        }
        else
        {
            rb.AddForce((movement * setVelocity) * movementMultiplier * airMultiplier, ForceMode.Acceleration);
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

        // Wall Jumping
        if (Physics.CheckSphere(wallRight.transform.position, wallRadius, wallLayer) && !isGrounded)
        {
            collisionLeftWall = true;
        } else if(Physics.CheckSphere(wallLeft.transform.position, wallRadius, wallLayer) && !isGrounded)
        {
            collisionRightWall = true;
        }
        else
        {
            collisionRightWall = false;
            collisionLeftWall = false;
        }

      

        // Wall run
        if (collisionLeftWall)
        {
            WallRun();
            
        } else if (collisionRightWall)
        {
            WallRun();
        }
    }

    void WallRun()
    {
        rb.useGravity = false;

        rb.AddForce(Vector3.down * wallRunGravity, ForceMode.Force);

        if (playerControls.Controls.Jumping.triggered)
        {
            if (collisionLeftWall)
            {
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(-orientation.transform.right * 100f, ForceMode.Force);
            } 
            else if (collisionRightWall)
            {
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(orientation.transform.right * 100f, ForceMode.Force);
            }
        }
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
        Gizmos.DrawWireSphere(wallLeft.position, wallRadius);
        Gizmos.DrawWireSphere(wallRight.position, wallRadius);


    }

}
