using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelGap
{
    public Direction CurrentDirection;
    public Gap Gap;
    GameConfig Config;

    public LevelGap(Direction dir, Vector2 initPos, GameConfig config)
    {
        Config = config;
        CurrentDirection = dir;
        Gap = new Gap(config, initPos);
    }

    public void Move()
    {
        switch (CurrentDirection)
        {
            case Direction.Left:
                Gap.Return();
                break;
            case Direction.Right:
                Gap.MoveForward();
                break;
        }
    }

}

[System.Serializable]
public class Gap
{
    public Vector2[] GapUnits;
    GameConfig Config;

    public Gap(GameConfig config, Vector2 initPos)
    {
        Config = config;
        GapUnits = new Vector2[config.GapUnits];

        for (int i = 0; i < config.GapUnits; i++)
        {
            GapUnits[i].x = initPos.x + i;
            GapUnits[i].y = initPos.y;
        }
    }

    public void MoveForward ()
    {
        for (int i = 0; i < GapUnits.Length; i++)
        {
            GapUnits[i].x++;
            if (GapUnits[i].x > Config.Units - 1)
            {
                GapUnits[i].x = 0;
                GapUnits[i].y++;
                if (GapUnits[i].y > Config.Lines - 1)
                {
                    GapUnits[i].y = 0;
                }
            }
        }
    }

    public void Return()
    {
        for (int i = 0; i < GapUnits.Length; i++)
        {
            GapUnits[i].x--;
            if(GapUnits[i].x < 0)
            {
                GapUnits[i].x = Config.Units - 1;
                GapUnits[i].y--;
                if(GapUnits[i].y < 0)
                {
                    GapUnits[i].y = Config.Lines - 1;
                }
            }
        }
    }
}

public enum Direction { Left, Right };
