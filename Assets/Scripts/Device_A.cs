using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Device_A : MonoBehaviour
{
    // Start is called before the first frame update

    public float damage = 10f;
    public float range = 100f;

    public float shootRate = 0.1f;
    public float explodeRate = 2;
    public float nextShootTime = 0;
    public float nextExplodeTime = 0;

    [SerializeField] PlayerController player;
    [SerializeField] Camera cam;

    [SerializeField] private FlashImage flashImage = null;



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.getShootingBool()&& Time.time >nextShootTime)
        {
            Shoot();
            nextShootTime = Time.time + shootRate;
        }

        else if(player.getHoldingBool())
        {
            holdShoot();
        }

        else if(player.getExplosionBool() && Time.time > nextExplodeTime)
        {
            explodeShoot();
            nextExplodeTime = Time.time + explodeRate;
        }
    }

    public void Shoot()
    {
        Debug.Log("pew");

        RaycastHit hit;
       if( Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            if (hit.transform.GetComponent<EnemyBehavior>().ID == "Ghost")
            {
                Debug.Log("wahhhhhhhhhhhhhhhhhhhhhh");

                Debug.Log(hit.transform.GetComponent<EnemyBehavior>().ID);

                hit.transform.GetComponent<EnemyBehavior>().TakeDamage(1);
                hit.transform.GetComponent<EnemyBehavior>().DisplayStats();
            }
                
        }
    }

    public void holdShoot()
    {
        Debug.Log("wwwwwwww");

        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))

        {
            Debug.Log("hnggggggggggggggg");

            Debug.Log(hit.transform.name);
            if (hit.transform.GetComponent<EnemyBehavior>().ID == "Bat")
            {
                hit.transform.GetComponent<EnemyBehavior>().TakeDamage(2);
                hit.transform.GetComponent<EnemyBehavior>().DisplayStats();
            }
            
        }
    }

    public void explodeShoot()
    {
        Debug.Log("BANG");

        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))

        {
            Debug.Log("KACHOW");

            Debug.Log(hit.transform.name);
            if (hit.transform.GetComponent<EnemyBehavior>().ID == "Pumpkin")
            {
                hit.transform.GetComponent<EnemyBehavior>().TakeDamage(30);
            }
            hit.transform.GetComponent<EnemyBehavior>().DisplayStats();
        }
    }

}
