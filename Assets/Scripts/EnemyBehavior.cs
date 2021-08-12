using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{

    [SerializeField] GameObject player;
    [SerializeField] GameObject projectile;
    float idleTime;
    float timer = 0.0f;
    private GameObject target;
    private Vector3 directionToTarget;
    private float moveSpeed = 50;

  
    // Start is called before the first frame update
    void Start()
    {
        idleTime = Random.Range(8.0f, 12.0f);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(player.transform);
        if (timer < idleTime)
        {
           timer += Time.deltaTime;
        }

        else if(GameManager.Instance.gamePhase == 1 && this.ID != "" && this.ID != "Soul")
        {
            //Debug.Log(this.gameObject.name + " damages player, ID: " + this.ID);
            //GameManager.Instance.takeDamage(5.0f);
            ThrowProjectile();
            timer = 0.0f;
        }

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

    public GameObject getTarget()
    {
        return target;
    }
    //-------------------------------------------------------------------------------------------------------------------------//

    public string ID;
    public int HP;
    public int DEF;
    public Animator animator;
    public bool isDead = false;
    public bool spawnSoul = false;


    public void IntializeEnemyStats()
    {
        switch (gameObject.name)
        {
            case "Ghost":
                {
                    HP = 3;
                    DEF = 0;
                    ID = "Ghost";
                }; break;

            case "Bat":
                {
                    HP = 100;
                    DEF = 0;
                    ID = "Bat";
                }; break;

            case "Pumpkin":
                {
                    HP = 100;
                    DEF = 0;
                    ID = "Pumpkin";
                }; break;

            case "Eyeball":
                {
                    HP = 100;
                    DEF = 0;
                    ID = "Eyeball";
                }; break;

            case "Soul":
                {
                    HP = 100;
                    DEF = 0;
                    ID = "Soul";
                }; break;
        }
    }

    public void TakeDamage(int amount)
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
                Die();
            }
        }
        else
        {
            //GetSoul();
        }
     

    }
    public void DisplayStats()
    {
        Debug.Log("Name: " + gameObject.name + "\n" + "HP: " + HP + "DEF: " + DEF + "\n");
    }

    public void Die()
    {
        animator.SetBool("isDead", true);
        isDead = true;
    }

    public void GetSoul()
    {
        Destroy(this.gameObject);
    }
    public void TurnToSoul()
    {
        spawnSoul = true;
    }

    public void ThrowProjectile()
    {
        GameObject.Instantiate(projectile, this.transform);
    }
}
