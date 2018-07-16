using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour {
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
    }

    void OnDisable()
    {
        PlayerMovement.StunState -= StunAnimation;
        PlayerMovement.Jump -= JumpAnimation;
        PlayerMovement.Falling -= FallingAnimation;
    }

    void StunAnimation(bool stunState)
    {
        if (stunState)
            Debug.Log("Stun start!");
        else
            Debug.Log("End stun!");
    }

    void JumpAnimation (bool jumpState)
    {
        if (jumpState)
            Debug.Log("jump start!");
        else
            Debug.Log("End jump!");
    }

    void FallingAnimation(bool fallState)
    {
        if (fallState)
            Debug.Log("fall start!");
        else
            Debug.Log("End fall!");
    }
}
