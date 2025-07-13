using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
public class InputRecordManager : MonoBehaviour
{
    public string fileName;
    public SoundManager soundManager;
    public GameObject Note;
    public GameObject LineBase;

    
    protected bool recordRunning = false;
    protected float recentPosition = -100f;
    protected float beforeYPosition = 0f;
    protected ArrayList noteList = new ArrayList();
    protected int noteCnt = 0;


    bool recordEnd = false;
    private void Awake()
    {
        fileName = "./Assets/RecordData/" + fileName + ".txt";//@".\Assets\RecordData\NoteData1.txt"))
    }
    public void Record()
    {
        if (LineBase.GetComponent<NoteMoveManager>().running)
        {
            if (Input.GetKey(KeyCode.E))
            {
                soundManager.PlayBgm(false);
                bool running_check = LineBase.GetComponent<NoteMoveManager>().running;
                if (!recordEnd && running_check)
                {
                    Debug.Log("Record End" + LineBase.transform.position.y);
                    Debug.Log("Default Length: " + LineBase.GetComponent<NoteMoveManager>().Print_default_pos());
                    using (StreamWriter outputFile = new StreamWriter(fileName))
                    {
                        outputFile.WriteLine(LineBase.GetComponent<NoteMoveManager>().Print_default_pos() - LineBase.transform.position.y);
                        for (int i = 0; i < noteList.Count; i++)
                        {
                            outputFile.WriteLine(noteList[i]);
                        }
                    }
                    recordEnd = true;
                    recentPosition = -100f;
                    LineBase.GetComponent<NoteMoveManager>().running = false;
                }
            }
        }

        if (Input.GetKey(KeyCode.R))
        {
            if (!recordRunning)
            {
                bool running_check = LineBase.GetComponent<NoteMoveManager>().running;
               
                if (!running_check)
                {
                    LineBase.GetComponent<NoteMoveManager>().running = true;
                    Debug.Log("Record Start");
                }
                recordRunning = true;
                StartCoroutine(Delay5SecondPlay());
            }
        }
    }

    protected IEnumerator Delay5SecondPlay()
    {
        yield return new WaitForSeconds(5.0f);
        soundManager.PlayBgm(true);
    }


    protected float NotePosition(float paramPos)
    {
        float positionY = paramPos;

        if (recentPosition == -100f) recentPosition = positionY;
        else
        {
            if (Math.Abs(recentPosition - positionY) < 0.1f)
            {
                positionY = recentPosition;
            }
            else
            {
                recentPosition = positionY;
            }
        }
        return positionY;
    }


    protected IEnumerator ColorDisabled(SpriteRenderer target)
    {
        yield return new WaitForSeconds(0.25f);
        if (target.color.a > 0)
        {
            target.color = new Color(target.color.r, target.color.g, target.color.b, target.color.a - 0.05f);
            StartCoroutine(ColorDisabled(target));
        }
        else
        {
            target.color = new Color(target.color.r, target.color.g, target.color.b, 0);
        }
    }
    protected float CalNoteLength(float noteLength)
    {
        float result = 0f;
        //Debug.Log(noteLength);
        if (noteLength >= 10)
        {
            result = noteLength / 10;
        }
        else
        {
            result = 0.25f;
        }

        return result;
    }
}
