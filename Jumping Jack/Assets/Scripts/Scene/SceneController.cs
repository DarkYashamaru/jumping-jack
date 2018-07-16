using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SceneCreator))]
public class SceneController : MonoBehaviour {
    SceneCreator creator;
    public float UpdateRate = 0.1f;
    public List<LevelGap> Gaps = new List<LevelGap>();
    int extraGaps;

    private void Awake()
    {
        creator = GetComponent<SceneCreator>();
    }

    private void Start()
    {
        CreateInitialGaps();
    }

    void OnEnable()
    {
        StartCoroutine(CustomUpdate());
        PlayerMovement.NewLevel += CreateRandomGap;
    }

    void OnDisable()
    {
        PlayerMovement.NewLevel -= CreateRandomGap;
    }

    public void CreateInitialGaps()
    {
        LevelGap gap = new LevelGap(Direction.Left, new Vector2(50, 1), creator.Config);
        LevelGap gap1 = new LevelGap(Direction.Right, new Vector2(50, 1), creator.Config);
        Gaps.Add(gap);
        Gaps.Add(gap1);
    }

    [ContextMenu("Create Random Gap")]
    public void CreateRandomGap()
    {
        if(Gaps.Count < creator.Config.MaxGaps)
        {
            int dir = 1;
            if(extraGaps > 3)
            {
                dir = 0;
            }
            int x = Random.Range(0, 80);
            int y = Random.Range(0, creator.Config.Lines);
            Debug.Log(string.Format("Creating gap at X:{0} Y:{1}", x, y));
            LevelGap gap = new LevelGap((Direction)dir, new Vector2(x, y), creator.Config);
            Gaps.Add(gap);
            extraGaps++;
        }
        else
        {
            Debug.Log("Max gaps");
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            CreateRandomGap();
        }
    }

    IEnumerator CustomUpdate()
    {
        while (enabled)
        {
            MoveGaps();
            UpdateScene();
            yield return new WaitForSeconds(UpdateRate);
        }
    }

    void MoveGaps()
    {
        creator.Scene.ResetScene();
        for (int i = 0; i < Gaps.Count; i++)
        {
            Gaps[i].Move();
        }
    }

    void UpdateScene ()
    {
        for (int i = 0; i < Gaps.Count; i++)
        {
            creator.Scene.DrawGap(Gaps[i]);
        }
    }
}
