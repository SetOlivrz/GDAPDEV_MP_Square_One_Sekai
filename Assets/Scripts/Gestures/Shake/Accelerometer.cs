//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Accelerometer : MonoBehaviour
//{
//    public float minChange = 1.5f;
//    //[SerializeField]  handler;
    
//    // Start is called before the first frame update
//    void Start()
//    {
        
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        Vector3 accel = Input.acceleration;
//        // Debug.Log("X: "+accel.x +"   Y: "+accel.y+"   Z: "+accel.z);

//        float num = accel.sqrMagnitude;

//        if (num > minChange && handler.hasReset)
//        {
//            Debug.Log("Shake: "+ num );
//            handler.ClearHistoryNotes();
//            handler.hasReset = false;
//        }

//    }
//}
