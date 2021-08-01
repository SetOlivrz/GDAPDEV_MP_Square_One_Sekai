using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    // The enemy
    public Transform enemy;
    // How wide is the area considered to be looking at the enemy?
    public float lookWidth = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 enemyVector = enemy.position - transform.position;

        // Check if enemy is in range, and if looking at enemy.
        if (IsVectorInLookWidth(enemyVector))
        {
            Debug.Log("Enemy Detected");
        }


    }

    bool IsVectorInLookWidth(Vector3 vector)
    {
        // Check if line falls within given viewWidth from forward vector.
        return Vector3.Dot(vector.normalized, transform.forward) > (1.0f - lookWidth);

    }

}
