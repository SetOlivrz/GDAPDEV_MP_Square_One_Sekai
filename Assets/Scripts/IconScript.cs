using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class IconScript : MonoBehaviour
{
   

    [SerializeField] Animator animator;


    [SerializeField] Text mode;



    // Start is called before the first frame update
    void Start()
    {
        UpdateIcon();
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void  UpdateIcon()
    {
        animator.SetBool("Ghost", false);
        animator.SetBool("Bat", false);
        animator.SetBool("Pump", false);
        animator.SetBool("Eye", false);

        switch (mode.text)
        {
            case "FLASH MODE": animator.SetBool("Ghost", true); break;
            case "SONIC MODE": animator.SetBool("Bat", true); break;
            case "PUMP MODE": animator.SetBool("Pump", true); break;
            case "TAP MODE": animator.SetBool("Eye", true); break;
        }
    }

    
}
