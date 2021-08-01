using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float cameraSensitivity;


    // Camera control
    private Vector2 lookInput;
    private float cameraPitch;

    // Touch detection
    int leftFingerID, rightFingerID;
    float halfScreenWidth;

    [SerializeField] private FlashImage flashImage = null;
    [SerializeField] private Color newColor = Color.white;
    [SerializeField] private float flashOpacity = 1;

    private bool isFlash = false;

    void Start()
    {
        // id = -1 means the finger is not being tracked
        leftFingerID = -1;
        rightFingerID = -1;

        // only calculate once
        halfScreenWidth = Screen.width / 2;
    }

    // Update is called once per frame
    void Update()
    {
        isFlash = false;
        // Handles input
        GetTouchInput();

        if (rightFingerID != -1)
        {
            // Ony look around if the right finger is being tracked
            Debug.Log("Rotating");
            LookAround();
        }

        if (leftFingerID != -1)
        {
            // Ony move if the left finger is being tracked
            Debug.Log("Moving");
            //Move();
        }
    }

    void GetTouchInput()
    {
        // Iterate through all the detected touches
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch t = Input.GetTouch(i);

            // Check each touch's phase
            switch (t.phase)
            {
                case TouchPhase.Began:

                    if (t.position.x < halfScreenWidth && leftFingerID == -1)
                    {
                        // Start tracking the right finger if it was not previously being tracked
                        leftFingerID = t.fingerId;
                        Debug.Log("tracking Left finger");
                        flashImage.StartFlash(0.25f, flashOpacity, newColor);
                        isFlash = true;

                    }
                    else if (t.position.x > halfScreenWidth && rightFingerID == -1)
                    {
                        // Start tracking the leftfinger if it was not previously being tracked
                        rightFingerID = t.fingerId;
                        Debug.Log("tracking Right finger");
                    }
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:

                    if (t.fingerId == leftFingerID)
                    {
                        // Stop tracking the left finger
                        leftFingerID = -1;
                        Debug.Log("Stopped tracking left finger");
                    }
                    else if (t.fingerId == rightFingerID)
                    {
                        // Stop tracking the right finger
                        rightFingerID = -1;
                        Debug.Log("Stopped tracking right finger");
                    }

                    break;
                case TouchPhase.Moved:

                    // Get input for looking around
                    if (t.fingerId == rightFingerID)
                    {
                        lookInput = t.deltaPosition * cameraSensitivity * Time.deltaTime;
                    }
                    break;

                case TouchPhase.Stationary:
                    // Set the look input to zero if the finger is still
                    if (t.fingerId == rightFingerID)
                    {
                        lookInput = Vector2.zero;
                    }
                    break;
            }
        }
    }

    void LookAround()
    {
        // vertical (pitch) rotation
        cameraPitch = Mathf.Clamp(cameraPitch - lookInput.y, -45f, 45f);
        cameraTransform.localRotation = Quaternion.Euler(cameraPitch, 0, 0);

        // horizontal (yaw) rotation
        transform.Rotate(transform.up, lookInput.x);
    }

    public bool getCameraFlashBool()
    {
        return isFlash;
    }
}
