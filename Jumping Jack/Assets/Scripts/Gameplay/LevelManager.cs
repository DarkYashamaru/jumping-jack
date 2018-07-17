using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    public int StartLevel;
    public int Currentlevel;
    static LevelManager Instance;
    public static event System.Action<int> LevelStart;

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
        Currentlevel = StartLevel;
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
        StartCoroutine(LoadNextLevel());
    }

    IEnumerator LoadNextLevel ()
    {
        Instance.Currentlevel++;
        yield return new WaitForSeconds(1);
        FadeSceneChanger.ChangeScene(0);
    }
}
