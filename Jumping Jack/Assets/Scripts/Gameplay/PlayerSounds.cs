using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerSounds : MonoBehaviour {
    AudioSource aSource;
    public AudioClip Damage;
    public AudioClip JumpFail;
    public AudioClip Score;
    public AudioClip Fall;
    public AudioClip Clear;

    private void Awake()
    {
        aSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        PlayerMovement.Damage += DamageSound;
        PlayerMovement.JumpFail += JumpFailSound;
        PlayerMovement.Score += ScoreSound;
        PlayerMovement.Falling += FallingSound;
        LevelManager.NextLevelHazards += ClearSound;
    }

    private void OnDisable()
    {
        PlayerMovement.Damage -= DamageSound;
        PlayerMovement.JumpFail -= JumpFailSound;
        PlayerMovement.Score -= ScoreSound;
        PlayerMovement.Falling -= FallingSound;
        LevelManager.NextLevelHazards -= ClearSound;
    }

    void DamageSound ()
    {
        aSource.PlayOneShot(Damage);
    }

    void JumpFailSound ()
    {
        aSource.PlayOneShot(JumpFail);
    }

    void ScoreSound ()
    {
        aSource.PlayOneShot(Score);
    }

    void FallingSound (bool state)
    {
        if(!state)
            aSource.PlayOneShot(Fall);
    }

    void ClearSound (int obj)
    {
        aSource.PlayOneShot(Clear);
    }
}
