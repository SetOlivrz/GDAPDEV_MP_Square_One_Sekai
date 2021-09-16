using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerData
{
    public static int nCollectedSouls = 0;
    public static int nEnemyMonstersKilled = 0;
    public static int nEnemyBossesKilled = 0;
    public static int gold = 100000;
    public static float weapon1DMG = 1.0f; // flash
    public static float weapon2DMG = 2.0f; // sonic
    public static float weapon3DMG = 30.0f; // pump
    public static float tapDMG = 11.0f; // 
    public static float playerHP = 100.0f;
    public static float healPercentage = 0.10f;

    public static bool level2Unlocked = false;
    public static bool level3unlocked = false;
}
