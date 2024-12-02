using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterRotationY : MonoBehaviour
{
    public float rotationSpeed = 5f; // Speed of rotation
    private float rotationY = 0f;   // Current rotation around the Y-axis

    void Update()
    {
        HandleTouchRotation();
    }

    void HandleTouchRotation()
    {
        if (Input.touchCount == 1) // Detect a single touch
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                // Get the vertical drag input (Y-axis movement of the finger)
                float verticalDelta = touch.deltaPosition.x;

                // Adjust rotation around the Y-axis
                rotationY -= verticalDelta * rotationSpeed * Time.deltaTime; // Subtract for natural inverted controls

                // Apply the rotation to the character
                transform.rotation = Quaternion.Euler(0f, rotationY, 0f);
            }
        }
    }
}


