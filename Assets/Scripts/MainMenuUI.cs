using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenuUI : MonoBehaviour
{
    [SerializeField] Text goldTxt = null;
    [SerializeField] Text flashCamTxt = null;
    [SerializeField] Text sonicCamTxt = null;
    [SerializeField] Text pumpCamTxt = null;
    [SerializeField] Text hpTxt = null;
    [SerializeField] Text healTxt = null;
    [SerializeField] GameObject level2Btn = null;
    [SerializeField] GameObject level3Btn = null;

    private bool level2Unlocked = false;
    private bool level3Unlocked = false;
    // Start is called before the first frame update
    void Start()
    {
        goldTxt.text = PlayerData.gold.ToString() + "G";

        if(flashCamTxt != null)
            flashCamTxt.text = "[50G]\nFlash Cam Dmg: " + PlayerData.weapon1DMG.ToString();
        if (sonicCamTxt != null)
            sonicCamTxt.text = "[50G]\nSonic Cam Dmg: " + PlayerData.weapon2DMG.ToString();
        if (pumpCamTxt != null)
            pumpCamTxt.text = "[50G]\nPump Cam Dmg: " + PlayerData.weapon3DMG.ToString();
        if (hpTxt != null)
            hpTxt.text = "[50G]\nStarting HP: " + PlayerData.playerHP.ToString();
        if (healTxt != null)
            healTxt.text = "[50G]\nHeal Percentage: " + PlayerData.healPercentage.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(level2Btn != null)
        {
            if (PlayerData.level2Unlocked && !level2Unlocked)
            {
                level2Btn.GetComponent<Image>().color = new Color(1, 1, 1);
                level2Unlocked = true;
            }
        }

        if (level3Btn != null)
        {
            if (PlayerData.level3Unlocked && !level3Unlocked)
            {
                level3Btn.GetComponent<Image>().color = new Color(1, 1, 1);
                level3Unlocked = true;
            }
        }

        goldTxt.text = PlayerData.gold.ToString() + "G";
    }

    public void addGold()
    {
        PlayerData.gold += 1000;
    }

    public void startLevel1()
    {
        SceneManager.LoadScene("Level 1");
        PlayerData.currentLevel = 1;
    }

    public void startLevel2()
    {
        if (PlayerData.level2Unlocked)
        {
            SceneManager.LoadScene("Level 2");
            PlayerData.currentLevel = 2;
        }
            
    }

    public void startLevel3()
    {
        if(PlayerData.level3Unlocked)
        {
            SceneManager.LoadScene("Level 3");
            PlayerData.currentLevel = 3;
        }
            
    }
    public void upgradeCam1()
    {
        if (PlayerData.gold > 50)
        {
            PlayerData.weapon1DMG *= 1.25f;

            PlayerData.gold -= 50;

            if (flashCamTxt != null)
                flashCamTxt.text = "[50G]\nFlash Cam Dmg: " + PlayerData.weapon1DMG.ToString();

            Debug.Log("Upgraded Cam1");

            AudioManager.Instance.playPurchaseUpgradeSound();
        }
    }



    public void upgradeCam2()
    {
        if (PlayerData.gold >= 50)
        {
            PlayerData.weapon2DMG *= 1.25f;

            PlayerData.gold -= 50;

            if (sonicCamTxt != null)
                sonicCamTxt.text = "[50G]\nSonic Cam Dmg: " + PlayerData.weapon2DMG.ToString();

            Debug.Log("Upgraded Cam2");

            AudioManager.Instance.playPurchaseUpgradeSound();
        }
    }

    public void upgradeCam3()
    {
        if (PlayerData.gold >= 50)
        {
            PlayerData.weapon3DMG *= 1.25f;

            PlayerData.gold -= 50;
            Debug.Log("Upgraded Cam3");

            if (pumpCamTxt != null)
                pumpCamTxt.text = "[50G]\nPump Cam Dmg: " + PlayerData.weapon3DMG.ToString();

            AudioManager.Instance.playPurchaseUpgradeSound();
        }
    }

    public void upgradeHP()
    {
        if (PlayerData.gold >= 50)
        {
            PlayerData.playerHP += 10.0f;

            PlayerData.gold -= 50;

            if (hpTxt != null)
                hpTxt.text = "[50G]\nStarting HP: " + PlayerData.playerHP.ToString();

            Debug.Log("Upgraded HP");

            AudioManager.Instance.playPurchaseUpgradeSound();
        }
            
    }

    public void upgradeHeal()
    {
        if (PlayerData.gold >= 50)
        {
            PlayerData.healPercentage += 0.0125f;

            PlayerData.gold -= 50;
            

            if (healTxt != null)
                healTxt.text = "[50G]\nMax HP % Heal: " + PlayerData.healPercentage.ToString();

            Debug.Log("Upgraded Heal Percentage");

            AudioManager.Instance.playPurchaseUpgradeSound();
        }

    }

    public void unlockAllLevels()
    {
        PlayerData.level2Unlocked = true;
        PlayerData.level3Unlocked = true;
    }
    public void GoToDebugMenu()
    {
        SceneManager.LoadScene("DebugMenu");

    }


}
