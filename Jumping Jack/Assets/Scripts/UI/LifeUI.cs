using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeUI : MonoBehaviour {
    public Text Life;

    private void OnEnable()
    {
        LifeManager.LifeUpdated += UpdateLife;
    }

    private void OnDisable()
    {
        LifeManager.LifeUpdated -= UpdateLife;
    }

    private void UpdateLife(int lives)
    {
        Life.text = lives.ToString();
    }



}
