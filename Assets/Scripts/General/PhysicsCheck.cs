using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    private CapsuleCollider2D coll;

    [Header("Collision Check")]
    public bool manual;
    public Vector2 bottomOffset;

    public Vector2 leftOffset;

    public Vector2 rightOffset;
    public float checkRadius;

    public LayerMask groundLayer;

    [Header("Collision Status")]
    public bool isGround;

    public bool touchLeftWall;

    public bool touchRightWall;


    private void Awake() {
        coll = GetComponent<CapsuleCollider2D>();

        if (!manual) 
        {
            rightOffset = new Vector2((coll.bounds.size.x + coll.offset.x) / 2, coll.bounds.size.y / 2);
            leftOffset = new Vector2(-(coll.bounds.size.x + coll.offset.x) / 2, coll.bounds.size.y / 2);

        }

    }

    private void Update() {
        Check();
    }

    private void Check(){


        //check ground
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset * transform.localScale.x, checkRadius, groundLayer);

        //check left wall
        touchLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, checkRadius, groundLayer);

        //check right wall
        touchRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, checkRadius, groundLayer);
    }

    //draw the gizmo line
    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset * transform.localScale.x, checkRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, checkRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, checkRadius);
    }
}
