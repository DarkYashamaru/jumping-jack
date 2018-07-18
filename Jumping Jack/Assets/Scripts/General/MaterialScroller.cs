using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialScroller : MonoBehaviour {
    public float ScrollSpeed = 1;
    public float currentValue;
    Material mat;

    private void Awake()
    {
        mat = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        currentValue += ScrollSpeed * Time.deltaTime;
        mat.SetTextureOffset("_MainTex", new Vector2(currentValue, 0));
    }
}
