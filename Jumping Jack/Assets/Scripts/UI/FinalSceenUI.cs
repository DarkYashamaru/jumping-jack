using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class FinalSceenUI : MonoBehaviour {

    public Text FinalScore;
    CanvasGroup Group;

    private void Awake()
    {
        Group = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        LevelManager.GameClear += Show;
    }

    private void OnDisable()
    {
        LevelManager.GameClear -= Show;
    }

    private void Show()
    {
        FinalScore.text = ScoreManager.GetScore().ToString();
        LeanTween.value(gameObject, UpdateCanvas, 0, 1, 1);
    }

    void UpdateCanvas(float newValue)
    {
        Group.alpha = newValue;
    }
}
