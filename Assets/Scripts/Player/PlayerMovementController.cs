using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : PhysicsObject
{
    public float maxWalkSpeed = 7;

    [Range(1, 10)] public float timeToApex = 1f;
    [Range(1, 25)] public float maxJumpHeight = 4;
    [Range(1, 10)] public float lowGravityModifier = 2;
    [Range(1, 10)] public float fallingGravityModifier = 2.5f;
    private float initialJumpVelocity = 0;
    private float gravity = 0;

    private float baseGravityModifier;

    private PlayerAnimationController animator;

    // Use this for initialization
    void Awake()
    {
      animator = GetComponent<PlayerAnimationController>();
      baseGravityModifier = gravityModifier;
    }

    protected override void ComputeVelocity()
    {
        gravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
        baseGravityModifier = gravity / Physics2D.gravity.y;

        if (grounded)
        {
            gravityModifier = baseGravityModifier;
        }

        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = initialJumpVelocity;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            gravityModifier = lowGravityModifier;

            if (velocity.y > 0)
            {
                velocity.y = velocity.y * 0.5f;
                gravityModifier = fallingGravityModifier;
            }
        }

        if (move.x > 0.01f)
        {
            if (!animator.facingRight)
            {
                animator.Flip();
            }
        }
        else if (move.x < -0.01f)
        {
            if (animator.facingRight)
            {
                animator.Flip();
            }
        }

        if (grounded && Mathf.Abs(move.x) > .01f)
        {
            animator.StartAnimation("run");
        }
        else if (!grounded && velocity.y < 0)
        {
            animator.StartAnimation("aimup");
        }
        else if (!grounded && velocity.y > 0)
        {
            animator.StartAnimation("aimup");
        }
        else
        {
            animator.StartAnimation("idle");
        }

        targetVelocity = move * maxWalkSpeed;
    }
}