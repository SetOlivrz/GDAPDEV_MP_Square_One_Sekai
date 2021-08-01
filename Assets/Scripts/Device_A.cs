using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Device_A : MonoBehaviour
{
    // Start is called before the first frame update

    public float damage = 10f;
    public float range = 100f;

    [SerializeField] PlayerController player;
    [SerializeField] Camera cam;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.getCameraFlashBool())
        {
            Debug.Log("shamana");

            Shoot();
        }
    }

    public void Shoot()
    {
        Debug.Log("pew");

        RaycastHit hit;
       if( Physics.Raycast(cam.transform.position,cam.transform.forward, out hit, range))

        {
            Debug.Log("wahhhhhhhhhhhhhhhhhhhhhh");

            Debug.Log(hit.transform.name);

            hit.transform.GetComponent<Stats>().TakeDamage(1);
            hit.transform.GetComponent<Stats>().displayStats();
        }
    }


}
