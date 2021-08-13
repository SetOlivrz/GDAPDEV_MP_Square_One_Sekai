using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultsManager : MonoBehaviour
{
    // Start is called before the first frame update
    private int initialSouls;
    private int finalSouls;
    private int totalSoulsCollected;

    private int initialGold;
    private int finalGold;
    private int totalGoldEarned;

    private int initialEnemyKills;
    private int finalEnemyKills;
    private int totalEnemyKills;

    private int initialBossKills;
    private int finalBossKills;
    private int totalBossKills;

    [SerializeField] Text[] UIsoulsCollected;
    [SerializeField] Text[] UIgoldEarned;
    [SerializeField] Text[] UIEnemyKills;
    [SerializeField] Text UIBossKills;
    [SerializeField] Text UIEnemiesRemaining;

    private int totalEnemiesRemaining;

    void Start()
    {
        initialSouls = PlayerData.nCollectedSouls;
        initialGold = PlayerData.gold;
        initialEnemyKills = PlayerData.nEnemyMonstersKilled;
        initialBossKills = PlayerData.nEnemyBossesKilled;

        updateInfo();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateInfo()
    {
        finalSouls = PlayerData.nCollectedSouls;
        finalGold = PlayerData.gold;
        finalEnemyKills = PlayerData.nEnemyMonstersKilled;
        finalBossKills = PlayerData.nEnemyBossesKilled;

        totalSoulsCollected = finalSouls - initialSouls;
        totalGoldEarned = finalGold - initialGold;
        totalEnemyKills = finalEnemyKills - initialEnemyKills;
        totalBossKills = finalBossKills - initialBossKills;

        totalEnemiesRemaining = GameManager.Instance.enemyList.Count;

        UIsoulsCollected[0].text = "Souls Colleceted: " + totalSoulsCollected.ToString();
        UIsoulsCollected[1].text = "Souls Colleceted: " + totalSoulsCollected.ToString();
        UIgoldEarned[0].text = "Gold Earned: " + totalGoldEarned.ToString();
        UIgoldEarned[1].text = "Gold Earned: " + totalGoldEarned.ToString();
        UIEnemyKills[0].text = "Enemies Killed: " + totalEnemyKills.ToString();
        UIEnemyKills[1].text = "Enemies Killed: " + totalEnemyKills.ToString();
        UIBossKills.text = "Bosses Killed: " + totalBossKills.ToString();
        UIEnemiesRemaining.text = "Enemies Remaning: " + totalEnemiesRemaining.ToString();
    }
}
