using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    public int StartLevel;
    public int Currentlevel;
    static LevelManager Instance;
    public static event System.Action<int> LevelStart;
    public static event System.Action RestartAll;

    private void Awake()
    {
        if(Instance==null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        if(Instance!=this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SetInitialLevel();
        LevelStartEvent();
    }

    public static LevelManager GetInstance ()
    {
        return Instance;
    }

    private void OnEnable()
    {
        PlayerMovement.NextLevel += NextLevel;
        FadeSceneChanger.LevelFinishedLoading += NewLevelLoaded;
        GameOverUI.RestartPressed += RestartGame;
    }

    private void OnDisable()
    {
        PlayerMovement.NextLevel -= NextLevel;
        FadeSceneChanger.LevelFinishedLoading -= NewLevelLoaded;
    }

    private void NewLevelLoaded()
    {
        LevelStartEvent();
    }

    void LevelStartEvent ()
    {
        if (LevelStart != null)
            LevelStart(Currentlevel);
        Debug.Log("Starting level " + Currentlevel);
    }

    public void NextLevel()
    {
        Instance.Currentlevel++;
        StartCoroutine(RestartLevel());
    }

    IEnumerator RestartLevel ()
    {
        yield return new WaitForSeconds(1);
        FadeSceneChanger.ChangeScene(0);
    }

    private void RestartGame()
    {
        if (RestartAll != null)
            RestartAll();
        SetInitialLevel();
        StartCoroutine(RestartLevel());
    }

    void SetInitialLevel ()
    {
        Currentlevel = StartLevel;
    }
}
