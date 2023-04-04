using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerInputControl inputControl;
    public Rigidbody2D rb;
    private PhysicsCheck physicsCheck;
    public Vector2 inputDirection;
    private CapsuleCollider2D coll;

    public PlayerAnimation playerAnimation;

    [Header("Physical Material")]

    public PhysicsMaterial2D normal;

    public PhysicsMaterial2D wall;

    [Header("Base Status")]
    public float speed;
    private float runspeed;
    private float walkspeed => speed / 2.5f;
    
    // public int int_combo;
    public float jumpForce;

    public float hurtForce;
    private Vector2 originalOffset;

    private Vector2 originalSize;

    [Header("Bool Set")]
    public bool isCrouch;
    public bool isHurt;

    public bool isDead;

    public bool isAttack;


    
    private void Awake()
    {
        physicsCheck = GetComponent<PhysicsCheck>();

        inputControl = new PlayerInputControl();

        coll = GetComponent<CapsuleCollider2D>();

        playerAnimation = GetComponent<PlayerAnimation>();


        //init the coll
        originalOffset = coll.offset;
        originalSize = coll.size;

        inputControl.Gameplay.Jump.started += Jump;
        //rb = GetComponent<Rigidbody2D>();
        #region 静步操作
        runspeed = speed;
        //静步
        inputControl.Gameplay.WalkButton.performed += ctx =>{
            if(physicsCheck.isGround){
                speed = walkspeed;
            }
        };

        inputControl.Gameplay.WalkButton.canceled += ctx =>{
            if(physicsCheck.isGround){
                speed = runspeed;
            }
        };
        #endregion 

        //Attack
        inputControl.Gameplay.Attack.started += PlayerAttack;
    }

    

    private void OnEnable()
    {
        inputControl.Enable();
    }

    private void OnDisable()
    {
        inputControl.Disable();
    }

    private void Update()
    {
        inputDirection = inputControl.Gameplay.Move.ReadValue<Vector2>();

        CheckState();
    }

    private void FixedUpdate()
    {
        if(!isHurt && !isAttack)
            Move();
    }

    // private void OnTriggerEnter2D(Collider2D other) {
    //     Debug.Log(other.name);
    // }

    public void Move()
    {   
        if(!isCrouch)
            rb.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime, rb.velocity.y);

        //int faceDir = (int)transform.localScale.x;
        if (inputDirection.x > 0)
            transform.localScale = new Vector3(1, 1, 1);// if u want to flip the player, dont use flipX or u will get a bug on attacking the boar when u face its ass.
        if (inputDirection.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        //player filp
//        transform.localScale = new Vector3(faceDir, 1, 1);
        isCrouch = inputDirection.y<-0.5f && physicsCheck.isGround;
        
        if(isCrouch){
            coll.offset = new Vector2(-0.05f, 0.85f);
            coll.size = new Vector2(0.7f, 1.7f);
        }else{
            coll.size = originalSize;
            coll.offset = originalOffset;
        }

    }

    private void Jump(InputAction.CallbackContext obj)
    {
        // Debug.Log("JUMP");
        if(physicsCheck.isGround)
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    private void PlayerAttack(InputAction.CallbackContext obj)
    {
        if(!physicsCheck.isGround)
            return;
        playerAnimation.PlayerAttack();
        isAttack = true;
        // int_combo++;
        // if(int_combo >= 3)
        //     int_combo = 0;
    }

    public void getHurt(Transform attacker){
        isHurt = true;
        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2((transform.position.x - attacker.position.x), 0).normalized;
        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
    }

    public void PlayerDead()
    {
        isDead = true;
        inputControl.Gameplay.Disable();
    }

    public void CheckState()
    {
        coll.sharedMaterial = physicsCheck.isGround ? normal : wall;
    }
    

    
}
