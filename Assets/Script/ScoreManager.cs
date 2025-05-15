using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public ResultManager resultManager;

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
        float absData = Math.Abs(data);
        float inAcc = 0f;

        //Accuracy Range
        if(absData != 100)
        {
            if (absData > 0f && absData < 0.3)
            {
                inAcc = 100f;
                resultManager.CheckHitTypeCount(0);
            }
            else if (absData >= 0.3f && absData < 0.5)
            {
                inAcc = 90f;
                resultManager.CheckHitTypeCount(1);
            }
            else if (absData >= 0.5f && absData < 0.7)
            {
                inAcc = 70f;
                resultManager.CheckHitTypeCount(2);
            }
            else
            {
                inAcc = 50f;
                resultManager.CheckHitTypeCount(3);
            }
        } else
        {
            inAcc = 0;
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

    public void reset_note_count()
    {
        in_note_count = 0;
    }
}
