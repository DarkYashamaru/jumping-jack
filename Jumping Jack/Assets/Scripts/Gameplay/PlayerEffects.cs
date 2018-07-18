using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffects : MonoBehaviour {
    public ParticleSystem HitEffect;
    public GameObject StartEffect;

    private void OnEnable()
    {
        PlayerMovement.JumpFail += CrashEffect;
        PlayerMovement.StunState += StunEffect;
    }

    private void OnDisable()
    {
        PlayerMovement.JumpFail -= CrashEffect;
    }

    void CrashEffect ()
    {
        HitEffect.Play();
    }

    void StunEffect (bool state)
    {
        StartEffect.SetActive(state);
    }
}
