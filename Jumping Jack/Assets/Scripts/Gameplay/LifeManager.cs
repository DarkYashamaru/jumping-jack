using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeManager : MonoBehaviour {
    public int CurrentLife;
    public GameConfig Config;
    static LifeManager Instance;
    public static event System.Action<int> LifeUpdated;
    public static event System.Action GameOver;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SetInitialLives();
        RefreshLife();
    }

    private void OnEnable()
    {
        PlayerMovement.LifeReduced += LifeReduced;
        LevelManager.LevelStart += LevelLoaded;
        LevelManager.RestartAll += RestartAll;
    }

    private void OnDisable()
    {
        PlayerMovement.LifeReduced -= LifeReduced;
        LevelManager.LevelStart -= LevelLoaded;
    }

    private void LifeReduced()
    {
        CurrentLife--;
        RefreshLife();
        if (CurrentLife <= 0)
        {
            if (GameOver != null)
                GameOver();
        }
    }

    void LevelLoaded (int currentLevel)
    {
        RefreshLife();
    }

    void RefreshLife ()
    {
        if (LifeUpdated != null)
            LifeUpdated(CurrentLife);
    }

    private void RestartAll()
    {
        SetInitialLives();
    }

    void SetInitialLives ()
    {
        CurrentLife = Config.InitialLives;
    }
}
