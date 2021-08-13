using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponsManager : MonoBehaviour
{
    // Start is called before the first frame update

    public float damage = 10f;
    public float range = 100f;

    public float shootRate = 1000000.5f;
    public float explodeRate = 1.5f;
    public float nextShootTime = 0;
    public float nextExplodeTime = 0;

    [SerializeField] GameObject weaponsPanel = null;
    private Color filmColor;

    [SerializeField] ButtonManager button;
    [SerializeField] Camera cam;
    [SerializeField] private FlashImage flashImage = null;

    [SerializeField] ParticleSystem flashCam;
    [SerializeField] ParticleSystem sonicVFX;
    [SerializeField] ParticleSystem pumpVFX;

    [SerializeField] GameObject FlashCam;
    [SerializeField] GameObject SonicCam;
    [SerializeField] GameObject PumpCam;

    [SerializeField] Text Mode;

    private enum weaponType
    {
        soulCam = 0,
        ghostCam = 1,
        batCam = 2,
        pumpkinCam = 3
    }

    private weaponType currentWeapon = 0;

    Color newColor = Color.white;
    int flashOpacity = 1;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GestureManager.Instance.onSwipe.isSwiping == true)
        {
            switchWeapon();
        }

        if (currentWeapon == weaponType.soulCam)
        {
            //Capture();
        }
        if (button.click && currentWeapon == weaponType.ghostCam && Time.time >nextShootTime)
        {
            AudioManager.Instance.playFlashCamSound();
            Shoot();
            nextShootTime = Time.time + (1.0f/shootRate);
            flashImage.StartFlash(0.25f, flashOpacity, newColor);
        }

        else if(button.hold && currentWeapon == weaponType.batCam)
        {
            AudioManager.Instance.playSonicCamSound();
            if(!(sonicVFX.gameObject.activeSelf))
            {
                sonicVFX.gameObject.SetActive(true);
            }
                

            holdShoot();
        }

        else if(button.click && currentWeapon == weaponType.pumpkinCam && Time.time > nextExplodeTime)
        {
            AudioManager.Instance.playPumpCamSound();
            if (!(pumpVFX.gameObject.activeSelf))
            {
                pumpVFX.gameObject.SetActive(true);
            }
            pumpVFX.Play();



            explodeShoot();
            nextExplodeTime = Time.time + (1.0f /explodeRate);
        }
        else
        {
            if(sonicVFX.gameObject.activeInHierarchy == true)
            {
                sonicVFX.gameObject.SetActive(false);
            }

            if (pumpVFX.gameObject.activeInHierarchy == true && !(pumpVFX.IsAlive()))
            {
                pumpVFX.gameObject.SetActive(false);
            }
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
            if (hit.transform.TryGetComponent(out EnemyBehavior EB))
            {
                if (EB.ID == "Ghost")
                {
                    Debug.Log("click");

                    Debug.Log(EB.ID);

                    EB.TakeDamage(PlayerData.weapon1DMG);
                    EB.DisplayStats();
                }
            } 
        }
    }

    public void holdShoot()
    {
        //Debug.Log("using batcam");

        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))

        {
            //Debug.Log("wwwwwwwwwwwwwww");

            Debug.Log(hit.transform.name);

            if (hit.transform.TryGetComponent(out EnemyBehavior EB))
            {
                if(EB.ID == "Bat")
                EB.TakeDamage(PlayerData.weapon2DMG);
                EB.DisplayStats();
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

            if(hit.transform.TryGetComponent(out EnemyBehavior EB))
            {
                if (EB.ID == "Pumpkin")
                {
                    EB.TakeDamage(PlayerData.weapon3DMG);
                    EB.DisplayStats();
                }
            }
            
            
        }
    }

    private void switchWeapon()
    {
        currentWeapon += GestureManager.Instance.onSwipe.direction;

        if ((int)currentWeapon == 4)
        {
            currentWeapon = (weaponType)0;
        }

        else if ((int)currentWeapon == -1)
        {
            currentWeapon = (weaponType)3;
        }
        Debug.Log(currentWeapon);

        SwitchCam();
        SwitchFilm();
    }

    private void SwitchCam()
    {
        this.FlashCam.SetActive(false);
        this.SonicCam.SetActive(false);
        this.PumpCam.SetActive(false);

        switch ((int)currentWeapon)
        {
            case 0: Mode.text = ("TAP MODE");
                    break;
            case 1: FlashCam.SetActive(true);
                    Mode.text = ("FLASH MODE"); 
                    break;
            case 2: SonicCam.SetActive(true);
                    Mode.text = ("SONIC MODE");
                    break;
            case 3: PumpCam.SetActive(true); 
                    Mode.text = ("PUMP MODE");
                    break;
        }
    }

    public void SwitchFilm()
    {
        if (weaponsPanel != null)
        {
            switch ((int)currentWeapon)
            {
                case 0: filmColor = Color.white; break;
                case 1: filmColor = Color.red; break;
                case 2: filmColor = Color.green; break;
                case 3: filmColor = Color.blue; break;
            }

            filmColor.a = 1f;
            weaponsPanel.GetComponent<Image>().color = filmColor;
        }


    }
}
