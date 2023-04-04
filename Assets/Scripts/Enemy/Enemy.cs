using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(PhysicsCheck))]

public class Enemy : MonoBehaviour
{
    [HideInInspector]public Rigidbody2D rb;

    [HideInInspector]public Animator anim;

    [HideInInspector]public PhysicsCheck physicsCheck;

    [Header("Base Status")]

    public float normalSpeed;
    [HideInInspector]public float currentSpeed;
    public float chaseSpeed;

    public float hurtForce;
    public Vector3 faceDir;

    public Vector3 spwanPoint;

    public Transform attacker;

    [Header("Detecter")]
    public Vector2 centerOffset;

    public Vector2 checkSize;

    public float checkDistance;

    public LayerMask attackLayer;


    [Header("Time Counter")]

    public float waitTime = 2.0f;
    public float waitTimeCounter;
    public bool wait;
    public float lostTime;
    public float lostTimeCounter;


    // Start is called before the first frame update
    [Header("Hurt Status")]
    public bool isHurt;
    public bool isDead;
    private BaseState currentState;
    protected BaseState patrolState;
    protected BaseState chaseState;

    protected BaseState skillState;
    protected virtual void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        physicsCheck = GetComponent<PhysicsCheck>();
        
        currentSpeed = normalSpeed;
        waitTimeCounter = waitTime;
        spwanPoint = transform.position;
    }

    private void OnEnable() 
    {
        currentState = patrolState;
        currentState.OnEnter(this);    
    }
    // Update is called once per frame
    private void Update()
    {
        faceDir = new Vector3(-transform.localScale.x, 0, 0);
        currentState.LogicUpdate();
        TimeCounter();
    }

    private void FixedUpdate() 
    {
        currentState.PhysicsUpdate();
        if(!isHurt && !isDead && !wait)
            Move();
    }

    private void OnDisable() {
        currentState.OnExit();
    }

    public virtual void Move()
    {   if(!anim.GetCurrentAnimatorStateInfo(0).IsName("PreWalk")&&!anim.GetCurrentAnimatorStateInfo(0).IsName("snailRecover"))
            rb.velocity = new Vector2(currentSpeed * faceDir.x * Time.deltaTime, rb.velocity.y);
    }

    /// <summary>
    /// Time Counter

    /// </summary>
    public void TimeCounter()
    {
        if(wait)
        {
            waitTimeCounter -= Time.deltaTime;
            if(waitTimeCounter <= 0)
            {
                // waitTime = Random.Range(1.0f, 3.0f);
                wait = false;
                waitTimeCounter = waitTime;
                transform.localScale = new Vector3(faceDir.x, 1, 1);
                
            }
        }

        if(!FoundPlayer() && lostTimeCounter > 0)
        {
            lostTimeCounter -= Time.deltaTime;
        }
        // else
        // {
        //     lostTimeCounter = lostTime;
        // }
    }

    public virtual bool FoundPlayer()
    {
        return Physics2D.BoxCast(transform.position + (Vector3)centerOffset, checkSize, 0.0f, faceDir, checkDistance, attackLayer);
    }

    public void SwitchState(NPCState state)
    {
        var newState = state switch{
            NPCState.Patrol => patrolState,
            NPCState.Chase => chaseState,
            NPCState.Skill => skillState,
            _ => null
        };
        
        currentState.OnExit();
        currentState = newState;
        currentState.OnEnter(this);
    }

    public virtual Vector3 GetNewPoint()
    {
        return transform.position;
    }

    #region affair action
    public void OnTakeDamage(Transform attackTrans)
    {
        attacker = attackTrans;
        //turnoff
        if(attacker.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if(attacker.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        // anim.SetTrigger("isHurt");

        //taking damage and turn off the enemy
        isHurt = true;
        anim.SetTrigger("Hurt");
        Vector2 dir = new Vector2((transform.position.x - attacker.position.x), 0).normalized;
        rb.velocity = Vector2.zero;
        StartCoroutine(OnHurt(dir));
        
    }

    private IEnumerator OnHurt(Vector2 dir)
    {
        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.5f);
        isHurt = false;
    }

    public void OnDie()
    {
        gameObject.layer = 2;
        anim.SetBool("isDead", true);
        isDead = true;
    }

    public void DestroyAfterAnimation()
    {
        Destroy(this.gameObject);
    }

    #endregion

    public virtual void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + (Vector3)centerOffset + new Vector3(checkDistance*-transform.localScale.x, 0, 0), 0.2f);
    }
        
}
