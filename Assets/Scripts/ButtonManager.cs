using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonManager : MonoBehaviour
{

    
    
    public bool click = false;
    public bool hold = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        click = false;

        if(hold == true)
        {
            //Debug.Log("Holding button");
        }
    }

    public void Capture()
    {
        //Debug.Log("releasing button");
        hold = false;
        click = true;

    }

    public void Hold()
    {
        
        hold = true;
    }


}
