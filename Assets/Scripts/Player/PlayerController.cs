using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PlayerInputControl inputControl;

    public Rigidbody2D rb;

    public Vector2 inputDirection;

    public float speed;

    private void Awake()
    {
        inputControl = new PlayerInputControl();

        //rb = GetComponent<Rigidbody2D>();
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
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        rb.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime, rb.velocity.y);

        //int faceDir = (int)transform.localScale.x;

        if (inputDirection.x > 0)
            GetComponent<SpriteRenderer>().flipX = false;
        if (inputDirection.x < 0)
            GetComponent<SpriteRenderer>().flipX = true;
        //player filp
//        transform.localScale = new Vector3(faceDir, 1, 1);
        
    }
}