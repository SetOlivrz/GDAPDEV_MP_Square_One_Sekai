using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> enemySpawnLocList;
    [SerializeField] GameObject[] enemyTemplates;
    [SerializeField] GameObject boss = null;

    [SerializeField] GameObject player;

    private int numEnemy = 0;
    int nSpawns = 0;
    private bool isSpawnInitial = false;
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
        int enemySpawnTemplateIndex = Random.Range(0, enemyTemplates.Length);

        float spawnTimeDelay = Random.Range(2.5f, 3.5f);
        StartCoroutine(SpawnDelay(spawnTimeDelay, nSpawns, enemySpawnTemplateIndex));
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
        for (int i = 0; i < enemySpawnLocList.Count; i++)
        {
            if (deadEnemy.transform.position == enemySpawnLocList[i].transform.position)
            {
                float delayTime = Random.Range(7f, 15f);
                StartCoroutine(SpawnDelay(delayTime, i, 0));
            }
        }
    }

    private IEnumerator SpawnDelay(float waitTime, int spawnerIndex, int templateIndex)
    {
        yield return new WaitForSeconds(waitTime);
        GameObject enemySpawn = GameObject.Instantiate(enemyTemplates[templateIndex], enemySpawnLocList[spawnerIndex].transform.position, Quaternion.identity, null);
        enemySpawn.transform.LookAt(player.transform);
        enemySpawn.name = enemyTemplates[templateIndex].name;
        //enemySpawn.GetComponent<EnemyBehavior>().setTarget(player);

        if (enemySpawn.GetComponent<EnemyBehavior>() == null)
        {
            Transform child;
            child = enemySpawn.transform.GetChild(0);

            child.GetComponent<EnemyBehavior>().IntializeEnemyStats();
            child.GetComponent<EnemyBehavior>().setTarget(player);


            enemySpawn = enemySpawn.transform.GetChild(0).gameObject;
        }
        else
        {
            Debug.Log("no child");
            enemySpawn.GetComponent<EnemyBehavior>().IntializeEnemyStats();
            enemySpawn.GetComponent<EnemyBehavior>().setTarget(player);
        }

        GameManager.Instance.addToEnemyList(enemySpawn);

        nSpawns++;

        if (GameManager.Instance.gamePhase == 0)
            GameManager.Instance.gamePhase = 1;

        if (nSpawns < enemySpawnLocList.Count)
        {
            int enemySpawnTemplateIndex = Random.Range(0, enemyTemplates.Length);

            float spawnTimeDelay = Random.Range(3f, 5f);
            StartCoroutine(SpawnDelay(spawnTimeDelay, nSpawns, enemySpawnTemplateIndex));
        }

    }

    public GameObject spawnBoss()
    {
        if (boss != null)
        {
            GameObject bossObj = GameObject.Instantiate(boss, enemySpawnLocList[2].transform.position, Quaternion.identity, null);
            bossObj = bossObj.transform.GetChild(0).gameObject;
            bossObj.GetComponent<BossBehavior>().SpawnLocList = enemySpawnLocList;
            return bossObj;
        }


        else return null;
    }
}
