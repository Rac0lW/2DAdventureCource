using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailSkillState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        // throw new System.NotImplementedException();
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.chaseSpeed;
        currentEnemy.anim.SetBool("isWalk", false);
        currentEnemy.anim.SetBool("isHide", true);
        currentEnemy.anim.SetTrigger("Skill");

        currentEnemy.lostTimeCounter = currentEnemy.lostTime;
        currentEnemy.GetComponent<Character>().invulnerable = true;

        currentEnemy.GetComponent<Character>().invulnerableCounter = currentEnemy.lostTimeCounter;
    }
    public override void LogicUpdate()
    {
        // throw new System.NotImplementedException();
        if(currentEnemy.lostTimeCounter <= 0){
            currentEnemy.SwitchState(NPCState.Patrol);
        }
        currentEnemy.GetComponent<Character>().invulnerableCounter = currentEnemy.lostTimeCounter;
    }
    public override void PhysicsUpdate()
    {
        // throw new System.NotImplementedException();
    }
    public override void OnExit()
    {
        // throw new System.NotImplementedException();
        currentEnemy.anim.SetBool("isHide", false);
        currentEnemy.GetComponent<Character>().invulnerable = false;
    }
}
