using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{

    public struct Stats
    {
        public string ID;
        public int HP;
        public int DEF;
    }

     public Stats stats;
    // Start is called before the first frame update
    void Start()
    {

   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void intializeEnemyStats()
    {
        switch(gameObject.name)
        {
            case "Square(Clone)":
                {
                    stats.ID = "Square";
                    stats.HP = 2;
                    stats.DEF = 0;
                }; break;
        }
    }

    public void displayStats()
    {
        Debug.Log("Name: " + gameObject.name + "\n" + "HP: "+ stats.HP + "DEF: "+ stats.DEF+"\n");
    }
}
