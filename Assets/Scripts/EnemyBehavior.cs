using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    float idleTime;
    float timer = 0.0f;
    private GameObject target;
    private Vector3 directionToTarget;
    private float moveSpeed = 50;

  
    // Start is called before the first frame update
    void Start()
    {
        idleTime = Random.RandomRange(4f, 7.5f);
    }

    // Update is called once per frame
    void Update()
    {
        //if (timer < idleTime)
        //{
        //    timer += Time.deltaTime;
        //}

        //else if(target != null)
        //{
        //    if (Vector3.Distance(gameObject.transform.position, target.transform.position) > 1)
        //        gameObject.transform.position += directionToTarget * Time.deltaTime * moveSpeed;

        //    else GameObject.Destroy(this.gameObject);

        //}
    }

    public void setTarget(GameObject target)
    {
        this.target = target;
        directionToTarget = (target.transform.position - gameObject.transform.position).normalized;
        Debug.DrawRay(gameObject.transform.position, directionToTarget, Color.black, 20);
    }    
}
