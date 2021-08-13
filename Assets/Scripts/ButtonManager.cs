using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{

    public static ButtonManager Instance;

    public bool click = false;
    public bool hold = false;

    [SerializeField] GameObject PopupPanel;
    [SerializeField] GameObject PausePanel;

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        click = false;

        if(hold == true)
        {
            //Debug.Log("Holding button");
        }
    }

    public void Capture()
    {
        //Debug.Log("releasing button");
        hold = false;
        click = true;

    }

    public void Hold()
    {
        
        hold = true;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        Debug.Log(" game paused");
        PopupPanel.SetActive(true);
        PausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        Debug.Log(" game paused");
        if(PopupPanel != null)
            PopupPanel.SetActive(false);
        if (PausePanel != null)
            PausePanel.SetActive(false);
    }


    public void QuitToMenu()
    {

        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");

    }


}
