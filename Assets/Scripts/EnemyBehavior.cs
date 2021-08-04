using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{

    [SerializeField] GameObject player;
    [SerializeField] GameObject soul;

    float idleTime;
    float timer = 0.0f;
    private GameObject target;
    private Vector3 directionToTarget;
    private float moveSpeed = 50;

  
    // Start is called before the first frame update
    void Start()
    {
        idleTime = Random.RandomRange(4f, 7.5f);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(player.transform);
        //if (timer < idleTime)
        //{
        //    timer += Time.deltaTime;
        //}

        //else if(target != null)
        //{
        //    if (Vector3.Distance(gameObject.transform.position, target.transform.position) > 1)
        //        gameObject.transform.position += directionToTarget * Time.deltaTime * moveSpeed;

        //    else GameObject.Destroy(this.gameObject);

        //}
    }

    public void setTarget(GameObject target)
    {
        this.target = target;
        directionToTarget = (target.transform.position - gameObject.transform.position).normalized;
        Debug.DrawRay(gameObject.transform.position, directionToTarget, Color.black, 20);
    }


    //-------------------------------------------------------------------------------------------------------------------------//

    public string ID;
    public int HP;
    public int DEF;
    public Animator animator;
    private bool isDead = false;
    GameObject Soul;


    public void IntializeEnemyStats()
    {
        switch (gameObject.name)
        {
            case "Square":
                {
                    HP = 3;
                    DEF = 0;
                    ID = "Square :>";
                }; break;

            case "Square(Clone)":
                {
                    HP = 3;
                    DEF = 0;
                    ID = "Square :>";
                }; break;

            case "Bat":
                {
                    HP = 4;
                    DEF = 0;
                    ID = "Bat";
                }; break;

            case "Pumpkin":
                {
                    HP = 4;
                    DEF = 0;
                    ID = "Pumpkin";
                }; break;

            case "Soul":
                {
                    HP = 1;
                    DEF = 0;
                    ID = "Soul";
                }; break;
        }
    }

    public void TakeDamage(int amount)
    {
        if (this.gameObject.name == "Soul")
        {
            // remove instantly if soul
            Destroy(this.gameObject);
        }
        else
        {
            if (isDead == false)
            {
                animator.SetTrigger("takeDamage");

                if (amount - DEF > 0)
                {
                    this.HP -= (amount - DEF);
                }

                if (this.HP <= 0)
                {
                    animator.SetBool("isDead", true);
                    isDead = true;
                }
            }
        }
        
    }
    public void DisplayStats()
    {
        Debug.Log("Name: " + gameObject.name + "\n" + "HP: " + HP + "DEF: " + DEF + "\n");
    }

    public void SpawnSoul()
    {
        if (this.gameObject.name != "Soul")
        {
            Soul = GameObject.Instantiate(soul, this.transform.parent.position, Quaternion.identity);
            Soul.name = soul.name;
            Soul.transform.LookAt(player.transform);

        }
        Destroy(this.gameObject);
    }

   
    
}
