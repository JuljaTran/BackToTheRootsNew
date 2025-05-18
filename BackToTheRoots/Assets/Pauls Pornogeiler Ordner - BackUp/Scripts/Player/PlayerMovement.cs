using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float groundDrag;
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    public float doubleJumpForce;
    bool readyToJump;
    bool canDoubleJump;


    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.3f;
    public LayerMask whatIsGround;
    public bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;
    Rigidbody rb;

    [Header("Mic Settings")]
    public float screamThreshold = 0.3f;
    private AudioClip micClip;
    private string micName;
    private int sampleWindow = 128;

    [Header("Animator")]
    public Animator animator;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;

        animator = GetComponentInChildren<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator wurde nicht gefunden!");
        }

        // Mikrofon starten
        if (Microphone.devices.Length > 0)
        {
            micName = Microphone.devices[0];
            micClip = Microphone.Start(micName, true, 1, AudioSettings.outputSampleRate);
        }
        else
        {
            Debug.LogWarning("Kein Mikrofon gefunden.");
        }
    }

    void Update()
    {
        // Ground Check
        grounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, whatIsGround);

        MyInput();
        SpeedControl();

        // Drag Handling
        rb.drag = grounded ? groundDrag : 0f;

        // Wenn geschrien wird und Double Jump erlaubt
        if (!grounded && canDoubleJump && GetMicLoudness() > screamThreshold)
        {
            Debug.Log("Schrei erkannt → Double Jump!");
            DoubleJump();
        }

        // Animator Parameter setzen
        float speed = new Vector3(rb.velocity.x, 0, rb.velocity.z).magnitude;
        animator.SetFloat("Speed", speed);
        animator.SetBool("Grounded", grounded);
        Debug.Log("Grounded: " + grounded + " | Speed: " + speed);
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        else
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);

        // Double Jump aktivieren
        canDoubleJump = true;
    }

    private void DoubleJump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(transform.up * (jumpForce * doubleJumpForce), ForceMode.Impulse);
        canDoubleJump = false;
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    private float GetMicLoudness()
    {
        if (micClip == null) return 0f;

        float[] data = new float[sampleWindow];
        int micPosition = Microphone.GetPosition(micName) - sampleWindow + 1;
        if (micPosition < 0) return 0f;

        micClip.GetData(data, micPosition);

        float levelMax = 0;
        for (int i = 0; i < sampleWindow; ++i)
        {
            float wavePeak = Mathf.Abs(data[i]);
            if (levelMax < wavePeak)
                levelMax = wavePeak;
        }

        return levelMax;
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;
        Gizmos.color = grounded ? Color.green : Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
