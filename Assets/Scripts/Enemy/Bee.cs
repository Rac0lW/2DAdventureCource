using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : Enemy
{
    [Header("patrolRadius")]
    public float patrolRadius;

    protected override void Awake() 
    {
        base.Awake();
        patrolState = new BeePatrolState();
        chaseState = new BeeChaseState();
            
    }
    public override bool FoundPlayer()
    {
        var obj = Physics2D.OverlapCircle(transform.position, checkDistance, attackLayer);
        if(obj)
        {
            attacker = obj.transform;
        }
        return obj;
    }

    public override void OnDrawGizmosSelected()
    {
        // base.OnDrawGizmosSelected();
        Gizmos.DrawWireSphere(transform.position, checkDistance);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, patrolRadius);
    }

    public override Vector3 GetNewPoint()
    {
        // return base.GetNewPoint();
        var targetX = Random.Range(-patrolRadius, patrolRadius);
        var targetY = Random.Range(-patrolRadius, patrolRadius);

        return spwanPoint + new Vector3(targetX, targetY, 0);
    }

    public override void Move()
    {
        
    }
}
