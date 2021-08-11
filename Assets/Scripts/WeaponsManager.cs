using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsManager : MonoBehaviour
{
    // Start is called before the first frame update

    public float damage = 10f;
    public float range = 100f;

    public float shootRate = 1000000.5f;
    public float explodeRate = 1.5f;
    public float nextShootTime = 0;
    public float nextExplodeTime = 0;
    
    [SerializeField] ButtonManager button;
    [SerializeField] PlayerController player;
    [SerializeField] Camera cam;
    [SerializeField] private FlashImage flashImage = null;

    Color newColor = Color.white;
    int flashOpacity = 1;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GestureManager.Instance.getCurrentWeapon() == GestureManager.weaponType.soulCam)
        {
            Capture();
        }
        if (button.click && GestureManager.Instance.getCurrentWeapon() == GestureManager.weaponType.ghostCam && Time.time >nextShootTime)
        {
            Shoot();
            nextShootTime = Time.time + (1.0f/shootRate);
            flashImage.StartFlash(0.25f, flashOpacity, newColor);
        }

        else if(button.hold && GestureManager.Instance.getCurrentWeapon() == GestureManager.weaponType.batCam)
        {
            holdShoot();
        }

        else if(button.click && GestureManager.Instance.getCurrentWeapon() == GestureManager.weaponType.pumpkinCam && Time.time > nextExplodeTime)
        {
            explodeShoot();
            nextExplodeTime = Time.time + (1.0f /explodeRate);
        }
    }

    public void Capture()
    {
        Debug.Log("using soulcam");

        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))

        {
            Debug.Log("weewoo");

            Debug.Log(hit.transform.name);
            //if (hit.transform.GetComponent<EnemyBehavior>().ID == "Soul")
            if(hit.transform.name == "Soul")
            {
                Debug.Log("Destroy");
                GameManager.Instance.enemyList.Remove(hit.transform.gameObject);
                Destroy(hit.transform.gameObject);
            }

        }
    }
    public void Shoot()
    {
        Debug.Log("using ghostcam");

        RaycastHit hit;
       if( Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            if (hit.transform.GetComponent<EnemyBehavior>().ID == "Ghost")
            {
                Debug.Log("click");

                Debug.Log(hit.transform.GetComponent<EnemyBehavior>().ID);

                hit.transform.GetComponent<EnemyBehavior>().TakeDamage(1);
                hit.transform.GetComponent<EnemyBehavior>().DisplayStats();
            }
                
        }
    }

    public void holdShoot()
    {
        Debug.Log("using batcam");

        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))

        {
            Debug.Log("wwwwwwwwwwwwwww");

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
        Debug.Log("using pumpkin cam");

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
