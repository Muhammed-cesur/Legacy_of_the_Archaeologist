using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float rotationSpeed = 100f;
    public float jumpForce = 5f;
    public float runMultiplier = 1.5f;
    public Transform cameraTransform;

    private Rigidbody rb;
    private bool isGrounded = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Read player input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        bool isJumping = Input.GetKeyDown(KeyCode.Space);

        // Rotate the player with respect to the camera's direction
        Vector3 cameraForward = cameraTransform.forward;
        cameraForward.y = 0; // Ensure the player doesn't tilt up or down
        Quaternion rotation = Quaternion.LookRotation(cameraForward);
        rb.MoveRotation(rotation);

        // Move the player
        Vector3 movement = (horizontalInput * cameraTransform.right + verticalInput * cameraForward).normalized;
        float currentSpeed = movementSpeed;
        if (isRunning)
            currentSpeed *= runMultiplier;
        rb.MovePosition(transform.position + movement * currentSpeed * Time.deltaTime);

        // Jumping
        if (isJumping && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the player is grounded
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
