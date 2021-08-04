using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> enemySpawnLocList;
    [SerializeField] GameObject enemyTemplate;
    [SerializeField] GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        //while(enemySpawnList.Count != 0)
        //{
        //    GameObject enemySpawn = GameObject.Instantiate(enemyObj, enemySpawnList[0].transform.position, Quaternion.identity, null);
        //    enemySpawn.transform.LookAt(player.transform);
        //    targetHolder.addTarget(enemySpawn);
        //    enemySpawnList.RemoveAt(0);
        //}

        for(int i = 0; i < enemySpawnLocList.Count; i++)
        {
            float spawnTimeDelay = Random.Range(1f, 10f);
            StartCoroutine(reviveDelay(spawnTimeDelay, i));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public List<GameObject> getSpawnLocList()
    {
        return enemySpawnLocList;
    }
    
    public void respawnEnemy(GameObject deadEnemy)
    {
        for(int i = 0; i < enemySpawnLocList.Count; i++)
        {
            if(deadEnemy.transform.position == enemySpawnLocList[i].transform.position)
            {
                float delayTime = Random.Range(7f, 15f);
                StartCoroutine(reviveDelay(delayTime, i));
            }
        }
    }

    private IEnumerator reviveDelay(float waitTime, int index)
    {
        yield return new WaitForSeconds(waitTime);
        GameObject enemySpawn = GameObject.Instantiate(enemyTemplate, enemySpawnLocList[index].transform.position, Quaternion.identity, null);
        enemySpawn.transform.LookAt(player.transform);
        enemySpawn.name = enemyTemplate.name;
        //enemySpawn.GetComponent<EnemyBehavior>().setTarget(player);

        if (enemySpawn.GetComponent<EnemyBehavior>() == null)
        {
            Transform child;
            child = enemySpawn.transform.GetChild(0);

            child.GetComponent<EnemyBehavior>().IntializeEnemyStats();
            child.GetComponent<EnemyBehavior>().setTarget(player);
        }
        else
        {
            enemySpawn.GetComponent<EnemyBehavior>().IntializeEnemyStats();
            enemySpawn.GetComponent<EnemyBehavior>().setTarget(player);
        }

        GameManager.Instance.addToEnemyList(enemySpawn);


    }
}
