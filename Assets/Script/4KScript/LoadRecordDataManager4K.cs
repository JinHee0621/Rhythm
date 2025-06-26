using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadRecordDataManager4K : LoadRecordDataManager
{
    public GameObject Note;
    public GameObject NoteLine_Base;

    public Transform Line1;
    public Transform Line2;
    public Transform Line3;
    public Transform Line4;

//    private int Line_index = 0;

    void Start()
    {
        notePoolManager.InitLine();
        LoadDataLine();
        Debug.Log(lines.Length);
    }

    public void LoadDataLine()
    {
        //File in first line data is number of note data
        for(int i=1; i < notePoolManager.poolLength; i++)
        {
            AddNote(lines[i]);
        }
        NoteLine_Base.GetComponent<NoteMoveManager>().running = true;
    }


    public override void NextLine(int index)
    {
        if(index < lines.Length)
        {
            AddNote(lines[index]);
        }
    }

    private void AddNote(string data)
    {
        string[] data_element = data.Split("|");

        Transform parent_line = null;
        if (data_element[0].Equals("Btn1")) parent_line = Line1;
        else if (data_element[0].Equals("Btn2")) parent_line = Line2;
        else if (data_element[0].Equals("Btn3")) parent_line = Line3;
        else if (data_element[0].Equals("Btn4")) parent_line = Line4;

        float position_y = 0f;
        float.TryParse(data_element[1], out position_y);
        //position_y *= noteMoveManager.speed;

        float noteLength = 0f;
        float.TryParse(data_element[2], out noteLength);

        if (parent_line != null)
        {
            notePoolManager.InitNote(parent_line, position_y, noteLength);
        }
    }

}
