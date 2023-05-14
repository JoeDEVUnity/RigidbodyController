using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Movement : MonoBehaviour
{
    [Header("Input Variables")]
    public Vector2 moveInput;

    public Vector2 mouseInput;

    public Vector3 slopeMovement;

    private Vector3 movement;
    public float isSwitching { get; private set; }
    public float fireValue { get; private set; }
    public RayScript rayScript;


    [Header("Physics Variables")]

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

    [Header("Check Variables")]

    public Transform groundCheck;
    public LayerMask groundLayer, wallLayer;

    public float checkRadius;
    public float jumpVelocity;
    public bool isGrounded;

    public bool canJump;

    private float playerHeight;

    [Header("Wall Jumping Variables")]

    public bool wallLeft;
    public bool wallRight;
    public float wallDistance;
    public float forceMultiplier, wallJumpForce, jumpWallForce;

    private float wallJumpTimer;

    public int wallJump;

    private float jumpValue;

    Ray wallLeftRay;
    Ray wallRightRay;
    RaycastHit slopeHit;

    RaycastHit wallLeftRayOut;
    RaycastHit wallRightRayOut;

    [Header("Slider Variables")]

    public Slider heatSlider;
    public Slider healthSlider;
    [SerializeField]
    private float heatMax, healthMax;
    public float heatRate;
    public float currentHeat { get; private set; }
    public float currentHP;
    public float regenTimer, regenValue;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        currentHeat = heatMax;
        heatSlider.maxValue = heatMax;

        currentHP = healthMax;
        healthSlider.maxValue = healthMax;

    }

    // Update is called once per frame
    void Update()
    {
        playerHeight = transform.localScale.y;
        wallJumpTimer += Time.deltaTime;
        
        Detections();
        HandleDrag();
        SpeedControl();
        HandleBars();

    }

    private void FixedUpdate()
    {
        HandleMovement();
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
        
        if(jumpValue > 0 && (isGrounded || SlopeCheck())) // Only jump if grounded
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
            rb.AddForce((movement * setVelocity) * airMultiplier, ForceMode.Acceleration);
        }
        else
        {
            rb.AddForce((movement * setVelocity) * movementMultiplier, ForceMode.Acceleration);
        }

        if (wallLeft && !isGrounded)
        {
            rb.useGravity = false;
            Debug.Log("touching Left wall");
            rb.AddForce(Vector3.down * wallRunGravity, ForceMode.Force);
            if (moveInput.y > 0)
            {
                rb.AddForce(orientation.transform.forward * 2 * forceMultiplier, ForceMode.Acceleration);
            }
            if (jumpValue > 0 && wallJumpTimer > 0.1f)
            {
                Debug.Log("JUMPING LEFT WALL");

                rb.AddForce(orientation.transform.right * wallJumpForce, ForceMode.Impulse);
                rb.AddForce(Vector3.up * jumpWallForce, ForceMode.Impulse);
                wallJumpTimer = 0f;
            }
        }
        else if (wallRight && !isGrounded)
        {
            rb.useGravity = false;
            Debug.Log("touching Right wall");
            rb.AddForce(Vector3.down * wallRunGravity, ForceMode.Force);
            if (moveInput.y > 0)
            {
                rb.AddForce(orientation.transform.forward * 2 * forceMultiplier, ForceMode.Acceleration);
            }

            // Handle wall jumping force jump

            if (jumpValue > 0 && wallJumpTimer > 0.1f)
            {
                Debug.Log("JUMPING RIGHT WALL ");

                rb.AddForce(-orientation.transform.right * wallJumpForce, ForceMode.Impulse);
                rb.AddForce(Vector3.up * jumpWallForce, ForceMode.Impulse);
                wallJumpTimer = 0f;
            }

        } 
        else
        {
            rb.useGravity = true;
        }

    }
    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, jumpVelocity, rb.velocity.z);
    }


    // Function made to handle the value of heat, health, etc..

    void HandleBars()
    {
        // Cap heatValue
        heatSlider.value = currentHeat;
        currentHeat = Mathf.Clamp(currentHeat, 0, heatMax);

        healthSlider.value = currentHP;

        // Regen health only if regen timer is greater than 0
        regenTimer += Time.deltaTime;

        if(regenTimer > 3 && currentHP < healthMax)
        {
            currentHP += regenValue * Time.deltaTime;
        }

        // When fire Value is instanced, reduce heat by heatRate 
        
        if(fireValue > 0 && rayScript.shootingMode == RayScript.ShootingTypes.Automatic && currentHeat > 0)
        {
            // Reduce by heatRate

            currentHeat -= heatRate * 2 * Time.deltaTime;

        }
        else if(currentHeat < heatMax && fireValue == 0)
        {
            // Increase by heatRate
            currentHeat += heatRate * Time.deltaTime;
        }

        // When taken damage, reduce health by X

    }


    private void OnEnable()
    {
        if(playerControls == null)
        {
            playerControls = new PlayerControls();

            playerControls.Controls.Mouse.performed += i => mouseInput = i.ReadValue<Vector2>();
            playerControls.Controls.Movement.performed += i => moveInput = i.ReadValue<Vector2>();
            playerControls.Controls.Sprinting.performed += i => isSprinting = i.ReadValue<float>();
            playerControls.Controls.Jumping.performed += i => jumpValue = i.ReadValue<float>();

            playerControls.Controls.Switching.performed += i => isSwitching = i.ReadValue<float>();
            playerControls.Controls.FireValue.performed += i => fireValue = i.ReadValue<float>();

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
