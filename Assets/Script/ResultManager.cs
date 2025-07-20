using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class ResultManager : MonoBehaviour
{
    public GameObject resultSet;
    public GameObject gameSet;
    public GameObject gameGearSet;
    public Image fadeOutScreen;

    public int[] hitTypeCnt = new int[5];
    public Text[] hitTypeCntTxt = new Text[5];

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
        yield return new WaitForSeconds(5f);

        gameSet.SetActive(false);
        gameGearSet.SetActive(false);
        resultSet.SetActive(true);

        for(int idx = 0; idx < 5; idx++)
        {
            hitTypeCntTxt[idx].text = hitTypeCnt[idx].ToString();
        }

        fadeOutScreen.DOColor(new Color(0f, 0f, 0f, 0f), 1f);
        yield return new WaitForSeconds(5f);
        StartCoroutine(MoveMusicSelect());
    }

    IEnumerator MoveMusicSelect()
    {
        yield return new WaitForSeconds(5f);
        fadeOutScreen.DOColor(new Color(0f, 0f, 0f, 1f), 1f);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("MusicSelect");
    }


}
