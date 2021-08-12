using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] Text goldTxt;
    // Start is called before the first frame update
    void Start()
    {
        goldTxt.text = PlayerData.gold.ToString() + "G";
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void startLevel1()
    {
        SceneManager.LoadScene("Level 1");
    }

}
