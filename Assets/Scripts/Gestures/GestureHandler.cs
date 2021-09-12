using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureHandler : MonoBehaviour
{
    public TapProperty tapProperty;
    public SwipeProperty swipeProperty;
    public SpreadProperty spreadProperty;
    public RotateProperty rotateProperty;

    public static GestureHandler Instance;


    private Touch trackedFinger1;
    private Touch trackedFinger2;

    Vector2 startPoint;
    Vector2 endPoint;

    float gestureTime;

    // Touch detection
    int leftFingerID;
    int rightFingerID;
    float halfScreenWidth;

    public bool onSwipe = false;
    public int swipeDirection = 0;

    bool bothSidesTracked = false;
    public bool twoFingerHold = false;




    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    // Start is called before the first frame update
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

        onSwipe = false;

        if (Input.touchCount > 0)
        {
            if (Input.touchCount == 1)
            {
                SingleFingerTracking();
            }
            else
            {
                MultipleFingerTracking();
            }
        }


        if (leftFingerID != -1 && rightFingerID != -1)
        {
            twoFingerHold = true;
        }
        else
        {
            twoFingerHold = false;
        }
    }

    private void MultipleFingerTracking()
    {

        Touch trackedFinger;

        for (int i = 0; i < Input.touchCount; i++)
        {
            trackedFinger = Input.GetTouch(i);

            if (trackedFinger.phase == TouchPhase.Began)
            {
                // assign left and right finger
                if (trackedFinger.position.x < halfScreenWidth && leftFingerID == -1)
                {
                    leftFingerID = trackedFinger.fingerId;
                    Debug.Log("tracking Left finger");

                }
                else if (trackedFinger.position.x > halfScreenWidth && rightFingerID == -1)
                {
                    rightFingerID = trackedFinger.fingerId;
                    Debug.Log("tracking Right finger");
                }
            }
            else if (trackedFinger.phase == TouchPhase.Moved)
            {
                Debug.Log("moved");

                //if (Vector2.Distance(trackedFinger1.position, trackedFinger2.position) >= (Screen.dpi * rotateProperty.minDistance))
                //{
                //    CheckForRotation();

                //}
            }
            else if (trackedFinger.phase == TouchPhase.Ended)
            {
                if (trackedFinger.fingerId == rightFingerID)
                {
                    rightFingerID = -1;
                    Debug.Log("untracking right finger");

                }
                else if (trackedFinger.fingerId == leftFingerID)
                {
                    leftFingerID = -1;
                    Debug.Log("untracking left finger");
                }
            }
        }

       
        
    }

    private void SingleFingerTracking()
    {
        trackedFinger1 = Input.GetTouch(0);

        if (trackedFinger1.phase == TouchPhase.Began)
        {
            startPoint = trackedFinger1.position;
            gestureTime = 0;
        }
        else if (trackedFinger1.phase == TouchPhase.Ended)
        {
            endPoint = trackedFinger1.position;
            swipeDirection = 0;

            if (gestureTime <= tapProperty.tapTime && Vector2.Distance(startPoint, endPoint) <= (Screen.dpi * tapProperty.tapMaxDistance))
            {
                FireTapEvent();
            }
            else if (gestureTime <= swipeProperty.swipeTime && Vector2.Distance(startPoint, endPoint) >= (Screen.dpi * swipeProperty.minSwipeDistance))
            {
                    FireSwipeEvent();
            }
        }
        else
        {
            gestureTime += Time.deltaTime;
        }
    }

    private void FireTapEvent()
    {
        Debug.Log("Tapped");
        Debug.Log("Touch");
        Ray ray = Camera.main.ScreenPointToRay(trackedFinger1.position);
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Debug.Log("Touching " + hit.transform.name);
            if (hit.transform.name == "Soul")
            {
                Debug.Log("Destroy");
                GameManager.Instance.enemyList.Remove(hit.transform.gameObject);
                Destroy(hit.transform.gameObject);
                PlayerData.nCollectedSouls++;
                PlayerData.gold += 5;
            }
            else if (hit.transform.name == "Eyeball(Boss)")
            {
                hit.transform.gameObject.GetComponent<EnemyBehavior>().TakeDamage(PlayerData.tapDMG);
                hit.transform.gameObject.GetComponent<EnemyBehavior>().DisplayStats();
            }
            else if (hit.transform.name == "Eyeball(Soul)")
            {
                Debug.Log("Destroy");
                Destroy(hit.transform.gameObject);
                PlayerData.nCollectedSouls += 100;
                PlayerData.gold += 100;
                GameManager.Instance.levelComplete = true;
            }
        }
        }


    private void FireSwipeEvent()
    {
        onSwipe = true;
        Vector2 dir = endPoint - startPoint;

        

        // determine direction
        if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
        {
            //horizontal    
            if (dir.x > 0)
            {
                Debug.Log("Swipe Right");
                swipeDirection = 1;


                // change wep
            }
            else
            {
                Debug.Log("Swipe Left");
                swipeDirection = -1;

            }
        }
        else
        {

            //// vertical
            ////horizontal    
            //if (dir.y > 0)
            //{
            //    Debug.Log("Swipe Up");


            //}
            //else
            //{
            //    Debug.Log("Swipe Down");


            //};
        }

    }
}
