using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ScoreManager : MonoBehaviour
{
    public InGameUIManager inGameUIManager;

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

    private string[] accuracyUiText = {"Perfect","Greate","Soso","Bad","Miss"};

    private bool delayComboEnd = false;

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
        current_score += (int)( note_score * (acc / 100f) );
        ShowScore();
    }

    public void ShowScore()
    {
        scoreText.text = current_score.ToString();
    }

    public string PrintScore()
    {
        return current_score.ToString();
    }

    public string PrintAccuracy()
    {
        return accuracyText.text;
    }

    public void AddCombo()
    {
        current_combo += 1;
        inGameUIManager.ComboAnim();
        ShowCombo();
    }

    public void AddComboDelay()
    {
        if(!delayComboEnd)
        {
            delayComboEnd = true;
            StartCoroutine(ComboDelay());
        }
    }

    IEnumerator ComboDelay()
    {
        current_combo += 1;
        inGameUIManager.ComboAnim();
        ShowCombo();
        yield return new WaitForSeconds(0.1f);
        delayComboEnd = false;   
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
        float perfect_rate = 0f;//(4.5f + (0.15f * noteMoveManager.speed)) * -1;
        float speed_by_rate = (noteMoveManager.speed-1) * 0.15f;
        //Accuracy Range
        if (accData != 100f)
        {
            //Debug.Log(accData);
            //pp : (perfect_rate - speed_by_rate) ~ ((perfect_rate - speed_by_rate) + 1f + speed_by_rate))
            if (accData > (perfect_rate - speed_by_rate) && accData < ((perfect_rate + 0.5f + speed_by_rate)))
            {
                inAcc = 100f;
                ShowAccText(0);
                resultManager.CheckHitTypeCount(0);
            }
            else if (accData >= (perfect_rate + 0.5f + speed_by_rate) && accData < (perfect_rate + 1.0f + speed_by_rate))
            {
                inAcc = 90f;
                ShowAccText(1);
                resultManager.CheckHitTypeCount(1);
            }
            else if (accData >= (perfect_rate + 1.0f + speed_by_rate) && accData < (perfect_rate + 2.0f + speed_by_rate))
            {
                inAcc = 70f;
                ShowAccText(2);
                resultManager.CheckHitTypeCount(2);
            }
            else
            {
                inAcc = 50f;
                ShowAccText(3);
                resultManager.CheckHitTypeCount(3);
            }
        } else
        {
            inAcc = 0f;
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
        hitText.transform.localScale = new Vector3(0f, 0f, 1f);
        StopCoroutine(ShowText(type));
        StartCoroutine(ShowText(type));
    }

    IEnumerator ShowText(int type)
    {
        hitText.text = accuracyUiText[type];
        hitText.transform.DOScale(new Vector3(1f, 1f, 1f), 0.25f);
        yield return new WaitForSeconds(0.25f);
        hitText.transform.localScale = new Vector3(0f, 0f, 1f);
    }

    public void reset_note_count()
    {
        in_note_count = 0;
    }
}
