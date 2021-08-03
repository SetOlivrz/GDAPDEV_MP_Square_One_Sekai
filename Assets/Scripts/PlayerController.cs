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
    private float touchTime;
    private float maxSwipeDuration = 0.5f;

    // Touch detection
    int leftFingerID, rightFingerID;
    float halfScreenWidth;

    [SerializeField] private FlashImage flashImage = null;
    Color newColor = Color.white;
    int flashOpacity = 1;

    private Vector3 fp;   //First touch position
    private Vector3 lp;   //Last touch position

    private bool isShoot = false;
    private bool isHold = false;

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
        isShoot = false;
        // Handles input
        GetTouchInput();

        if (rightFingerID != -1)
        {
            // Ony look around if the right finger is being tracked
           // Debug.Log("Rotating");
            LookAround();
        }

        if (leftFingerID != -1)
        {
            // Ony move if the left finger is being tracked
            //Debug.Log("Moving");
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
                    touchTime = Time.time;
                    fp = t.position;
                    lp = t.position;

                    if (t.position.x < halfScreenWidth && leftFingerID == -1)
                    {
                        // Start tracking the right finger if it was not previously being tracked
                        leftFingerID = t.fingerId;
                       // Debug.Log("tracking Left finger");
                        

                    }
                    else if (t.position.x > halfScreenWidth && rightFingerID == -1)
                    {
                        // Start tracking the leftfinger if it was not previously being tracked
                        rightFingerID = t.fingerId;
                      //  Debug.Log("tracking Right finger");
                    }
                    break;
                case TouchPhase.Ended:
                    {
                        if(!isHold && t.fingerId == leftFingerID)
                        {
                            flashImage.StartFlash(0.25f, flashOpacity, newColor);
                            isShoot = true;
                        }
                        
                        float touchDuration = Time.time - touchTime;
                        lp = t.position;
                        if ((Mathf.Abs(lp.x - fp.x) > Screen.height * 0.125 || Mathf.Abs(lp.y - fp.y) > Screen.height * 0.125) && touchDuration < maxSwipeDuration)
                        {//It's a drag

                            if ((lp.x > fp.x))  //If the movement was to the right)
                            {   //Right swipe
                                Debug.Log("Right Swipe");
                            }
                            else
                            {   //Left swipe
                                Debug.Log("Left Swipe");
                            }

                        }

                        isHold = false;
                    }
                    break;
                case TouchPhase.Canceled:

                    if (t.fingerId == leftFingerID)
                    {
                        // Stop tracking the left finger
                        leftFingerID = -1;
                      //  Debug.Log("Stopped tracking left finger");
                    }
                    else if (t.fingerId == rightFingerID)
                    {
                        // Stop tracking the right finger
                        rightFingerID = -1;
                        //Debug.Log("Stopped tracking right finger");
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

                    if (t.fingerId == leftFingerID)
                    {
                        if(Time.time - touchTime > 1f)
                        isHold = true;
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

    public bool getShootingBool()
    {
        return isShoot;
    }

    public bool getHoldingBool()
    {
        return isHold;
    }
}
