using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBehavior : MonoBehaviour
{
    [SerializeField] GameObject shieldObject;

    public bool shieldState = false;
    bool active = true;
    bool invalid = false;

    // Start is called before the first frame update
    void Start()
    {
        shieldObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!ButtonManager.Instance.click && !ButtonManager.Instance.hold)
        {
            if (GestureManager.Instance.twoFingerHold == true && shieldObject.activeInHierarchy == false)
            {
                shieldState = active;
                shieldObject.SetActive(true);
            }
            else if (GestureManager.Instance.twoFingerHold == false && shieldObject.activeInHierarchy == true)
            {
                shieldState = !active;
                shieldObject.SetActive(false);
            }
        }
        
        

    }
}
