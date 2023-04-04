using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SnailPatrolState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        Debug.Log("Enter the Patrol state");
        currentEnemy.currentSpeed = currentEnemy.normalSpeed;
    }
    public override void LogicUpdate()
    {
        if(currentEnemy.FoundPlayer())
        {
            //TODO: remember add Skill State for Snail
            currentEnemy.SwitchState(NPCState.Skill);
        }
        if (!currentEnemy.physicsCheck.isGround || (currentEnemy.physicsCheck.touchLeftWall && currentEnemy.faceDir.x < 0) || (currentEnemy.physicsCheck.touchRightWall && currentEnemy.faceDir.x > 0))
        {
            currentEnemy.wait = true;
            currentEnemy.anim.SetBool("isWalk", false);
        }
        else
        {
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
    }
}
