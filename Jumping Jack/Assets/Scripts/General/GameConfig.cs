using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfig : ScriptableObject {
    public int Units = 107;
    public int Lines = 8;
    public float LineDistance = 0.97f;
    public float JumpFailDistance = 0.272f;
    public int InitialLives = 5;
    public int BaseScoreValue = 5;
    public int GapUnits = 10;
    public int MaxGaps = 8;
    public float StunTime = 1;
    public float CrashTime = 0.3f;
    public float HazardInitialYPos = 4.2f;
    public float LeftLimit = -5.55f;
    public float RightLimit = 5.55f;
    public float MinLeftPos = -5.151f;
    public float MinRightPos = 5.151f;
    public string PlayerTag = "Player";
}
