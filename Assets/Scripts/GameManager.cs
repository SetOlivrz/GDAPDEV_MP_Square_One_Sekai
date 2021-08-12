using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // [SerializeField] HandheldCameraBehavior camera;
    [SerializeField] GameObject player;
    [SerializeField] EnemySpawner spawner;
    [SerializeField] GameObject soul;

    [SerializeField] ParticleSystem ghost_particle;
    [SerializeField] ParticleSystem bat_particle;
    [SerializeField] ParticleSystem pumpkin_particle;
    [SerializeField] ParticleSystem eyeball_particle;





    GameObject enemyFound;

    public List<GameObject> enemyList;
    private float hp = 100;
    public int gamePhase = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame

    void Update()
    {
        if (hp <= 0 && gamePhase != -1)
        {
            hp = 0;
            Debug.Log("you lose");
            gamePhase = -1;
        }

        else if (gamePhase == 1 && enemyList.Count == 0)
        {
            Debug.Log("boss fight");
            gamePhase = 2;
            enemyBoss = spawner.spawnBoss();
            enemyBoss.GetComponent<BossBehavior>().setTarget(player);

        }

        if (gamePhase == 2)
        {
            if (enemyBoss == null)
            {
                Debug.Log("you win");
                gamePhase = 2;
                enemyBoss = spawner.spawnBoss();
            }
        }
        //if(player.getShootingBool())
        //{
        //    enemyFound = camera.getVisibleTarget();
        //    if(enemyFound != null)
        //    {
        //        enemyFound.GetComponent<Stats>().displayStats();
        //        OnHit(enemyFound);
        //        //spawner.respawnEnemy(target);
        //        //target.SetActive(false);
        //        //camera.removeTarget(target);
        //    }
        //}
        spawnSoul();
    }

    public void OnHit(GameObject enemy)
    {
        // if conditions for enemy ids corresponding damage

    }

    public void addToEnemyList(GameObject enemy)
    {
        Instance.enemyList.Add(enemy);
    }

    public void removeFromEnemyList(GameObject enemy)
    {
        Instance.enemyList.Remove(enemy);
    }

    public void spawnSoul()
    {
        GameObject soulObj;
        GameObject holder;

        for (int i = 0; i < Instance.enemyList.Count; i++)
        {
            if (Instance.enemyList[i] == null)
            {
                Instance.removeFromEnemyList(enemyList[i]);
            }
            else if (Instance.enemyList[i].GetComponent<EnemyBehavior>() == null)
            {
                Transform child;
                child = Instance.enemyList[i].transform.GetChild(0);

                Debug.Log(enemyList[i].transform.childCount);
                Debug.Log(Instance.enemyList[i].name);

                if (child.GetComponentInChildren<EnemyBehavior>().spawnSoul)
                {
                    soulObj = GameObject.Instantiate(soul, child.position, Quaternion.identity, Instance.enemyList[i].transform.parent);
                    soulObj.name = soul.name;
                    soulObj.GetComponent<EnemyBehavior>().IntializeEnemyStats();
                    soulObj.GetComponent<EnemyBehavior>().setTarget(player.gameObject);


                    holder = child.gameObject;

                    Instance.enemyList[i].SetActive(false);
                    Instance.enemyList[i] = soulObj;
                    Destroy(holder);


                }
            }
            else if (Instance.enemyList[i].GetComponent<EnemyBehavior>().spawnSoul)
            {
                Debug.Log("Killed");
                soulObj = GameObject.Instantiate(soul, Instance.enemyList[i].gameObject.transform.position, Quaternion.identity, null);

                soulObj.name = soul.name;
                soulObj.GetComponent<EnemyBehavior>().IntializeEnemyStats();
                soulObj.GetComponent<EnemyBehavior>().setTarget(player.gameObject);
                holder = Instance.enemyList[i];

                Instance.enemyList[i].SetActive(false);
                Instance.enemyList[i] = soulObj;
                Destroy(holder);
            }
        }
    }

    public void takeDamage(float damage)
    {
        hp -= damage;
        Debug.Log("Hp: " + hp);
    }

    public void SpawnDeadParticles(string name)
    {
        switch (name)
        {
            case "Ghost": ghost_particle.Play(); break;
            case "Bat": bat_particle.Play(); break;
            case "Pumpkin": pumpkin_particle.Play(); break;
            case "Eyeball": eyeball_particle.Play(); break;

        }
    }
}