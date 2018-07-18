using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class LevelClearUI : MonoBehaviour {
    public Text Hazards;
    CanvasGroup Group;

    private void Awake()
    {
        Group = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        LevelManager.NextLevelHazards += Show;
    }

    private void OnDisable()
    {
        LevelManager.NextLevelHazards -= Show;
    }

    private void Show(int hazards)
    {
        Hazards.text = string.Format(Hazards.text, hazards);
        LeanTween.value(gameObject, UpdateCanvas, 0, 1, 1);
        Group.interactable = true;
    }

    void UpdateCanvas(float newValue)
    {
        Group.alpha = newValue;
    }
}
