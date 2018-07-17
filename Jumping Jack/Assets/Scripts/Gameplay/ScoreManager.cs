using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {
    public GameConfig Config;
    public int CurrentScore;
    public int HighScore;
    int currentLevel;
    const string HIGH_SCORE_KEY = "High_Score";
    static ScoreManager Instance;
    public static event System.Action<int> ScoreUpdated;
    public static event System.Action<int> HighScoreUpdated;

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
        LoadHighScore();
    }

    private void OnEnable()
    {
        LevelManager.LevelStart += LevelStart;
        PlayerMovement.Score += IncreaseScore;
    }

    private void OnDisable()
    {
        LevelManager.LevelStart -= LevelStart;
        PlayerMovement.Score -= IncreaseScore;
    }

    private void LevelStart(int currentLevel)
    {
        this.currentLevel = currentLevel;
        RefreshScores();
    }

    private void IncreaseScore()
    {
        CurrentScore += Config.BaseScoreValue * (currentLevel + 1);
        RefreshScores();
    }

    void LoadHighScore ()
    {
        HighScore = PlayerPrefs.GetInt(HIGH_SCORE_KEY, 0);
        RefreshScores();
    }

    void RefreshScores ()
    {
        if (ScoreUpdated != null)
            ScoreUpdated(CurrentScore);

        if (HighScoreUpdated != null)
            HighScoreUpdated(HighScore);
    }


}
