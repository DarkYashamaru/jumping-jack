using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : ScriptableObject {
    public Data[] LevelInfo;

}

[System.Serializable]
public class Data
{
    public bool ExtraLife;
    public Hazard[] Hazards;
}

[System.Serializable]
public class Hazard
{
    public Vector2 Position;
}

