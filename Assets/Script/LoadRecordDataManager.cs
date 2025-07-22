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

    [SerializeField]
    protected string[] origin_lines;
    [SerializeField]
    protected string[] lines;

    private void Awake()
    {
        Application.targetFrameRate = OptionManager.instance.frameRate;
        OptionManager.instance.ChageInGame(true);
        //LoadData(Song_Name);
    }

    public void LoadData(string musicName)
    {
        if(musicName.Equals("Test"))
        {
            origin_lines = File.ReadAllLines("./Assets/RecordData/" + Song_Name + ".txt");
        } else
        {
            origin_lines = File.ReadAllLines("./Assets/RecordData/" + musicName + ".txt");//(@".\Assets\RecordData\"+ Song_Name + ".txt");
        }

        CopyLines();
        NoteFixBySpeed(OptionManager.instance.noteSpeed);
        scoreManager.NoteCountInit((lines.Length - 1));
    }

    //Deep Copy
    private void CopyLines()
    {
        lines = new string[origin_lines.Length];
        for(int i = 0; i < origin_lines.Length; i++)
        {
            lines[i] = origin_lines[i];
        }
    }


    public string[] ReturnLine()
    {
        return lines;
    }

    public void NoteFixBySpeed(float speed)
    {
        List<NoteEntry> nextList = new List<NoteEntry>();
        for(int i = 0; i <lines.Length; i++)
        {
            string line_data = origin_lines[i]; //lines[i];
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
            float nextGap = originGap * speed;

            nextList[i].nextY = prevY + nextGap;

            if(nextList[i].length > 0.25)
            {
                float nextLength = nextList[i].length * speed;
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

    public void ResetNoteBySpeed()
    {
        NoteFixBySpeed(noteMoveManager.speed);
        //GameObject[] currentNotes = GameObject.FindGameObjectsWithTag("Note");
        //in_note_count ~ poolLength
        for (int i = scoreManager.in_note_count; i < notePoolManager.poolLength; i++)
        {
            ReLine(i + 1);
        }
    }

    public virtual void ReLine(int index)
    {
        Debug.Log("Re parent");
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
