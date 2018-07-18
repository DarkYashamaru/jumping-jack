using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardInstancer : MonoBehaviour {
    public GameObject[] HazardModels;
    public static int HazardIndex;

    private void Awake()
    {
        Instance();
    }

    void Instance()
    {
        GameObject instance = Instantiate(HazardModels[HazardIndex], transform);
        instance.transform.localPosition = Vector3.zero;
        IncreaseIndex();
    }

    void IncreaseIndex ()
    {
        HazardIndex++;
        if(HazardIndex >= HazardModels.Length-1)
        {
            HazardIndex = 0;
        }
    }

}
