using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class InGameUIManager : MonoBehaviour
{
    [Header("Combo")]
    public Text comboText;

    [Header("Fade")]
    public Image fadeOutScreen;

    //Combo UI
    public void ComboAnim()
    {
        comboText.transform.localPosition = new Vector3(comboText.transform.localPosition.x, 0f, comboText.transform.localPosition.z);
        comboText.transform.DOLocalMoveY(20f, 0.1f);
    }

    //Fade UI
    public void FadeOut()
    {
        fadeOutScreen.DOColor(new Color(0f, 0f, 0f, 1f), 1f);
    }

    public void FadeIn()
    {
        fadeOutScreen.DOColor(new Color(0f, 0f, 0f, 0f), 1f);
    }

}
