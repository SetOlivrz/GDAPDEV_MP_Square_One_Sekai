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
    [SerializeField] PlayerController player;
    [SerializeField] EnemySpawner spawner;
    [SerializeField] GameObject soul;

    GameObject enemyFound;

    public List<GameObject> enemyList;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
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
        //Debug.Log(enemyList.Count);
        for (int i = 0; i < Instance.enemyList.Count; i++)
        {
            if (Instance.enemyList[i].GetComponent<EnemyBehavior>().isDead)
            {
                Debug.Log("Killed");
                GameObject soulObj;

                if (Instance.enemyList[i].transform.parent != null)
                    soulObj = GameObject.Instantiate(soul, Instance.enemyList[i].transform.parent.gameObject.transform.position, Quaternion.identity, Instance.enemyList[i].transform.parent);

                else
                    soulObj = GameObject.Instantiate(soul, Instance.enemyList[i].gameObject.transform.position, Quaternion.identity, null);

                soulObj.name = soul.name;
                soul.GetComponent<EnemyBehavior>().IntializeEnemyStats();
                soul.GetComponent<EnemyBehavior>().setTarget(player.gameObject);
                Instance.enemyList[i].SetActive(false);
                Instance.enemyList[i] = soul;
            }
        }
    }
}