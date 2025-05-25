using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ScoreManager : MonoBehaviour
{
    private const int MAX_SCORE = 1000000;
    private int current_score = 0;
    private int total_note_count = 0;
    private int note_score = 0;
    public Text scoreText;

    private int current_combo = 0;
    public Text comboText;

    private float accuracy_rate = 100.00f;
    private float current_accuracy = 0f;
    public int in_note_count = 0;
    public Text accuracyText;

    public NoteMoveManager noteMoveManager;
    public ResultManager resultManager;

    //public Text[] hitText = new Text[5];
    public Text hitText;
    private String[] accuracyUiText = {"Perfect","Greate","Soso","Bad","Miss"};


    public void CheckFind()
    {
        Debug.Log("Find Score");
    }

    public void NoteCountInit(int note_cnt)
    {
        total_note_count = note_cnt;
        note_score = MAX_SCORE / note_cnt;
    }

    public void AddScore(float acc)
    {
        current_score += (int)( note_score * (acc / 100) );
        ShowScore();
    }

    public void ShowScore()
    {
        scoreText.text = current_score.ToString();
    }

    public void AddCombo()
    {
        current_combo += 1;
        ShowCombo();
    }

    public void ResetCombo()
    {
        current_combo = 0;
        ShowCombo();
    }

    public void ShowCombo()
    {
        comboText.text = current_combo.ToString();
    }

    public float CheckAccuracy(float data)
    {
        float accData = data;
        float inAcc = 0f;
        float perfect_rate = (-4.5f + (0.1f * noteMoveManager.speed));
//        Debug.Log(data);
        //Accuracy Range
        if(accData != 100f)
        {
            if (accData > (perfect_rate - 0.25f) && accData < (perfect_rate + 0.5f))
            {
                inAcc = 100f;
                ShowAccText(0);
                resultManager.CheckHitTypeCount(0);
            }
            else if (accData >= (perfect_rate + 0.5f) && accData < (perfect_rate + 1.25f))
            {
                inAcc = 90f;
                ShowAccText(1);
                resultManager.CheckHitTypeCount(1);
            }
            else if (accData >= (perfect_rate + 1.25f) && accData < (perfect_rate + 2.25f))
            {
                inAcc = 70f;
                ShowAccText(2);
                resultManager.CheckHitTypeCount(2);
            }
            else if (accData >= (perfect_rate + 2.25f))
            {
                inAcc = 50f;
                ShowAccText(3);
                resultManager.CheckHitTypeCount(3);
            } else
            {
                inAcc = 0;
                ShowAccText(4);
                resultManager.CheckHitTypeCount(4);
            }
        } else
        {
            inAcc = 0;
            ShowAccText(4);
            resultManager.CheckHitTypeCount(4);
        }

        in_note_count += 1;
        current_accuracy += inAcc;
        accuracy_rate = (current_accuracy / in_note_count);
        string rate_text = accuracy_rate.ToString("0.00") + "%";
        accuracyText.text = rate_text;

        if(in_note_count == total_note_count)
        {
            resultManager.ShowResult();
        }
        return inAcc;
    }

    public void ShowAccText(int type)
    {
        hitText.gameObject.SetActive(false);
        StartCoroutine(ShowText(type));
    }

    IEnumerator ShowText(int type)
    {
        hitText.gameObject.SetActive(true);
        hitText.text = accuracyUiText[type];
        hitText.transform.localScale = new Vector3(0f, 0f, 1f);
        hitText.transform.DOScale(new Vector3(1f, 1f, 1f), 0.25f);
        yield return new WaitForSeconds(0.25f);
        hitText.gameObject.SetActive(false);
    }

    public void reset_note_count()
    {
        in_note_count = 0;
    }
}
