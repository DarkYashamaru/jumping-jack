using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeUI : MonoBehaviour {
    public GameObject[] Lives;

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
        DisableAll();
        for (int i = 0; i < lives; i++)
        {
            Lives[i].SetActive(true);
        }
    }

    void DisableAll ()
    {
        for (int i = 0; i < Lives.Length; i++)
        {
            Lives[i].SetActive(false);
        }
    }

}
