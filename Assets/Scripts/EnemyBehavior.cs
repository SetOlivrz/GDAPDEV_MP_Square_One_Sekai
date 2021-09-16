using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject projectile;

  
    
    private float moveSpeed = 50;

    // STATS
    public int TYPE;
    public string ID;
    public float HP;
    public int DEF;

    // GENERAL COMPONENTS
    public Animator animator;
    public bool isDead = false;
    public bool spawnSoul = false;

    private GameObject target;
    private Vector3 directionToTarget;

    // MOB COMPONENT
    float idleTime;
    float timer = 0.0f;

    // BOSS COMPONENT
    [SerializeField] public List<GameObject> SpawnLocList;

    float attackRate = 2.0f;
    float attackTimer = 0.0f;
    int attackHand = -1;

    [SerializeField] ParticleSystem particle;


    // Start is called before the first frame update
    void Start()
    {
        idleTime = Random.Range(8.0f, 12.0f);

        if (PlayerData.currentLevel == 1)
        {
            attackRate = 2.0f;
            idleTime = Random.Range(10.0f, 14.0f);
        }
            

        else if (PlayerData.currentLevel == 2)
        {
            attackRate = 4.0f;
            idleTime = Random.Range(8.0f, 12.0f);
        }

        else if (PlayerData.currentLevel == 3)
        {
            attackRate = 5.5f;
            idleTime = Random.Range(7.0f, 8.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(player.transform);

        
        //
        if (this.TYPE ==1)
        {
            if (timer < idleTime)
            {
               timer += Time.deltaTime;
            }
            else if (this.gameObject.name != "Soul" || this.gameObject.name != "Boss Soul")
            {
                ThrowProjectile();
                timer = 0.0f;
            }
        }
        else if (this.TYPE ==3)
        {
            // FOR BOSS
            if (attackTimer < attackRate)
            {
                attackTimer += Time.deltaTime;
            }
            else
            {
                BossAttack();
                attackTimer = 0.0f;
            }
        }
       

       

        //else if (GameManager.Instance.gamePhase == 1 && this.ID != "" && this.ID != "Soul")
        //{
        //    //Debug.Log(this.gameObject.name + " damages player, ID: " + this.ID);
        //    //GameManager.Instance.takeDamage(5.0f);
        //    ThrowProjectile();
        //    timer = 0.0f;
        //}
    }

    // GENERAL FUNCTION
    public void setTarget(GameObject targetObj)
    {
        this.target = targetObj;
        this.player = targetObj;
    }

    public GameObject getTarget()
    {
        return target;
    }

    // MINIONS
    public void IntializeEnemyStats()
    {
       
        switch (gameObject.name)
        {
            case "Ghost":
                {
                    TYPE = 1;
                    HP = 3;
                    DEF = 0;
                    ID = "Ghost";
                }; break;

            case "Bat":
                {
                    TYPE = 1;
                    HP = 6;
                    DEF = 0;
                    ID = "Bat";
                }; break;

            case "Pumpkin":
                {
                    TYPE = 1;
                    HP = 100;
                    DEF = 0;
                    ID = "Pumpkin";
                }; break;

            case "Eyeball":
                {
                    TYPE = 1;
                    HP = 100;
                    DEF = 0;
                    ID = "Eyeball";
                }; break;

            case "Soul":
                {
                    TYPE = 2;
                    HP = 100;
                    DEF = 0;
                    ID = "Soul";
                }; break;

            case "Ghost(Boss)":
                {
                    TYPE = 3;
                    HP = 100;
                    DEF = 0;
                    ID = "Ghost(Boss)";
                }; break;
        }
    }

    public void TakeDamage(float amount)
    {
        if (isDead == false) // not dead
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
    }

    public void DisplayStats()
    {
        Debug.Log("Name: " + gameObject.name + "\n" + "HP: " + HP + "DEF: " + DEF + "\n");
    }

    public void Die()
    {
        PlayerData.nEnemyMonstersKilled++;
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

    public void deathParticles()
    {
        if (!(particle.gameObject.activeSelf))
        {
            particle.gameObject.SetActive(true);
        }
        particle.Play();

        if (particle.gameObject.activeInHierarchy == true && !(particle.IsAlive()))
        {
            particle.gameObject.SetActive(false);
        }
    }

    public void ThrowProjectile()
    {
        GameObject.Instantiate(projectile, this.transform.position +(this.transform.forward *0.2f), Quaternion.identity, this.transform);
    }

    
    // BOSS
    public void IntializeBossStats()
    {
        switch (gameObject.name)
        {
            case "Eyeball(Boss)":
                {
                    TYPE = 3;
                    HP = 200;
                    DEF = 0;
                    ID = "Eyeball(Boss)";
                }; break;

            case "Ghost(Boss)":
                {
                    TYPE = 3;
                    HP = 100;
                    DEF = 0;
                    ID = "Ghost(Boss)";
                }; break;
        }
    }

    private void BossAttack()
    {
        if (attackHand == -1)
        {
            GameObject.Instantiate(projectile, this.transform.position + (this.transform.right.normalized * -1), Quaternion.identity, this.transform);
        }

        else if (attackHand == 1)
        {
            GameObject.Instantiate(projectile, this.transform.position + (this.transform.right.normalized * 1), Quaternion.identity, this.transform);
        }

        attackHand *= -1;
    }

    public void goToSpawnPoints()
    {

        int index = Random.Range(0, SpawnLocList.Count - 1);

        this.transform.parent.position = SpawnLocList[index].transform.position;
        Debug.Log("move to" + SpawnLocList[index].name);
    }
}
