using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneCreator : MonoBehaviour {
    public GameConfig Config;
    public GameObject Unit;
    public Transform Parent;
    public Vector2 StartingPoint = new Vector2(-5.3f, 3.85f);
    public float UnitDistance = 0.1f;
    const string LINE_FORMAT = "Line {0}";
    public SceneLines Scene;

    void Awake()
    {
        CreateLevel();
    }

    void CreateLevel()
    {
        Scene = new SceneLines
        {
            Lines = new SceneLine[Config.Lines]
        };

        for (int i = 0; i < Config.Lines; i++)
        {
            GameObject line = new GameObject(string.Format(LINE_FORMAT, i));
            line.transform.SetParent(Parent);
            Scene.Lines[i] = new SceneLine
            {
                Units = new GameObject[Config.Units]
            };

            for (int l = 0; l < Config.Units; l++)
            {
                Scene.Lines[i].Units[l] = Instantiate(Unit, new Vector3(StartingPoint.x + (UnitDistance * l), StartingPoint.y - (Config.LineDistance * i), 0), Quaternion.identity, line.transform);
            }
        }
    }
}

[System.Serializable]
public class SceneLines
{
    public SceneLine[] Lines;

    public void ResetScene ()
    {
        for (int i = 0; i < Lines.Length; i++)
        {
            Lines[i].Reset();
        }
    }

    public void DrawGap(LevelGap levelGap)
    {
        for (int i = 0; i < levelGap.Gap.GapUnits.Length; i++)
        {
            int x = (int)levelGap.Gap.GapUnits[i].x;
            int y = (int)levelGap.Gap.GapUnits[i].y;
            Lines[y].Units[x].SetActive(false);
        }
    }
}

[System.Serializable]
public class SceneLine
{
    public GameObject[] Units;

    public void Reset ()
    {
        for (int i = 0; i < Units.Length; i++)
        {
            Units[i].SetActive(true);
        }
    }
}
