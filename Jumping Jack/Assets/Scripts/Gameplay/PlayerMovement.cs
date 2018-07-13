using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float MoveSpeed = 3;
    float moveDirection;
    public float LeftLimit = -5.55f;
    public float RightLimit = 5.55f;
    public float MinLeftPos = -5.151f;
    public float MinRightPos = 5.151f;
    float lastDirectionMoved;

    void Update()
    {
        GetInputs();
        MoveCharacter();
    }

    void GetInputs()
    {
        moveDirection = Input.GetAxisRaw("Horizontal");
    }

    void MoveCharacter()
    {
        AutomaticMovementInWalls();
        transform.Translate(moveDirection * MoveSpeed * Time.deltaTime, 0, 0, Space.World);
        Limits();
        if(Mathf.Abs(moveDirection) > 0)
        {
            lastDirectionMoved = moveDirection;
        }
    }

    /// <summary>
    /// Used to automatically move the character when the player stops outside of the screen
    /// </summary>
    void AutomaticMovementInWalls ()
    {
        if (Mathf.Approximately(moveDirection, 0)) // the character is not moving
        {
            bool inLeftCorner = transform.position.x < MinLeftPos && lastDirectionMoved < 0; // the character started moving to the left wall and stoped in the middle
            bool inRightCorner = transform.position.x > MinRightPos && lastDirectionMoved > 0; // the character started moving to the right wall and stoped in the middle
            bool standingInRightWall = transform.position.x > MinRightPos && lastDirectionMoved < 0;// the character is standing outside of right screen
            bool standingInLeftWall = transform.position.x < MinLeftPos && lastDirectionMoved > 0;// the character is standing outside of left screen
            if (inLeftCorner || inRightCorner || standingInRightWall || standingInLeftWall)
            {
                moveDirection = lastDirectionMoved;
            }
        }
    }

    void Limits ()
    {
        if (transform.position.x < LeftLimit)
        {
            transform.position = new Vector3(RightLimit, transform.position.y, 0);
        }
        if (transform.position.x > RightLimit)
        {
            transform.position = new Vector3(LeftLimit, transform.position.y, 0);
        }
    }
}
