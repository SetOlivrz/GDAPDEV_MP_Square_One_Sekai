using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureManager : MonoBehaviour
{

    public static GestureManager Instance;

    float touchTime;
    private Vector3 fp;   //First touch position
    private Vector3 lp;   //Last touch position
    public enum weaponType
    {
        soulCam = 0,
        ghostCam = 1,
        batCam = 2,
        pumpkinCam = 3
    }

    public weaponType currentWeapon = 0;

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

    private Touch trackedFinger1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        handleTouchInput();
    }

    private void OnDrawGizmos()
    {
        if (Input.touchCount > 0)
        {
                Ray ray = Camera.main.ScreenPointToRay(trackedFinger1.position);
                Gizmos.DrawIcon(ray.GetPoint(100), "ghost");
        }
    }

    private void handleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                trackedFinger1 = Input.GetTouch(i);
                if (trackedFinger1.phase == TouchPhase.Began)
                {
                    touchTime = Time.time;
                    fp = trackedFinger1.position;
                    lp = trackedFinger1.position;
                }

                if (trackedFinger1.phase == TouchPhase.Ended)
                {
                    float touchDuration = Time.time - touchTime;
                    lp = trackedFinger1.position;
                    if ((Mathf.Abs(lp.x - fp.x) > Screen.height * 0.125 || Mathf.Abs(lp.y - fp.y) > Screen.height * 0.125) && touchDuration < 0.35f) //touch moved distance and hold time
                    {
                        changeWeapon();
                    }

                    else
                    {
                        Debug.Log("Touch");
                        Ray ray = Camera.main.ScreenPointToRay(trackedFinger1.position);
                        RaycastHit hit = new RaycastHit();

                        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
                        {
                            Debug.Log("Touching " + hit.transform.name);
                            if(hit.transform.name == "Soul")
                            {
                                Debug.Log("Destroy");
                                GameManager.Instance.enemyList.Remove(hit.transform.gameObject);
                                Destroy(hit.transform.gameObject);
                            }    
                        }
                    }

                }
            }
        }
    }

    private void changeWeapon()
    {
        if ((lp.x > fp.x))  //If the movement was to the right)
        {   //Right swipe
            Debug.Log("Right Swipe");
            currentWeapon += 1;
        }

        else
        {   //Left swipe
            Debug.Log("Left Swipe");
            currentWeapon -= 1;
        }

        if ((int)currentWeapon == 4)
        {
            currentWeapon = (weaponType)0;
        }

        else if ((int)currentWeapon == -1)
        {
            currentWeapon = (weaponType)3;
        }
        Debug.Log(currentWeapon);
    }

    public weaponType getCurrentWeapon()
    {
        return currentWeapon;
    }

}
