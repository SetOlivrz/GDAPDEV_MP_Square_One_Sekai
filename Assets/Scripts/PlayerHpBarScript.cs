using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpBarScript : MonoBehaviour
{
    [SerializeField] Image HPBar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.HPBar.fillAmount = GameManager.Instance.hp / PlayerData.playerHP;
    }
}
