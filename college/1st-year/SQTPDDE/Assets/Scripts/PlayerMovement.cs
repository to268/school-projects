using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using UnityEngine.XR;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private GameObject skeleton;
    
    [SerializeField]
    private Transform orientation;
    
    [SerializeField]
    private Animator animator;
    
    [SerializeField]
    private TMP_Text boneText;

    [SerializeField]
    private float gravityScale = 8f;

    [Header("Keybinds")] 
    [SerializeField]
    private KeyCode jumpKey = KeyCode.Space;
    
    [SerializeField]
    private KeyCode runKey = KeyCode.LeftShift;
    
    [SerializeField]
    private KeyCode crounchKey = KeyCode.C;
    
    [SerializeField]
    private KeyCode rollKey = KeyCode.LeftControl;
    
    [SerializeField]
    private KeyCode trollKey = KeyCode.F;

    [SerializeField]
    private MouseButton throwKey = MouseButton.LeftMouse;
    
    [Header("Movement")]
    [SerializeField]
    private float walkSpeed = 700f;
    
    [SerializeField]
    private float runSpeed = 900f;
    
    [SerializeField]
    private float crounchSpeed = 250f;
    
    [Header("Jump")]
    [SerializeField]
    private float jumpHeight = 500f;
    
    [SerializeField]
    private float jumpCooldown = 0.5f;
    
    [SerializeField]
    private float airMultiplier = 1.05f;
    
    [Header("Ground")]
    [SerializeField]
    private float groundDrag = 30f;
    
    [SerializeField]
    private float playerHeight = 2f;
    
    [SerializeField]
    private LayerMask groundMask;
    
    [Header("Climb")]
    [SerializeField]
    private LayerMask wallMask;
    
    [SerializeField]
    private float climbCooldown = 2f;
    
    [SerializeField]
    private float wallDistance = 1f;
    
    [Header("Throw")]
    [SerializeField]
    private Transform throwPoint;
    
    [SerializeField]
    private Transform boneStacker;
    
    [SerializeField]
    private GameObject bone;
    
    [SerializeField]
    private float throwSpeed = 10f;
    
    [SerializeField]
    private float throwCooldown = 1f;
    
    [SerializeField]
    private int maxBones = 30;

    private bool isGrounded;
    private bool canJump;
    private bool isRolling;
    private bool canClimb;
    private bool isClimbing;
    private bool canMove;
    private bool canThrow;
    
    private float horizontalInput;
    private float verticalInput;
    private float movementSpeed;
    
    private Vector3 movementDirection;
    private Rigidbody rb;
    private int boneCount;
        
    // Start is called before the first frame update
    void Start()
    {
        horizontalInput = 0f;
        verticalInput = 0f;
        movementDirection = Vector3.zero;
        movementSpeed = walkSpeed;
        isGrounded = true;
        canJump = true;
        isRolling = false;
        canClimb = false;
        isClimbing = false;
        canMove = true;
        boneCount = maxBones;
        
        if (SceneManager.GetActiveScene().name == "Home")
            canThrow = false;
        else
            canThrow = true;
        
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (boneCount <= 0)
        {
            canMove = false;
            canThrow = false;
            animator.SetBool("isDead", true);
            Invoke(nameof(Die), 3.8f);
        } 
        else if (((float)boneCount / maxBones) <= 0.3f)
        {
            animator.SetBool("isInjured", true);
        }
        
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 
                              playerHeight * 0.5f + 0.2f, groundMask);
        
        canClimb = Physics.Raycast(orientation.position, transform.forward, 
                                   wallDistance, wallMask);
        
        HandleInputs();
        SpeedControl();
        
        if (isGrounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0f;
    }

    void FixedUpdate()
    {
        if (canMove)
            MovePlayer();

        ApplyGravity();
    }

    void HandleInputs()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        
        CheckWallCrash();
        CheckClimb();
        CheckThrow();
        
        ResetRolling();

        if (Input.GetKeyDown(trollKey))
        {
            Fall();
        }
        else if (Input.GetKey(rollKey) && !isRolling && isGrounded)
        {
            animator.SetBool("isRolling", true);
            isRolling = true;
        }
        else if (Input.GetKey(crounchKey) && isGrounded)
        {
            movementSpeed = crounchSpeed;
            animator.SetBool("isCrounching", true);
        }
        else if (Input.GetKey(runKey) && isGrounded)
        {
            movementSpeed = runSpeed;
            animator.SetBool("isRunning", true);
        }
        else
        {
            movementSpeed = walkSpeed;
            animator.SetBool("isRunning", false);
            animator.SetBool("isCrounching", false);
        }
       
        if (Input.GetKey(jumpKey) && canJump && isGrounded && !canClimb && canMove)
        {
            animator.SetBool("isJumping", true);
            canJump = false;
            Jump();
            
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    void MovePlayer()
    {
        movementDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        animator.SetFloat("HorizontalAxis", horizontalInput);
        animator.SetFloat("VerticalAxis", verticalInput);

        if (isClimbing)
            rb.AddForce(Vector3.up * (movementSpeed * 20f), ForceMode.Force);
        else if (isGrounded)
            rb.AddForce(movementDirection.normalized * (movementSpeed * 50f), ForceMode.Force);
        else
            rb.AddForce(movementDirection.normalized * ((movementSpeed * 50f) * airMultiplier) , ForceMode.Force);
    }

    void ApplyGravity()
    {
        Vector3 gravity = -9.81f * gravityScale * Vector3.up;
        rb.AddForce(gravity, ForceMode.Acceleration);
    }

    void SpeedControl()
    {
        Vector3 flatVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        
        if (flatVelocity.magnitude > movementSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * movementSpeed;
            rb.velocity = new Vector3(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
        }
    }

    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * (jumpHeight * 25f), ForceMode.Impulse);
    }

    void CheckWallCrash()
    {
        bool hasCrashed = Physics.Raycast(orientation.position, transform.forward, 
                              0.5f, wallMask);
        
        if (rb.velocity.magnitude >= 4f && hasCrashed && canMove)
        {
            animator.SetBool("hasWallCrashed", true);
            rb.AddForce((Vector3.down + Vector3.back) * (jumpHeight * 10f), ForceMode.Impulse);
            canMove = false;
            Invoke(nameof(ResetWallCrash), 3.6f);
        }
    }

    void CheckClimb()
    {
        if (Input.GetKeyDown(jumpKey) && canClimb)
        {
            animator.SetBool("isOnWall", true);
            isClimbing = true;
            Invoke(nameof(ResetClimb), climbCooldown);
        } 
        else if ((Input.GetKeyUp(jumpKey) && isClimbing) || (isClimbing && !canClimb))
        {
            ResetClimb();
        }
    }

    void CheckThrow()
    {
        if (Input.GetMouseButtonUp((int)throwKey) && canThrow && canMove && boneCount > 0)
        {
            animator.SetBool("isThrowing", true);
            Instantiate(bone, throwPoint.position, transform.rotation, boneStacker);
            
            Rigidbody boneRb = boneStacker.GetChild(boneStacker.childCount - 1)
                .GetChild(0).GetComponent<Rigidbody>();
            
            boneRb.AddForce(throwPoint.forward * throwSpeed, ForceMode.Impulse);
            
            boneCount--;
            UpdateBoneText();

            canThrow = false;
            Invoke(nameof(ResetThrowingAnimation), throwCooldown-0.5f);
            Invoke(nameof(ResetThrowing), throwCooldown);
        }
    }

    void Fall()
    {
        canMove = false;
        canThrow = false;
        
        animator.SetBool("hasFalled", true);
        Invoke(nameof(ResetFall), 2.3f);
    }

    void ResetJump()
    {
        canJump = true;
        animator.SetBool("isJumping", false);
    }
    
    void ResetClimb()
    {
        if (!isClimbing) return;
        
        isClimbing = false;
        animator.SetBool("isOnWall", false);
        
        if (!isGrounded)
            rb.AddForce((new Vector3(0f, 0.5f, -1f)) * (jumpHeight * 15f), ForceMode.Impulse);
    }

    void ResetWallCrash()
    {
        animator.SetBool("hasWallCrashed", false);
        canMove = true;
    }

    void ResetRolling()
    {
        if (isRolling)
        {
            animator.SetBool("isRolling", false);
            isRolling = false;
        }
    }

    void ResetThrowingAnimation()
    {
        animator.SetBool("isThrowing", false);
    }
    
    void ResetThrowing()
    {
        canThrow = true;
    }

    void ResetFall()
    {
        animator.SetBool("hasFalled", false);
        
        canMove = true;
        canThrow = true;
    }
    
    void Die()
    {
        Destroy(skeleton);
        Destroy(this);
        SceneLoader.LoadDeathScene();
    }

    public void TakeDamage(int boneAmount)
    {
        boneCount -= boneAmount;
        UpdateBoneText();
        
        if (Random.Range(1, 16) == 1)
            Fall();
    }

    void UpdateBoneText()
    {
        boneText.text = "x" + boneCount;
    }
}
