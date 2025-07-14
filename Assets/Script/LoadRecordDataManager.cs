using System;
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
        List<NoteEntry> nextList = new List<NoteEntry>();
        for(int i = 0; i <lines.Length; i++)
        {
            string line_data = lines[i];
            if (i == 0)
            {
                noteMoveManager.speed_by_sync = float.Parse(line_data.Split(",")[1]);
            } else
            {
                if (line_data != "" && line_data != null && line_data.Contains("|"))
                {
                    string[] line_ele = line_data.Trim().Split("|");
                    if (line_ele.Length != 3) continue;

                    nextList.Add(new NoteEntry
                    {
                        button = line_ele[0],
                        originY = float.Parse(line_ele[1]),
                        length = float.Parse(line_ele[2]),
                    });
                }
                else
                {
                    continue;
                }
            }
        }

        nextList.Sort(comparison: new Comparison<NoteEntry>(Compare));

        nextList[0].nextY = nextList[0].originY;
        for (int i = 1; i< nextList.Count; i++)
        {
            float prevY = nextList[i - 1].nextY;
            float originGap = nextList[i].originY - nextList[i - 1].originY;
            float nextGap = originGap * noteMoveManager.speed;

            nextList[i].nextY = prevY + nextGap;

            if(nextList[i].length > 0.25)
            {
                float nextLength = nextList[i].length * noteMoveManager.speed;
                nextList[i].length = nextLength;
            }
        }

        for(int i = 1; i < lines.Length; i++)
        {
            string ele = "";
            ele += nextList[i - 1].button + "|" + nextList[i - 1].nextY + "|" + nextList[i - 1].length;
            lines[i] = ele;
        }
    }

    public virtual void NextLine(int index)
    {
        Debug.Log("parent");   
    }

    static int Compare(NoteEntry x, NoteEntry y)
    {
        float num1 = x.originY;
        float num2 = y.originY;
        if (num1 > num2) return 1;
        else if (num1 < num2) return -1;
        else return 0;
    }

    class NoteEntry
    {
        public string button;
        public float originY;
        public float nextY;
        public float length;
    }

}
