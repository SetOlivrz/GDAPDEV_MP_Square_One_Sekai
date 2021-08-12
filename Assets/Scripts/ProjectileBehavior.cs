using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    GameObject target;
    private float moveSpeed = 30;
    // Start is called before the first frame update
    void Start()
    {
        target = this.transform.parent.gameObject.GetComponent<EnemyBehavior>().getTarget();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += (target.transform.position - this.transform.position).normalized * moveSpeed * Time.deltaTime;
    }

    public void OnCollisionEnter(Collision collision)
    {
        GameObject collisionObj = collision.gameObject;
        while(collisionObj.transform.parent != null)
        {
            collisionObj = collisionObj.transform.parent.gameObject;
        }

        Debug.Log("Collided with: " + collisionObj.name);
        if(collisionObj.name == "Player")
        {
            GameManager.Instance.takeDamage(5.0f);
            Destroy(this.gameObject);
        }
    }
}
