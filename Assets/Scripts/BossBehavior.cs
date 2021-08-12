using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    float attackRate;
    float attackTimer = 2.0f;
    [SerializeField] GameObject projectile;
    GameObject target = null;
    int attackHand = -1;
    // Start is called before the first frame update
    void Start()
    {
        if (target != null)
            this.transform.LookAt(target.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if(attackTimer < attackRate)
        {
            attackTimer += Time.deltaTime;
        }    

        else
        {
            attack();
            attackTimer = 0.0f;
        }
    }

    private void attack()
    {
        if(attackHand == -1)
        {
            GameObject.Instantiate(projectile, this.transform.position + (this.transform.right.normalized * -1), Quaternion.identity, this.transform);
        }

        else if(attackHand == 1)
        {
            GameObject.Instantiate(projectile, this.transform.position+ (this.transform.right.normalized * 1), Quaternion.identity, this.transform);
        }

        attackHand *= -1;
    }

    public void setTarget(GameObject targetObj)
    {
        this.target = targetObj;
    }

    public GameObject getTarget()
    {
        return target;
    }
}
