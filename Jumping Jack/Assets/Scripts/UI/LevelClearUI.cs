using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class LevelClearUI : MonoBehaviour {
    public Text Hazards;
    CanvasGroup Group;
    public GameData Data;
    public Text Message;
    int messageIndex;
    public float DelayBeforeText = 2;
    public float MessageDelayTime = 0.1f;

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
        messageIndex = hazards - 1;
        Hazards.text = string.Format(Hazards.text, hazards);
        LeanTween.value(gameObject, UpdateCanvas, 0, 1, 1);
        Group.interactable = true;
        DisplayMessage();
    }

    void UpdateCanvas(float newValue)
    {
        Group.alpha = newValue;
    }

    void DisplayMessage ()
    {
        StartCoroutine(AnimateMessage());
    }

    IEnumerator AnimateMessage ()
    {
        Message.text = string.Empty;
        yield return new WaitForSeconds(DelayBeforeText);
        string message = Data.Messages[messageIndex];
        message = message.Replace("[s]", "\n");

        for (int i = 0; i < message.Length; i++)
        {
            Message.text += message[i];
            yield return new WaitForSeconds(MessageDelayTime);
        }
    }
}
