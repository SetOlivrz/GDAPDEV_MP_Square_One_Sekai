using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Device_A : MonoBehaviour
{
    // Start is called before the first frame update

    public float damage = 10f;
    public float range = 100f;

    public float shootRate = 2;
    public float nextShootTime = 0;

    [SerializeField] PlayerController player;
    [SerializeField] Camera cam;

    [SerializeField] private FlashImage flashImage = null;



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.getShootingBool()&& Time.time >1)
        {
            Shoot();
            nextShootTime = Time.time + 1f / shootRate;
        }

        if(player.getHoldingBool())
        {
            holdShoot();
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

            hit.transform.GetComponent<EnemyBehavior>().TakeDamage(10);
            hit.transform.GetComponent<EnemyBehavior>().displayStats();
        }
    }

    public void holdShoot()
    {
        Debug.Log("pew");

        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))

        {
            Debug.Log("wahhhhhhhhhhhhhhhhhhhhhh");

            Debug.Log(hit.transform.name);
            if(hit.transform.GetComponent<EnemyBehavior>().ID == "Bat")
            {
                hit.transform.GetComponent<EnemyBehavior>().TakeDamage(2);
            }
            hit.transform.GetComponent<EnemyBehavior>().displayStats();
        }
    }
}
