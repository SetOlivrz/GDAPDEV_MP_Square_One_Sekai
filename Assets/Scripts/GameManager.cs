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
    }


    public void OnHit(GameObject enemy)
    {
        // if conditions for enemy ids corresponding damage
   
    }

    public void addToEnemyList(GameObject enemy)
    {
        enemyList.Add(enemy);
    }

    public void removeFromEnemyList(GameObject enemy)
    {
        enemyList.Remove(enemy);
    }
}

