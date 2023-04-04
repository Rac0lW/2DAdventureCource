using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarPatrolState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        // throw new System.NotImplementedException();
        currentEnemy = enemy;
        Debug.Log("Enter the Patrol state");
        currentEnemy.currentSpeed = currentEnemy.normalSpeed;
        // currentEnemy.hurtForce = 4.0f;
    }
    public override void LogicUpdate()
    {
        //TODO: detect player and change state to ChaseState
        if(currentEnemy.FoundPlayer())
        {
            currentEnemy.SwitchState(NPCState.Chase);
        }
        // throw new System.NotImplementedException();
        if (!currentEnemy.physicsCheck.isGround || (currentEnemy.physicsCheck.touchLeftWall && currentEnemy.faceDir.x < 0) || (currentEnemy.physicsCheck.touchRightWall && currentEnemy.faceDir.x > 0))
        {
            currentEnemy.wait = true;
            currentEnemy.anim.SetBool("isWalk", false);
            // currentEnemy.transform.localScale = new Vector3(currentEnemy.faceDir.x, 1, 1);
        }
        else
        {
            // currentEnemy.wait = false;
            // currentEnemy.transform.localScale = new Vector3(-currentEnemy.faceDir.x, 1, 1);
            currentEnemy.anim.SetBool("isWalk", true);
        }
    }
    public override void PhysicsUpdate()
    {
        // throw new System.NotImplementedException();
    }
    public override void OnExit()
    {
        // throw new System.NotImplementedException();
        currentEnemy.anim.SetBool("isWalk", false);
        Debug.Log("Exit from Patrol");
    }
}
