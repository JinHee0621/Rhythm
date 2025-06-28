using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    public NoteMoveManager noteMoveManager;
    public Text timeText;
    private double currTime;

    private void Update()
    {
        if(noteMoveManager.running)
        {
            StartTimeCheck();
        }       
    }


    public void StartTimeCheck()
    {
        currTime += Time.deltaTime;
        PrintTime(currTime);
    }
    public void PrintTime(double data)
    {
        double min = Math.Truncate(data / 60);
        double time = (Math.Truncate((data % 60) * 100) / 100);

        string min_str = min.ToString();
        string time_str = time.ToString();

        string min_text_str = "00";
        string time_sec_str = "00";
        string time_sub_str = "00";

        if (time_str.Length > 2)
        {
            time_sec_str = time_str.Substring(0, 2);
            if(time_str.Contains("."))
            {
                time_sub_str = time_str.Substring(time_str.IndexOf(".") + 1);
            }
        } else
        {
            time_sec_str = time_str;
        }

        if (min_str.Length == 1) min_str = "0" + min_str;
        min_text_str = min_str;

        if (time_sec_str.Contains(".")) time_sec_str = "0" + time_sec_str.Substring(0,1);

        if (time_sub_str.Length == 1) time_sub_str = "0" + time_sub_str;
        timeText.text = min_text_str + ":" + time_sec_str + ":" + time_sub_str;
    }

}
