using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour
{

    public float hp;
    public float maxhp;

    [SerializeField] Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        if (this.gameObject.GetComponent<EnemyBehavior>() ==  null)
        {
            this.hp =  this.gameObject.GetComponent<EnemyBehavior>().HP;
        }
        else
        {
            this.hp = this.gameObject.GetComponent<EnemyBehavior>().HP;

        }
        this.maxhp = hp;
        slider.value = CalculateHP();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.GetComponent<EnemyBehavior>() == null)
        {
            this.hp = this.gameObject.GetComponent<EnemyBehavior>().HP;
        }
        else
        {
            this.hp = this.gameObject.GetComponent<EnemyBehavior>().HP;

        }

        slider.value = CalculateHP();

        if (hp<= 0)
        {
            Destroy(slider);
        }
        if (hp> maxhp)
        {
            hp = maxhp;
        }
    }

 

    private float CalculateHP()
    {
        return hp / maxhp;
    }
}
