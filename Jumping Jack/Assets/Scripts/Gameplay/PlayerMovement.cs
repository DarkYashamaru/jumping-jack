using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [Header("Movement")]
    public float MoveSpeed = 3;
    public float VerticalSpeed = 4;
    public MovementState CurrentMovement;
    public int CurrentLevel;
    int LastLevel;
    [Header("Setup")]
    public GameConfig Config;
    public LayerMask RaycastMask;
    float initialY;
    float targetY;
    Vector3 targetPos;
    bool insideLimits;
    float afterJumpDelay = 0.2f;
    float currentJumpDelay;
    float currentStunTime;
    public static event System.Action NewLevel;
    public static event System.Action NextLevel;
    public static event System.Action LifeReduced;

    //Debug
    public Color[] RayColors;
    int rayColorIndex;

    //Inputs
    float moveDirection;
    float lastDirectionMoved;
    bool jump;
    bool grounded;

    private void Start()
    {
        initialY = transform.position.y;
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
        jump = false;
        moveDirection = Input.GetAxisRaw("Horizontal");
        jump = Input.GetAxisRaw("Vertical") > 0;
    }

    void MoveCharacter()
    {
        if(CurrentMovement == MovementState.Horizontal)
        {
            CheckFall();
            grounded = CheckGround();
            if (grounded && jump && currentJumpDelay <= 0)
            {
                SetTargetY();
                grounded = false;
                CurrentMovement = MovementState.JumpStart;
                Debug.Log("Jump Start "+Time.time);
            }

            AutomaticMovementInWalls();
            if(grounded)
                transform.Translate(moveDirection * MoveSpeed * Time.deltaTime, 0, 0, Space.World);
            Limits();
            if (Mathf.Abs(moveDirection) > 0)
            {
                lastDirectionMoved = moveDirection;
            }
        }
        if(CurrentMovement == MovementState.JumpStart)
        {
            RaycastHit2D hit = Physics2D.Raycast(RayOrigin(), Vector3.up, Config.LineDistance, RaycastMask);
            Debug.DrawRay(RayOrigin(), Vector3.up * Config.LineDistance, RayColors[Random.Range(0, RayColors.Length)], 5);
            if(hit.collider!= null)
            {
                JumpAndCollide();
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
                SetJumpDelay();
                CurrentLevel++;
                if(CurrentLevel > LastLevel)
                {
                    LastLevel = CurrentLevel;
                    if (NewLevel != null)
                        NewLevel();
                    if(CurrentLevel >= 8)
                    {
                        if (NextLevel != null)
                            NextLevel();
                        Debug.Log("next level ");
                        enabled = false;
                        CurrentMovement = MovementState.None;
                    }
                }
                SetTargetY();
                CurrentMovement = MovementState.Horizontal;
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
        if(CurrentMovement == MovementState.JumpFailUp)
        {
            MoveToTarget();
            if (transform.position == targetPos)
            {
                SetJumpFailYDown();
                CurrentMovement = MovementState.JumpFailDown;
            }
        }
        if(CurrentMovement == MovementState.JumpFailDown)
        {
            MoveToTarget();
            if (transform.position == targetPos)
            {
                Stun();
            }
        }
    }

    void JumpAndCollide ()
    {
        SetJumpFailYUp();
        CurrentMovement = MovementState.JumpFailUp;
    }

    public void Stun()
    {
        CurrentMovement = MovementState.stun;
        currentStunTime = Config.StunTime;
        if(CurrentLevel == 0)
        {
            if (LifeReduced != null)
                LifeReduced();
            Debug.Log("life reduced");
        }
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
        RaycastHit2D hit = Physics2D.Raycast(RayOrigin(), Vector3.down, Config.LineDistance, RaycastMask);
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

    void SetJumpFailYUp ()
    {
        SetTargetY();
        targetY -= (Config.LineDistance - Config.JumpFailDistance);
    }

    void SetJumpFailYDown ()
    {
        targetY = initialY + (CurrentLevel * Config.LineDistance);
    }

    Vector3 RayOrigin ()
    {
        return new Vector3(transform.position.x, initialY + (CurrentLevel * Config.LineDistance), 0);
    }

    public enum MovementState {None, Horizontal, JumpStart, Jumping, JumpFailUp, JumpFailDown, FallingStart, Falling, stun};
}
