using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GestureManager : MonoBehaviour
{
    
    public static GestureManager Instance;

    float touchTime;
    private Vector3 fp;   //First touch position
    private Vector3 lp;   //Last touch position

    // Touch detection
    int leftFingerID;
    int rightFingerID;
    float halfScreenWidth;
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

    private Touch trackedFinger;



    public class SwipeAction
    {
        public bool isSwiping = false;
        public int direction = 0;   // right = 1 : left = -1
    }

    public SwipeAction onSwipe;
    // Start is called before the first frame update
    void Start()
    {
        // id = -1 means the finger is not being tracked
        leftFingerID = -1;
        rightFingerID = -1;

        // only calculate once
        halfScreenWidth = Screen.width / 2;

        onSwipe = new SwipeAction();
    }

    // Update is called once per frame
    void Update()
    {


        if(onSwipe.isSwiping)
        {
            onSwipe.isSwiping = false;
            onSwipe.direction = 0;
        }

        handleTouchInput();
        
        if (leftFingerID != -1 && rightFingerID != -1)
        {
            twoFingerHold =  true;
        }
        else
        {
            twoFingerHold = false;
        }
    }

    private void OnDrawGizmos()
    {
        if (Input.touchCount > 0)
        {
                Ray ray = Camera.main.ScreenPointToRay(trackedFinger.position);
                Gizmos.DrawIcon(ray.GetPoint(100), "ghost");
        }
    }

    private void handleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                trackedFinger = Input.GetTouch(i);

                if (trackedFinger.phase == TouchPhase.Began)
                {
                    touchTime = Time.time;
                    fp = trackedFinger.position;
                    lp = trackedFinger.position;

                    // setting finger id
                    if (trackedFinger.position.x < halfScreenWidth && leftFingerID == -1)
                    {
                        // Start tracking the right finger if it was not previously being tracked
                        leftFingerID = trackedFinger.fingerId;
                         Debug.Log("tracking Left finger");

                    }
                    else if (trackedFinger.position.x > halfScreenWidth && rightFingerID == -1)
                    {
                        // Start tracking the leftfinger if it was not previously being tracked
                        rightFingerID = trackedFinger.fingerId;
                          Debug.Log("tracking Right finger");
                    }
                }

                if (trackedFinger.phase == TouchPhase.Ended)
                {
                    float touchDuration = Time.time - touchTime;
                    lp = trackedFinger.position;

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

                    // if within require swipe distance and duration
                    if ((Mathf.Abs(lp.x - fp.x) > Screen.height * 0.125 || Mathf.Abs(lp.y - fp.y) > Screen.height * 0.125) && touchDuration < 0.35f) //touch moved distance and hold time
                    {
                        if (twoFingerHold == false)
                        {
                            onSwipe.isSwiping = true;
                            if ((lp.x > fp.x))  //If the movement was to the right)
                            {   //Right swipe
                                Debug.Log("Right Swipe");
                                onSwipe.direction = 1;
                            }

                            else
                            {   //Left swipe
                                Debug.Log("Left Swipe");
                                onSwipe.direction = -1;
                            }
                        }
                        else
                        {
                            Debug.Log("cant change wepon, shiedl is active");
                        }
                        
                        //changeWeapon();
                    }

                    else
                    {
                        Debug.Log("Touch");
                        Ray ray = Camera.main.ScreenPointToRay(trackedFinger.position);
                        RaycastHit hit = new RaycastHit();

                        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
                        {
                            Debug.Log("Touching " + hit.transform.name);
                            if(hit.transform.name == "Soul")
                            {
                                Debug.Log("Destroy");
                                GameManager.Instance.enemyList.Remove(hit.transform.gameObject);
                                Destroy(hit.transform.gameObject);
                                PlayerData.nCollectedSouls++;
                                PlayerData.gold += 5;
                            }
                            else if (hit.transform.name == "Eyeball(Boss)")
                            {
                                hit.transform.gameObject.GetComponent<BossBehavior>().TakeDamage(PlayerData.tapDMG);
                                hit.transform.gameObject.GetComponent<BossBehavior>().DisplayStats();
                            }
                            else if (hit.transform.name == "Eyeball(soul)")
                            {
                                GameManager.Instance.levelComplete = true;
                            }
                        }
                    }

                }
            }
        }
    }

    //private void changeWeapon()
    //{
        

    //    if ((int)currentWeapon == 4)
    //    {
    //        currentWeapon = (weaponType)0;
    //        //SwitchCam();
    //    }

    //    else if ((int)currentWeapon == -1)
    //    {
    //        currentWeapon = (weaponType)3;
    //    }
    //    Debug.Log(currentWeapon);
    //    //SwitchCam();
    //    //SwitchFilm();
    //}

    //public weaponType getCurrentWeapon()
    //{
    //    return currentWeapon;
    //}

    //public void SwitchCam()
    //{
    //    this.FlashCam.SetActive(false);
    //    this.SonicCam.SetActive(false);
    //    this.PumpCam.SetActive(false);

    //    switch((int)currentWeapon)
    //    {
    //        case 0: break;
    //        case 1: FlashCam.SetActive(true); break;
    //        case 2: SonicCam.SetActive(true); break;
    //        case 3: PumpCam.SetActive(true); break;
    //    }
    //}

    //public void SwitchFilm()
    //{
    //    if(weaponsPanel!=null)
    //    {
    //        switch ((int)currentWeapon)
    //        {
    //            case 0: filmColor = Color.white; break;
    //            case 1: filmColor = Color.red; break;
    //            case 2: filmColor = Color.green; break;
    //            case 3: filmColor = Color.blue; break;
    //        }

    //        filmColor.a = 1f;
    //        weaponsPanel.GetComponent<Image>().color = filmColor;
    //    }
        
        
    //}
}
