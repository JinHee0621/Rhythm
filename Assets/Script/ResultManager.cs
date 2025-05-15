using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ResultManager : MonoBehaviour
{
    public GameObject resultSet;
    public Image fadeOutScreen;

    public int[] hitTypeCnt = new int[5];

    // Update is called once per frame
    void Update()
    {
        
    }

    //0:perfect, 1:good, 2:soso, 3:bad, 4:miss
    public void CheckHitTypeCount(int type)
    {
        hitTypeCnt[type] += 1;
    }

    public void ShowResult()
    {
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(5f);
        fadeOutScreen.DOColor(new Color(0f, 0f, 0f, 1f), 1f);
        yield return new WaitForSeconds(1f);
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        fadeOutScreen.DOColor(new Color(0f, 0f, 0f, 0f), 1f);
        yield return new WaitForSeconds(1f);
    }


}
