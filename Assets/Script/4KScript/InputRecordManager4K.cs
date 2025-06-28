using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class InputRecordManager4K : MonoBehaviour
{
    public GameObject Note;

    KeyCode inputBtnKey1 = KeyCode.D;
    KeyCode inputBtnKey2 = KeyCode.F;
    KeyCode inputBtnKey3 = KeyCode.J;
    KeyCode inputBtnKey4 = KeyCode.K;

    public SpriteRenderer Effect1;
    public SpriteRenderer Effect2;
    public SpriteRenderer Effect3;
    public SpriteRenderer Effect4;

    public BoxCollider2D BtnCollider1;
    public BoxCollider2D BtnCollider2;
    public BoxCollider2D BtnCollider3;
    public BoxCollider2D BtnCollider4;

    public Animator Btn1Anim;
    public Animator Btn2Anim;
    public Animator Btn3Anim;
    public Animator Btn4Anim;

    public GameObject LineBase;
    public Transform Line1;
    public Transform Line2;
    public Transform Line3;
    public Transform Line4;

    public Transform SponePos1;
    public Transform SponePos2;
    public Transform SponePos3;
    public Transform SponePos4;

    float btn1Time = 0f;
    float btn2Time = 0f;
    float btn3Time = 0f;
    float btn4Time = 0f;

    float beforeYPosition = 0f;

    bool Line1NoteSpone = false;
    bool Line2NoteSpone = false;
    bool Line3NoteSpone = false;
    bool Line4NoteSpone = false;
    bool recordEnd = false;

    ArrayList noteList = new ArrayList();
    int noteCnt = 0;

    GameObject newNote1 = null;
    GameObject newNote2 = null;
    GameObject newNote3 = null;
    GameObject newNote4 = null;

    private float recentPosition = -100f;

    private float btn1BeforeYPosition = 0f;
    private bool btn1FristInput = false;
    private float btn2BeforeYPosition = 0f;
    private bool btn2FristInput = false;
    private float btn3BeforeYPosition = 0f;
    private bool btn3FristInput = false;
    private float btn4BeforeYPosition = 0f;
    private bool btn4FristInput = false;

    void Update()
    {
        if (LineBase.GetComponent<NoteMoveManager>().running)
        {
            InputBtn1(Input.GetKey(inputBtnKey1));
            InputBtn2(Input.GetKey(inputBtnKey2));
            InputBtn3(Input.GetKey(inputBtnKey3));
            InputBtn4(Input.GetKey(inputBtnKey4));

            if (Input.GetKey(KeyCode.E))
            {
                bool running_check = LineBase.GetComponent<NoteMoveManager>().running;
                if (!recordEnd && running_check)
                {
                    Debug.Log("Record End" + LineBase.transform.position.y);
                    Debug.Log("Default Length: " + LineBase.GetComponent<NoteMoveManager>().Print_default_pos());
                    using (StreamWriter outputFile = new StreamWriter("./Assets/RecordData/NoteData3.txt"))//@".\Assets\RecordData\NoteData1.txt"))
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
            bool running_check = LineBase.GetComponent<NoteMoveManager>().running;
            if (!running_check)
            {
                LineBase.GetComponent<NoteMoveManager>().running = true;
                Debug.Log("Record Start");
            }
        }
    }

    private float NotePosition(float paramPos)
    {
        float positionY = paramPos;

        if (recentPosition == -100f) recentPosition = positionY;
        else
        { 
            if (Math.Abs(recentPosition - positionY) < 0.1f)
            {
                positionY = recentPosition;
            } else
            {
                recentPosition = positionY;
            }
        }
        return positionY;
    }


    public void InputBtn1(bool input)
    {
        if (input)
        {
            BtnCollider1.enabled = true;
            Btn1Anim.SetBool("ButtonPush", true);
            Effect1.color = new Color(Effect1.color.r, Effect1.color.g, Effect1.color.b, 1);
            btn1Time += 1f;
 
            if (!Line1NoteSpone)
            {
                newNote1 = Instantiate(Note, Line1);
                newNote1.transform.position = SponePos1.position;
                Line1NoteSpone = true;

                //Debug.Log(newNote1.transform.localPosition.y);
                //If LongNote sponed then check before position
                if(btn1FristInput == false)
                {
                    btn1BeforeYPosition = newNote1.transform.localPosition.y;
                    btn1FristInput = true;
                }
            }
        }
        else
        {
            if (btn1Time != 0f)
            {
                btn1Time = CalNoteLength(btn1Time);
                newNote1.GetComponentInChildren<NoteManager>().noteLength = btn1Time;
                newNote1.GetComponentInChildren<NoteManager>().SetNoteState();
                float currentYPosition = newNote1.transform.localPosition.y;
                if (beforeYPosition == 0f || (currentYPosition - beforeYPosition > 0.5f))
                {
                    //When Input Note time is far
                    beforeYPosition = newNote1.transform.localPosition.y;
                } else if(currentYPosition - beforeYPosition < 0.5f) {
                    //When Input Note time is near
                    currentYPosition = beforeYPosition;
                }

                //Check LongNote position
                string noteInfo;
                if(btn1Time > 0.25f)
                {
                    noteInfo = "Btn1|" + btn1BeforeYPosition + "|" + btn1Time;
                }
                else
                {
                    noteInfo = "Btn1|" + NotePosition(currentYPosition - (btn1Time * 0.016f)) + "|" + btn1Time;
                }

                noteCnt += 1;
                noteList.Add(noteInfo);
                Line1NoteSpone = false;
            }
            BtnCollider1.enabled = false;
            Btn1Anim.SetBool("ButtonPush", false);
            StartCoroutine(ColorDisabled(Effect1));
            btn1Time = 0f;

            //Before Position init
            btn1BeforeYPosition = 0f;
            btn1FristInput = false;
        }
    }

    public void InputBtn2(bool input)
    {
        Vector3 inputTimePos = SponePos2.position;
        if (input)
        {
            noteCnt += 1;
            BtnCollider2.enabled = true;
            Btn2Anim.SetBool("ButtonPush", true);
            Effect2.color = new Color(Effect2.color.r, Effect2.color.g, Effect2.color.b, 1);
            btn2Time += 1f;
            if (!Line2NoteSpone)
            {
                newNote2 = Instantiate(Note, Line2);
                newNote2.transform.position = SponePos2.position;
                Line2NoteSpone = true;
            }

            //If LongNote sponed then check before position
            if (btn2FristInput == false)
            {
                btn2BeforeYPosition = newNote2.transform.localPosition.y;
                btn2FristInput = true;
            }
        }
        else
        {
            if (btn2Time != 0f)
            {
                btn2Time = CalNoteLength(btn2Time);
                newNote2.GetComponentInChildren<NoteManager>().noteLength = btn2Time;
                newNote2.GetComponentInChildren<NoteManager>().SetNoteState();
                float currentYPosition = newNote2.transform.localPosition.y;
                if (beforeYPosition == 0f || (currentYPosition - beforeYPosition > 0.5f))
                {
                    //Debug.Log(currentYPosition - beforeYPosition);
                    //When Input Note time is far
                    beforeYPosition = newNote2.transform.localPosition.y;
                }
                else if (currentYPosition - beforeYPosition < 0.5f)
                {
                    //When Input Note time is near
                    currentYPosition = beforeYPosition;
                }

                //Check LongNote position
                string noteInfo;
                if (btn2Time > 0.25f)
                {
                    noteInfo = "Btn2|" + btn2BeforeYPosition + "|" + btn2Time;
                }
                else
                {
                    noteInfo = "Btn2|" + NotePosition(currentYPosition - (btn2Time * 0.016f)) + "|" + btn2Time;
                }

                noteCnt += 1;
                noteList.Add(noteInfo);
                Line2NoteSpone = false;

            }

            BtnCollider2.enabled = false;
            Btn2Anim.SetBool("ButtonPush", false);
            StartCoroutine(ColorDisabled(Effect2));
            btn2Time = 0f;

            //Before Position init
            btn2BeforeYPosition = 0f;
            btn2FristInput = false;
        }
    }

    public void InputBtn3(bool input)
    {
        Vector3 inputTimePos = SponePos3.position;
        if (input)
        {
            noteCnt += 1;
            BtnCollider3.enabled = true;
            Btn3Anim.SetBool("ButtonPush", true);
            Effect3.color = new Color(Effect3.color.r, Effect3.color.g, Effect3.color.b, 1);
            btn3Time += 1f;
            if (!Line3NoteSpone)
            {
                newNote3 = Instantiate(Note, Line3);
                newNote3.transform.position = SponePos3.position;
                Line3NoteSpone = true;
            }

            //If LongNote sponed then check before position
            if (btn3FristInput == false)
            {
                btn3BeforeYPosition = newNote3.transform.localPosition.y;
                btn3FristInput = true;
            }
        }
        else
        {
            if (btn3Time != 0f)
            {
                btn3Time = CalNoteLength(btn3Time);
                newNote3.GetComponentInChildren<NoteManager>().noteLength = btn3Time;
                newNote3.GetComponentInChildren<NoteManager>().SetNoteState();
                float currentYPosition = newNote3.transform.localPosition.y;
                if (beforeYPosition == 0f || (currentYPosition - beforeYPosition > 0.5f))
                {
                    //Debug.Log(currentYPosition - beforeYPosition);
                    //When Input Note time is far
                    beforeYPosition = newNote3.transform.localPosition.y;
                }
                else if (currentYPosition - beforeYPosition < 0.5f)
                {
                    //When Input Note time is near
                    currentYPosition = beforeYPosition;
                }

                //Check LongNote position
                string noteInfo;
                if (btn3Time > 0.25f)
                {
                    noteInfo = "Btn3|" + btn3BeforeYPosition + "|" + btn3Time;
                }
                else
                {
                    noteInfo = "Btn3|" + NotePosition(currentYPosition - (btn3Time * 0.016f)) + "|" + btn3Time;
                }

                noteCnt += 1;
                noteList.Add(noteInfo);
                Line3NoteSpone = false;
            }

            BtnCollider3.enabled = false;
            Btn3Anim.SetBool("ButtonPush", false);
            StartCoroutine(ColorDisabled(Effect3));
            btn3Time = 0f;
            
            //Before Position init
            btn3BeforeYPosition = 0f;
            btn3FristInput = false;
        }
    }

    public void InputBtn4(bool input)
    {
        Vector3 inputTimePos = SponePos4.position;
        if (input)
        {
            noteCnt += 1;
            BtnCollider4.enabled = true;
            Btn4Anim.SetBool("ButtonPush", true);
            Effect4.color = new Color(Effect4.color.r, Effect4.color.g, Effect4.color.b, 1);
            btn4Time += 1f;
            if (!Line4NoteSpone)
            {
                newNote4 = Instantiate(Note, Line4);
                newNote4.transform.position = SponePos4.position;
                Line4NoteSpone = true;
            }

            //If LongNote sponed then check before position
            if (btn4FristInput == false)
            {
                btn4BeforeYPosition = newNote4.transform.localPosition.y;
                btn4FristInput = true;
            }
        }
        else
        {
            if (btn4Time != 0f)
            {
                btn4Time = CalNoteLength(btn4Time);
                newNote4.GetComponentInChildren<NoteManager>().noteLength = btn4Time;
                newNote4.GetComponentInChildren<NoteManager>().SetNoteState();
                float currentYPosition = newNote4.transform.localPosition.y;
                if (beforeYPosition == 0f || (currentYPosition - beforeYPosition > 0.5f))
                {
                    //When Input Note time is far
                    beforeYPosition = newNote4.transform.localPosition.y;
                }
                else if (currentYPosition - beforeYPosition < 0.5f)
                {
                    //When Input Note time is near
                    currentYPosition = beforeYPosition;
                }

                //Check LongNote position
                string noteInfo;
                if (btn4Time > 0.25f)
                {
                    noteInfo = "Btn4|" + btn4BeforeYPosition + "|" + btn4Time;
                }
                else
                {
                    noteInfo = "Btn4|" + NotePosition(currentYPosition - (btn4Time * 0.016f)) + "|" + btn4Time;
                }

                noteCnt += 1;
                noteList.Add(noteInfo);
                Line4NoteSpone = false;
            }

            BtnCollider4.enabled = false;
            Btn4Anim.SetBool("ButtonPush", false);
            StartCoroutine(ColorDisabled(Effect4));
            btn4Time = 0f;

            //Before Position init
            btn4BeforeYPosition = 0f;
            btn4FristInput = false;
        }
    }

    IEnumerator ColorDisabled(SpriteRenderer target)
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

    private float CalNoteLength(float noteLength)
    {
        float result = 0f;

        if (noteLength >= 80)
        {
            result = noteLength / 80;
        }
        else
        {
            result = 0.25f;
        }

        return result;
    }
}
