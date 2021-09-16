using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Accelerometer : MonoBehaviour
{
    public float minChange = 1.5f;
    public bool cooldown = false;
    public float percentHeal = 0.10f;
    public int cooldownTime = 3;
    float ticks = 0.0f;
    public int maxHeal = 3;
    public int healUsed = 0;

    [SerializeField] GameObject icon1;
    [SerializeField] GameObject icon2;
    [SerializeField] GameObject icon3;

    [SerializeField] Color color;
    [SerializeField] Color CD;
    [SerializeField] Color defaultColor;




    //[SerializeField]  handler;

    // Start is called before the first frame update
    void Start()
    {
        color = new Color(1.0f, 1.0f, 1.0f, 0.3f);
        defaultColor = new Color(1.0f, 1.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 accel = Input.acceleration;
        // Debug.Log("X: "+accel.x +"   Y: "+accel.y+"   Z: "+accel.z);

        if (cooldown == true)
        {
            ticks += Time.deltaTime;

            if (ticks >= cooldownTime)
            {
                cooldown = false;
                ticks = 0;

                switch (healUsed)
                {
                    case 1: icon2.GetComponent<Image>().color = icon3.GetComponent<Image>().color = defaultColor; break;
                    case 2: icon3.GetComponent<Image>().color = defaultColor; break;
                }
            }
        }

        float num = accel.sqrMagnitude;
        if (num > minChange && cooldown == false && healUsed < maxHeal)
        {
            cooldown = true;
            Heal();
        }
    }
    public void Heal()
    {
        GameManager.Instance.hp += (PlayerData.playerHP * 0.10f);
        if (GameManager.Instance.hp > PlayerData.playerHP)
        {
            GameManager.Instance.hp = PlayerData.playerHP;
        }
        healUsed++;

        UpdateIcon();

    }

    public void UpdateIcon()
    {
        icon1.GetComponent<Image>().color = color;
        icon2.GetComponent<Image>().color = color;
        icon3.GetComponent<Image>().color = color;
    }
    
}
