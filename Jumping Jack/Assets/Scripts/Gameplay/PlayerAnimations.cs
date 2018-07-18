using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour {
    public bool FacingRight;
    Animator Anim;

    private void Awake()
    {
        Anim = GetComponentInChildren<Animator>();
    }

    void OnEnable()
    {
        PlayerMovement.StunState += StunAnimation;
        PlayerMovement.Jump += JumpAnimation;
        PlayerMovement.Falling += FallingAnimation;
        PlayerMovement.Movement += MovementAnimation;
        PlayerMovement.JumpFail += JumpCrash;
    }

    void OnDisable()
    {
        PlayerMovement.StunState -= StunAnimation;
        PlayerMovement.Jump -= JumpAnimation;
        PlayerMovement.Falling -= FallingAnimation;
        PlayerMovement.Movement -= MovementAnimation;
        PlayerMovement.JumpFail -= JumpCrash;
    }

    private void MovementAnimation(float speed)
    {
        Anim.SetFloat("Speed", Mathf.Abs(speed));
        if(speed > 0 && FacingRight)
        {
            Flip();
        }
        if(speed < 0 && !FacingRight)
        {
            Flip();
        }
    }

    void Flip ()
    {
        FacingRight = !FacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void StunAnimation(bool stunState)
    {
        Anim.SetBool("Stun", stunState);
    }

    void JumpAnimation (bool jumpState)
    {
        Anim.SetBool("Jump", jumpState);
    }

    void FallingAnimation(bool fallState)
    {
        Anim.SetBool("Falling", fallState);
    }

    void JumpCrash ()
    {
        Anim.SetTrigger("Crash");
    }
}
