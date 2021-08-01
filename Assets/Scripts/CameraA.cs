using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraA : MonoBehaviour
{
    // Start is called before the first frame update

    public float damage = 10f;
    public float range = 100f;

    [SerializeField] PlayerController player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.getCameraFlashBool())
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        
    }
}
