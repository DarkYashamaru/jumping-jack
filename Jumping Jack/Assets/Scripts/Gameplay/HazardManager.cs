using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardManager : MonoBehaviour {
    static HazardManager Instance;
    public GameData Data;
    public GameConfig Config;
    public GameObject HazardPlaceHolder;
    public Transform HazardParent;

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

    private void OnEnable()
    {
        LevelManager.LevelStart += SetHazardsByLevel;
    }

    private void OnDisable()
    {
        LevelManager.LevelStart -= SetHazardsByLevel;
    }

    void SetHazardsByLevel (int hazards)
    {
        Debug.Log(hazards);
        for (int i = 0; i < hazards; i++)
        {
            GameObject instance = Instantiate(HazardPlaceHolder, HazardParent);
            float y = Config.HazardInitialYPos - (Data.LevelInfo[hazards-1].Hazards[i].Position.y * Config.LineDistance);
            float x = Config.MinLeftPos + (Mathf.Abs(Config.MinRightPos - Config.MinLeftPos) * Data.LevelInfo[hazards - 1].Hazards[i].Position.x); 
            instance.transform.position = new Vector3(x, y, 0);
        }
    }
}
