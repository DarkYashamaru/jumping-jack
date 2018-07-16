using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [Header("Movement")]
    public float MoveSpeed = 3;
    public float VerticalSpeed = 4;
    public MovementState CurrentMovement;
    public int CurrentLevel;
    [Header("Setup")]
    public GameConfig Config;
    float initialY;
    float targetY;
    Vector3 targetPos;
    bool insideLimits;
    float afterJumpDelay = 0.1f;
    float currentJumpDelay;
    float currentStunTime;
    public static event System.Action NewLevel;

    //Inputs
    float moveDirection;
    float lastDirectionMoved;
    bool jump;

    private void Start()
    {
        initialY = transform.position.y;
        SetTargetY();
        SetJumpDelay();
        CurrentMovement = MovementState.Horizontal;
    }

    void Update()
    {
        GetInputs();
        MoveCharacter();
        ReduceDelays();
    }

    void ReduceDelays ()
    {
        currentJumpDelay -= Time.deltaTime;
        currentStunTime -= Time.deltaTime;
    }

    void GetInputs()
    {
        moveDirection = Input.GetAxisRaw("Horizontal");
        jump = Input.GetAxisRaw("Vertical") > 0;
    }

    void MoveCharacter()
    {
        if(CurrentMovement == MovementState.Horizontal)
        {
            CheckFall();

            if (jump && currentJumpDelay <= 0)
            {
                CurrentMovement = MovementState.JumpStart;
            }

            AutomaticMovementInWalls();
            transform.Translate(moveDirection * MoveSpeed * Time.deltaTime, 0, 0, Space.World);
            Limits();
            if (Mathf.Abs(moveDirection) > 0)
            {
                lastDirectionMoved = moveDirection;
            }
        }
        if(CurrentMovement == MovementState.JumpStart)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.up, Config.LineDistance);
            if(hit.collider!=null)
            {
                CurrentMovement = MovementState.Horizontal; 
                //TODO jump and collide
            }
            else
            {
                CurrentMovement = MovementState.Jumping;
            }
        }
        if(CurrentMovement == MovementState.Jumping)
        {
            MoveToTarget();
            if (transform.position == targetPos)
            {
                CurrentMovement = MovementState.Horizontal;
                SetJumpDelay();
                CurrentLevel++;
                if (NewLevel != null)
                    NewLevel();
                SetTargetY();
            }
        }
        if(CurrentMovement == MovementState.FallingStart)
        {
            SetFallY();
            CurrentMovement = MovementState.Falling;
        }
        if(CurrentMovement == MovementState.Falling)
        {
            MoveToTarget();
            if (transform.position == targetPos)
            {
                //TODO crash with floor
                CurrentLevel--;
                SetTargetY();
                Stun();
            }
        }
        if(CurrentMovement == MovementState.stun)
        {
            CheckFall();
            if(currentStunTime <= 0)
            {
                CurrentMovement = MovementState.Horizontal;
            }
        }
    }

    void Stun()
    {
        CurrentMovement = MovementState.stun;
        currentStunTime = Config.StunTime;
    }

    void SetJumpDelay()
    {
        currentJumpDelay = afterJumpDelay;
    }

    void CheckFall ()
    {
        if (!CheckGround() && currentJumpDelay <= 0)
        {
            CurrentMovement = MovementState.FallingStart;
        }
    }

    void MoveToTarget ()
    {
        targetPos = transform.position;
        targetPos.y = targetY;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, VerticalSpeed * Time.deltaTime);
    }

    bool CheckGround ()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.down, Config.LineDistance);
        if (hit.collider != null || !insideLimits)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Used to automatically move the character when the player stops outside of the screen
    /// </summary>
    void AutomaticMovementInWalls ()
    {
        if (Mathf.Approximately(moveDirection, 0)) // the character is not moving
        {
            bool inLeftCorner = transform.position.x < Config.MinLeftPos && lastDirectionMoved < 0; // the character started moving to the left wall and stoped in the middle
            bool inRightCorner = transform.position.x > Config.MinRightPos && lastDirectionMoved > 0; // the character started moving to the right wall and stoped in the middle
            bool standingInRightWall = transform.position.x > Config.MinRightPos && lastDirectionMoved < 0;// the character is standing outside of the right screen
            bool standingInLeftWall = transform.position.x < Config.MinLeftPos && lastDirectionMoved > 0;// the character is standing outside of the left screen
            if (inLeftCorner || inRightCorner || standingInRightWall || standingInLeftWall)
            {
                moveDirection = lastDirectionMoved;
            }
        }
    }

    void Limits ()
    {
        insideLimits = transform.position.x > Config.MinLeftPos && transform.position.x < Config.MinRightPos;
        if (transform.position.x < Config.LeftLimit)
        {
            transform.position = new Vector3(Config.RightLimit, transform.position.y, 0);
        }
        if (transform.position.x > Config.RightLimit)
        {
            transform.position = new Vector3(Config.LeftLimit, transform.position.y, 0);
        }
    }

    void SetTargetY ()
    {
        targetY = initialY + ((CurrentLevel + 1) * Config.LineDistance);
    }

    void SetFallY ()
    {
        targetY = initialY + ((CurrentLevel - 1) * Config.LineDistance);
    }

    public enum MovementState {None, Horizontal, JumpStart, Jumping, FallingStart, Falling, stun};
}
