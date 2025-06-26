using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadRecordDataManager : MonoBehaviour
{
    public ScoreManager scoreManager;
    public string Song_Name;

    public NoteMoveManager noteMoveManager;
    public NotePoolManager notePoolManager;

    protected string[] lines;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        LoadData();
    }

    public void LoadData()
    {
        lines = File.ReadAllLines("./Assets/RecordData/" + Song_Name + ".txt");//(@".\Assets\RecordData\"+ Song_Name + ".txt");
        NoteFixBySpeed();
        scoreManager.NoteCountInit((lines.Length - 1));
    }

    public string[] ReturnLine()
    {
        return lines;
    }


    public void NoteFixBySpeed()
    {
        for(int i = 0; i <lines.Length; i++)
        {
            Debug.Log(lines[i]);
        }
    }


    public virtual void NextLine(int index)
    {
        Debug.Log("parent");   
    }

}
