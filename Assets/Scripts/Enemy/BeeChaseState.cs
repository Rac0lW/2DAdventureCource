using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeChaseState : BaseState
{
    private Attack attack;
    private Vector3 target;

    private Vector3 moveDir;

    private bool isAttack = false;

    private float attackTimeCounter;
    public override void OnEnter(Enemy enemy)
    {
        // throw new System.NotImplementedException();
        currentEnemy = enemy;
        currentEnemy.currentSpeed = currentEnemy.chaseSpeed;
        attack = enemy.GetComponent<Attack>();

        currentEnemy.lostTimeCounter = currentEnemy.lostTime;

        currentEnemy.anim.SetBool("isChase", true);
    }
    public override void LogicUpdate()
    {
        // throw new System.NotImplementedException();
        if(currentEnemy.lostTimeCounter <= 0)
        {
            isAttack = false;
            currentEnemy.SwitchState(NPCState.Patrol);
            
        }
        
        target = new Vector3(currentEnemy.attacker.position.x, currentEnemy.attacker.position.y + 1.5f, 0);

        //judge the attack range
        if(Mathf.Abs(target.x - currentEnemy.transform.position.x) < attack.attackRange && Mathf.Abs(target.y - currentEnemy.transform.position.y) < attack.attackRange)
        {
            isAttack = true;
            if(!currentEnemy.isHurt && !currentEnemy.isDead)
                currentEnemy.rb.velocity = Vector2.zero;
            // currentEnemy.SwitchState(NPCState.Attack);
            attackTimeCounter -= Time.deltaTime;
            if(attackTimeCounter <= 0)
            {
                attackTimeCounter = attack.attackRate;
                currentEnemy.anim.SetTrigger("Attack");
            }
            
        }
        else
        {
            isAttack = false;
            
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
        if(!currentEnemy.isHurt && !currentEnemy.isDead && !isAttack)
            currentEnemy.rb.velocity = moveDir * currentEnemy.currentSpeed * Time.deltaTime;
        

        
    }
    public override void OnExit()
    {
        // throw new System.NotImplementedException();
        currentEnemy.anim.SetBool("isChase", false);

    }
}
