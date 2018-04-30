using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaperEnemy : PhysicsObject
{
    private float timer = 0;
    public bool facingRight = true;
    [Range(0, 20)] public float jumpPowerH = 5;
    [Range(0, 20)] public float jumpPowerV = 5;

    protected override void ComputeVelocity() { 
        if (grounded)
        {
            timer += Time.deltaTime;
        }

        if (grounded && timer >= 3)
        {
            timer -= 3;
            velocity.y = jumpPowerV;
            grounded = false;
            facingRight = !facingRight;
        }

        if (!grounded)
        {
            targetVelocity.x = (facingRight ? jumpPowerH : -jumpPowerH);
        }
	}

}
