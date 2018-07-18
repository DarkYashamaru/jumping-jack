using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour {
    public Text FinalScore;
    public CanvasGroup Group;
    public static event System.Action RestartPressed;
    bool restartPressed;

    private void Awake()
    {
        Group = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        LifeManager.GameOver += Show;
    }

    private void Show()
    {
        FinalScore.text = ScoreManager.GetScore().ToString();
        LeanTween.value(gameObject, UpdateCanvas, 0, 1, 1);
    }

    void UpdateCanvas (float newValue)
    {
        Group.alpha = newValue;
    }

    private void OnDisable()
    {
        LifeManager.GameOver -= Show;
    }

    public void Restart ()
    {
        if(!restartPressed)
        {
            restartPressed = true;
            if (RestartPressed != null)
                RestartPressed();
        }
    }
}
