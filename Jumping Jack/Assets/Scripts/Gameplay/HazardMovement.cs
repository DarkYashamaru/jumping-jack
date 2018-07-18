using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardMovement : MonoBehaviour {
    public float MoveSpeed = 3;
    public GameConfig Config;
    public int CurrentY;

    private void Start()
    {
        GetCurrentY();
    }

    private void Update()
    {
        MoveHazard();
        CheckLimits();
    }

    void MoveHazard ()
    {
        transform.Translate(new Vector3(-MoveSpeed,0,0) * Time.deltaTime, Space.World);
    }

    void CheckLimits ()
    {
        if(transform.position.x < Config.LeftLimit)
        {
            CurrentY--;
            if(CurrentY < 0)
            {
                CurrentY = Config.Lines-1;
            }
            float y = Config.HazardInitialYPos - (CurrentY * Config.LineDistance);
            transform.position = new Vector3(Config.RightLimit, y, 0);
        }
    }

    void GetCurrentY ()
    {
        if(Mathf.Approximately(transform.position.y, Config.HazardInitialYPos))
        {
            CurrentY = 0;
        }
        else
        {
            float result = Mathf.Abs(transform.position.y - Config.HazardInitialYPos);
            float div = result / Config.LineDistance;
            CurrentY = Mathf.RoundToInt(div);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(Config.PlayerTag))
        {
            collision.GetComponent<PlayerMovement>().HazardDamage();
        }
    }

}
