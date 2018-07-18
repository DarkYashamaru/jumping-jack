using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageIndicator : MonoBehaviour {
    public Image DamageImage;

    private void OnEnable()
    {
        PlayerMovement.Damage += DisplayDamage;
    }

    private void OnDisable()
    {
        PlayerMovement.Damage -= DisplayDamage;
    }

    void DisplayDamage ()
    {
        StartCoroutine(DamageAnimation());
    }

    IEnumerator DamageAnimation ()
    {
        float animationTime = 0.1f;
        LeanTween.alpha(DamageImage.rectTransform, 0.3f, animationTime);
        yield return new WaitForSeconds(animationTime);
        LeanTween.alpha(DamageImage.rectTransform, 0f, animationTime);
    }

}
