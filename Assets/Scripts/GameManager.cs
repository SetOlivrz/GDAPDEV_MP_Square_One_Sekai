using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    [SerializeField] ResultsManager resultsManager;
    [SerializeField] GameObject victoryPopup;
    [SerializeField] GameObject defeatPopup;
    [SerializeField] GameObject bossFightPopup;
    GameObject enemyBossInstance;
    GameObject enemyFound;

    public List<GameObject> enemyList;
    public float hp = PlayerData.playerHP;
    public int gamePhase = 0;

    public bool levelComplete = false;
    private bool enemyBossDead = false;
    private bool isResultsDisplayed = false;





    // Start is called before the first frame update
    void Start()
    {
        hp = PlayerData.playerHP;
    }

    // Update is called once per frame

    void Update()
    {
        // if dead -> phase -1
        if (gamePhase == -1)
        {
            if(!isResultsDisplayed)
            {
                resultsManager.updateInfo();
                defeatPopup.transform.parent.gameObject.SetActive(true);
                defeatPopup.SetActive(true);
                isResultsDisplayed = true;
            }
        }

        
        else if (gamePhase == 1) //wave clear phase
        {
            // if 
            if (hp <= 0)
            {
                PlayerLose();
            }

            // if defeat all enemy -> phase 2
            if (enemyList.Count == 0 && spawner.nSpawns >= spawner.getSpawnLocList().Count)
            {
                InitiateBossFight();
            }
            
        }

        // phase 2 -> boss
        else if (gamePhase == 2)
        {
            if (hp <= 0)
            {
                PlayerLose();
            }

            if (levelComplete)
            {
                levelComplete = false;
                gamePhase = 3;
            }
        }


        if (gamePhase == 3)
        {
            if(!isResultsDisplayed)
            {
                resultsManager.updateInfo();
                victoryPopup.transform.parent.gameObject.SetActive(true);
                victoryPopup.SetActive(true);
                isResultsDisplayed = true;
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
        checkForDeadEnemies();
    }

    private void InitiateBossFight()
    {
        Debug.Log("boss fight");
        gamePhase = 2;
        Time.timeScale = 0;
        bossFightPopup.transform.parent.gameObject.SetActive(true);
        bossFightPopup.SetActive(true);
        enemyBossInstance = spawner.spawnBoss();
        enemyBossInstance.GetComponent<BossBehavior>().setTarget(player);
    }

    private void PlayerLose()
    {
        hp = 0;
        Debug.Log("you lose");
        gamePhase = -1;
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

    public void checkForDeadEnemies()
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


                    holder = child.transform.parent.gameObject;

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
                holder = Instance.enemyList[i].transform.parent.gameObject;

                Instance.enemyList[i].SetActive(false);
                Instance.enemyList[i] = soulObj;
                Destroy(holder);
            }
        }

        if (enemyBossInstance != null && enemyBossDead != true)
        {
            //Transform child;
            //child = enemyBossInstance.transform.GetChild(0);


            if (enemyBossInstance.GetComponent<BossBehavior>().spawnSoul)
            {
                soulObj = GameObject.Instantiate(soul, enemyBossInstance.transform.position, Quaternion.identity, null);
                soulObj.name = "Eyeball(Soul)";
                soulObj.GetComponent<EnemyBehavior>().IntializeEnemyStats();
                soulObj.GetComponent<EnemyBehavior>().setTarget(player.gameObject);


                holder = enemyBossInstance.transform.parent.gameObject;
                enemyBossInstance.SetActive(false);
                Destroy(holder);
                enemyBossDead = true;
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