using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour {
    public Text Score;
    public Text HScore;

    private void OnEnable()
    {
        ScoreManager.ScoreUpdated += UpdateScore;
        ScoreManager.HighScoreUpdated += UpdateHScore;
    }

    private void OnDisable()
    {
        ScoreManager.ScoreUpdated -= UpdateScore;
        ScoreManager.HighScoreUpdated -= UpdateHScore;
    }

    private void UpdateScore(int newScore)
    {
        Score.text = newScore.ToString();
    }

    void UpdateHScore (int newHScore)
    {
        HScore.text = newHScore.ToString();
    }

}
