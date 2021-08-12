using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    GameObject target;
    private float moveSpeed = 20;
    // Start is called before the first frame update
    void Start()
    {
        if (this.transform.parent.gameObject.TryGetComponent(out EnemyBehavior EB))
        {
            target = EB.getTarget();
        }

        else if (this.transform.parent.gameObject.TryGetComponent(out BossBehavior BB))
        {
            target = BB.getTarget();
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(target.transform);
        gameObject.transform.position += (target.transform.position - this.transform.position).normalized * moveSpeed * Time.deltaTime;
    }

    public void OnCollisionEnter(Collision collision)
    {
        GameObject collisionObj = collision.gameObject;
        while (collisionObj.transform.parent != null)
        {
            collisionObj = collisionObj.transform.parent.gameObject;
        }

        Debug.Log("Collided with: " + collisionObj.name);
        if (collisionObj.name == "Player")
        {
            //GameManager.Instance.takeDamage(5.0f);
            Destroy(this.gameObject);
        }
    }
}
