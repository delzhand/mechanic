using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour {

    [HideInInspector] public bool facingRight = true;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void StartAnimation(string name)
    {
        animator.Play(name);
    }

    public void Flip()
    {
        transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0) + transform.localRotation.eulerAngles);
        facingRight = !facingRight;
    }
}
