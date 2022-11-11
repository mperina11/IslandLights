using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    // Character
    private CharacterController characterController;

    [SerializeField]
    private float maxSpeed = 1;

    [SerializeField]
    private float sprint_speed = 2;

    [SerializeField]
    private Transform cameraTransform;

    [SerializeField]
    private float rotationspeed = 500;

    // Jumping
    [SerializeField]
    private float jumpSpeed;

    [SerializeField]
    private float jumpButtonGracePeriod;

    private Animator animator;
    private float ySpeed;
    private float originalStepOffset;
    private float? lastGroundedTime; // is nullable
    private float? jumpButtonPressedTime; // is nullable

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        originalStepOffset = characterController.stepOffset;
    }


    // Update is called once per frame
    void Update()
    {
        // Get Input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Movement
        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);

        // Gamepad varying speed
        float magnitude = movementDirection.magnitude; // get magnitude
        magnitude = Mathf.Clamp01(magnitude); // prevent being above 1

        if (Input.GetKey(KeyCode.LeftShift))
        {
            magnitude *= sprint_speed;
        }

        float speed = magnitude * maxSpeed;
        movementDirection = Quaternion.AngleAxis(cameraTransform.rotation.eulerAngles.y, Vector3.up) * movementDirection;
        movementDirection.Normalize();

        ySpeed += Physics.gravity.y * Time.deltaTime;

        // jumping logic
        if (characterController.isGrounded)
        {
            lastGroundedTime = Time.time;
        }
        if (Input.GetButtonDown("Jump"))
        {
            jumpButtonPressedTime = Time.time;
        }

        if (Time.time - lastGroundedTime <= jumpButtonGracePeriod)
        {
            characterController.stepOffset = originalStepOffset;
            ySpeed = -0.5f;

            if (Time.time - jumpButtonPressedTime <= jumpButtonGracePeriod)
            {
                ySpeed = jumpSpeed;
                jumpButtonPressedTime = null;
                lastGroundedTime = null;
            }
        }
        else
        {
            characterController.stepOffset = 0;
        }

        Vector3 velocity = movementDirection * speed;
        velocity.y = ySpeed;

        characterController.Move(velocity * Time.deltaTime);
        //transform.Translate(movementDirection * magnitude * speed * Time.deltaTime, Space.World); // without charactercontroller

        // Rotate
        if (movementDirection != Vector3.zero)
        {
            animator.SetBool("isMoving", true);
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationspeed * Time.deltaTime);

        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }


}