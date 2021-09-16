using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> enemySpawnLocList;
    [SerializeField] List<GameObject> enemySpawnGround;
    [SerializeField] GameObject[] enemyTemplates;
    [SerializeField] GameObject bossTemplate = null;

    [SerializeField] GameObject player;

    private int numEnemy = 0; // enemy alive
    public int totalSpawn = 0;  // number of totalSpawn
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
        StartCoroutine(SpawnDelay(spawnTimeDelay, totalSpawn, enemySpawnTemplateIndex));
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

    private IEnumerator SpawnDelay(float waitTime, int spawnLocIndex, int enemyIndex)
    {
        yield return new WaitForSeconds(waitTime);
        GameObject enemySpawn;
        if (enemyTemplates[enemyIndex].name == "Pumpkin(parent)" && enemySpawnGround.Count != 0)
        {
        
            spawnLocIndex = Random.Range(0, enemySpawnGround.Count-1);
            enemySpawn = GameObject.Instantiate(enemyTemplates[enemyIndex], enemySpawnGround[spawnLocIndex].transform.position, Quaternion.identity, null);
            enemySpawnGround.Remove(enemySpawnGround[spawnLocIndex]);
        }
        else
        {
            enemySpawn = GameObject.Instantiate(enemyTemplates[enemyIndex], enemySpawnLocList[spawnLocIndex].transform.position, Quaternion.identity, null);

        }
        enemySpawn.transform.LookAt(player.transform);
        enemySpawn.name = enemyTemplates[enemyIndex].name;

        //if(GameManager.Instance.gamePhase != -1 && GameManager.Instance.gamePhase != 3)
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

        totalSpawn++;

        if (GameManager.Instance.gamePhase == 0)
        {
            GameManager.Instance.gamePhase = 1;

        }

        if (totalSpawn < enemySpawnLocList.Count)
        {
            int enemySpawnTemplateIndex = Random.Range(0, enemyTemplates.Length);

            float spawnTimeDelay = Random.Range(3f, 5f);
            StartCoroutine(SpawnDelay(spawnTimeDelay, totalSpawn, enemySpawnTemplateIndex));
        }

    }

    public GameObject spawnBoss()
    {
        if (bossTemplate != null)
        {
            GameObject bossObj = GameObject.Instantiate(bossTemplate, enemySpawnLocList[0].transform.position, Quaternion.identity, null);
            bossObj.name = bossTemplate.name;
            bossObj = bossObj.transform.GetChild(0).gameObject;
            bossObj.GetComponent<EnemyBehavior>().IntializeBossStats();
            bossObj.GetComponent<EnemyBehavior>().SpawnLocList = enemySpawnLocList;
            return bossObj;
        }


        else return null;
    }
}
