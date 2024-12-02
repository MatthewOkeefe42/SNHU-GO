using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraFollow : MonoBehaviour
{
    public Transform target; // The target the camera will follow (e.g., the player)
    public Vector3 offset; // Offset position relative to the target
    public float smoothSpeed = 0.125f; // Speed for smoothing camera movement
    public float rotationSpeed = 5f; // Speed for touch rotation

    private Vector3 currentOffset; // Current offset to adjust during rotation
    private Vector2 previousTouchPosition; // Previous touch position for calculating rotation

    void Start()
    {
        // Initialize the current offset
        currentOffset = offset;
    }

    void LateUpdate()
    {
        if (target != null)
        {
            HandleTouchRotation();

            // Calculate the desired position
            Vector3 desiredPosition = target.position + currentOffset;

            // Smoothly interpolate to the desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Apply the position to the camera
            transform.position = smoothedPosition;

            // Optionally, make the camera look at the target
            transform.LookAt(target);
        }
    }

    void HandleTouchRotation()
    {
        if (Input.touchCount == 1) // Check for a single touch
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                // Calculate the rotation angles based on touch movement
                Vector2 delta = touch.deltaPosition;

                float yaw = delta.x * rotationSpeed * Time.deltaTime; // Horizontal rotation
                float pitch = -delta.y * rotationSpeed * Time.deltaTime; // Vertical rotation

                // Rotate the offset vector around the target
                currentOffset = Quaternion.Euler(pitch, yaw, 0) * currentOffset;
            }

            // Save the previous touch position for the next frame
            previousTouchPosition = touch.position;
        }
    }
}
