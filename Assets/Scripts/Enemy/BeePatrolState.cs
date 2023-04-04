using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeePatrolState : BaseState
{
    private Vector3 target;

    private Vector3 moveDir;
    
    public override void OnEnter(Enemy enemy)
    {
        // throw new System.NotImplementedException();
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.normalSpeed;
        target = currentEnemy.GetNewPoint();

        // currentEnemy.lostTimeCounter = currentEnemy.lostTime;
    }
    public override void LogicUpdate()
    {
        // throw new System.NotImplementedException();
        if(currentEnemy.FoundPlayer())
            currentEnemy.SwitchState(NPCState.Chase);
        
        if(Mathf.Abs(target.x - currentEnemy.transform.position.x) < 0.1f && Mathf.Abs(target.y - currentEnemy.transform.position.y) < 0.1f)
        {
            currentEnemy.wait = true;
            target = currentEnemy.GetNewPoint();
        }
        moveDir = (target - currentEnemy.transform.position).normalized;
        if(moveDir.x > 0)
        {
            currentEnemy.transform.localScale = new Vector3(-1, 1, 1);
            // Debug.Log("right");
        }
        if(moveDir.x < 0)
        {
            currentEnemy.transform.localScale = new Vector3(1, 1, 1);
            // Debug.Log("left");
        }
        
    }
    public override void PhysicsUpdate()
    {
        // throw new System.NotImplementedException();
        if(!currentEnemy.wait && !currentEnemy.isHurt && !currentEnemy.isDead)
            currentEnemy.rb.velocity = moveDir * currentEnemy.currentSpeed * Time.deltaTime;
        else
            currentEnemy.rb.velocity = Vector2.zero;
    }
    public override void OnExit()
    {
        // throw new System.NotImplementedException();
    }
}
