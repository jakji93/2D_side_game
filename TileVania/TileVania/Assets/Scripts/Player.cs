using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(100f, 100f);

    Rigidbody2D myRigidBody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider2D;
    BoxCollider2D myFeetCollider2D;

    bool isAlive = true;
    float gravityScale;

    private void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider2D = GetComponent<CapsuleCollider2D>();
        myFeetCollider2D = GetComponent<BoxCollider2D>();
        gravityScale = myRigidBody.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isAlive)
        {
            return;
        }

        PlayerMove();
        FlipSprite();
        PlayerJump();
        PlayerClimb();
        Die();
    }

    private void PlayerMove()
    {
        float deltaX = Input.GetAxisRaw("Horizontal") * Time.deltaTime * moveSpeed;
        Vector2 newPos = new Vector2(deltaX, myRigidBody.velocity.y);
        myRigidBody.velocity = newPos;
        bool playerHasMovementSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("IsRunning", playerHasMovementSpeed);
    }

    private void PlayerClimb()
    {
        if(!myFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            myAnimator.SetBool("IsClimbing", false);
            myRigidBody.gravityScale = gravityScale;
            return;
        }
        float controlThrow = Input.GetAxisRaw("Vertical") * Time.deltaTime * moveSpeed;
        Vector2 climbVelocity = new Vector2(myRigidBody.velocity.x, controlThrow);
        myRigidBody.velocity = climbVelocity;
        myRigidBody.gravityScale = 0f;
        bool playerHasClimbSpeed = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("IsClimbing", playerHasClimbSpeed);
    }

    private void PlayerJump()
    {
        if(!myFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }

        if (Input.GetButtonDown("Jump"))
        {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            myRigidBody.velocity += jumpVelocityToAdd;
        }
    }

    private void Die()
    {
        if(myBodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazard")))
        {
            isAlive = false;
            myAnimator.SetTrigger("IsDead");
            myRigidBody.velocity = deathKick;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }

    private void FlipSprite()
    {
        bool playerHasMovementSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        if(playerHasMovementSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1);
        }
    }
}
