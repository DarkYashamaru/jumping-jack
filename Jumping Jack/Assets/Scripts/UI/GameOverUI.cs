using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class GameOverUI : MonoBehaviour {
    public Text FinalScore;
    public Text Hazards;
    CanvasGroup Group;
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
        Hazards.text = string.Format(Hazards.text, LevelManager.GetInstance().Currentlevel);
        LeanTween.value(gameObject, UpdateCanvas, 0, 1, 1);
        Group.interactable = true;
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
