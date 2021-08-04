using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public string ID;
    public int HP;
    public int DEF;


    // Start is called before the first frame update
    void Start()
    {
   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //public void IntializeEnemyStats()
    //{
    //    switch(gameObject.name)
    //    {
    //        case "Square":
    //            {
    //                HP = 3;
    //                DEF = 0;
    //                ID = "Square :>";
    //            }; break;

    //        case "Square(Clone)":
    //            {
    //                HP = 3;
    //                DEF = 0;
    //                ID = "Square :>";
    //            }; break;

    //        case "Bat":
    //            {
    //                HP = 4;
    //                DEF = 0;
    //                ID = "Bat";
    //            }; break;
    //    }
    //}

    //public void TakeDamage(int amount)
    //{
    //    if (amount - DEF > 0)
    //    {
    //        this.HP -= (amount - DEF);
    //    }

    //    if (this.HP <= 0)
    //    {
    //        SpawnSoul();
    //    }

    //}
    //public void displayStats()
    //{
    //    Debug.Log("Name: " + gameObject.name + "\n" + "HP: "+ HP + "DEF: "+ DEF+"\n");
    //}

    //public void SpawnSoul()
    //{
        
    //    Destroy(gameObject);
    //}
}
