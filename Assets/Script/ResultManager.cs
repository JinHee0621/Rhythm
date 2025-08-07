using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class ResultManager : MonoBehaviour
{
    public InGameUIManager inGameUIManager;

    public ScoreManager scoreManager;
    public GameObject resultSet;
    public GameObject gameSet;
    public GameObject gameGearSet;

    public int[] hitTypeCnt = new int[5];
    public Text[] hitTypeCntTxt = new Text[5];

    //0:perfect, 1:good, 2:soso, 3:bad, 4:miss

    public void CheckHitTypeCount(int type)
    {
        hitTypeCnt[type] += 1;
    }

    public void ShowResult()
    {
        StartCoroutine(ShowResultRunning());
    }

    IEnumerator ShowResultRunning()
    {
        yield return new WaitForSeconds(5f);
        inGameUIManager.FadeOut();
        yield return new WaitForSeconds(1f);

        yield return new WaitForSeconds(5f);
        gameSet.SetActive(false);
        gameGearSet.SetActive(false);
        resultSet.SetActive(true);

        for (int idx = 0; idx < 5; idx++)
        {
            hitTypeCntTxt[idx].text = hitTypeCnt[idx].ToString();
        }

        inGameUIManager.FadeIn();
        yield return new WaitForSeconds(5f);
        StartCoroutine(MoveMusicSelect());
    }

    IEnumerator MoveMusicSelect()
    {
        yield return new WaitForSeconds(5f);
        inGameUIManager.FadeOut();
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("MusicSelect");
    }

}
