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

    public void CheckFind()
    {
        Debug.Log("Find Score");
    }

    public void NoteCountInit(int note_cnt)
    {
        total_note_count = note_cnt;
        note_score = MAX_SCORE / note_cnt;
    }

    public void AddScore()
    {
        current_score += note_score;
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

    public void CheckAccuracy(float data)
    {
        float absData = Math.Abs(data);
        float inAcc = 0f;

        //Accuracy Range
        if(absData != 100)
        {
            if (absData > 0f && absData < 0.3)
            {
                inAcc = 100f;
            }
            else if (absData >= 0.3f && absData < 0.5)
            {
                inAcc = 90f;
            }
            else if (absData >= 0.5f && absData < 0.7)
            {
                inAcc = 70f;
            }
            else
            {
                inAcc = 50f;
            }
        } else
        {
            inAcc = 0;
        }

        in_note_count += 1;
        current_accuracy += inAcc;
        accuracy_rate = (current_accuracy / in_note_count);
        string rate_text = accuracy_rate.ToString("0.00") + "%";
        accuracyText.text = rate_text;
    }

    public void reset_note_count()
    {
        in_note_count = 0;
    }
}
