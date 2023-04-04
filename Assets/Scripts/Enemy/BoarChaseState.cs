using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarChaseState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        // throw new System.NotImplementedException();
        currentEnemy = enemy;
        Debug.Log("Enter the Chase state");
        currentEnemy.currentSpeed = currentEnemy.chaseSpeed;
        currentEnemy.anim.SetBool("isRun", true);
    }

    public override void LogicUpdate()
    {   
        // detect lostTimeCounter and change state to PatrolState
        if(currentEnemy.lostTimeCounter <= 0)
            currentEnemy.SwitchState(NPCState.Patrol);

        //  Running without Waiting
        if (!currentEnemy.physicsCheck.isGround || (currentEnemy.physicsCheck.touchLeftWall && currentEnemy.faceDir.x < 0) || (currentEnemy.physicsCheck.touchRightWall && currentEnemy.faceDir.x > 0))
        {
            currentEnemy.transform.localScale = new Vector3(currentEnemy.faceDir.x, 1, 1);
        }
    }

    public override void PhysicsUpdate()
    {
        // throw new System.NotImplementedException();
    }

    public override void OnExit()
    {
        // throw new System.NotImplementedException();
        currentEnemy.lostTimeCounter = currentEnemy.lostTime;
        currentEnemy.anim.SetBool("isRun", false);
    }

    
}
