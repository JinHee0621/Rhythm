using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputRecordManager2K : MonoBehaviour
{
    public GameObject Note;

    KeyCode inputBtnKey1 = KeyCode.D;
    KeyCode inputBtnKey2 = KeyCode.F;

    public SpriteRenderer Effect1;
    public SpriteRenderer Effect2;

    public BoxCollider2D BtnCollider1;
    public BoxCollider2D BtnCollider2;

    public Animator Btn1Anim;
    public Animator Btn2Anim;

    public GameObject LineBase;
    public Transform Line1;
    public Transform Line2;
    public Transform SponePos1;
    public Transform SponePos2;

    float btn1Time = 0f;
    float btn2Time = 0f;

    bool Line1NoteSpone = false;
    bool Line2NoteSpone = false;
    bool recordEnd = false;

    ArrayList noteList = new ArrayList();
    int noteCnt = 0;

    GameObject newNote1 = null;
    GameObject newNote2 = null;

    void Update()
    {
        InputBtn1(Input.GetKey(inputBtnKey1));
        InputBtn2(Input.GetKey(inputBtnKey2));
        if(Input.GetKey(KeyCode.E))
        {
            if(!recordEnd)
            {
                Debug.Log("Record End" + LineBase.transform.position.y);
                Debug.Log("Default Length: " + LineBase.GetComponent<NoteMoveManager>().Print_default_pos());
                for(int i = 0; i < noteList.Count; i++)
                {
                    Debug.Log(noteList[i]);
                }
                recordEnd = true;
            }
        }
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
            }
        }
        else
        {
            if (btn1Time != 0f)
            {
                newNote1.GetComponentInChildren<NoteManager>().noteLength = btn1Time;
                newNote1.GetComponentInChildren<NoteManager>().SetNoteState();
                string noteInfo = "Btn1|" + newNote1.transform.localPosition.y + "|" + btn1Time;
                noteCnt += 1;
                noteList.Add(noteInfo);
                Line1NoteSpone = false;
            }
            BtnCollider1.enabled = false;
            Btn1Anim.SetBool("ButtonPush", false);
            StartCoroutine(ColorDisabled(Effect1));
            btn1Time = 0f;
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
        }
        else
        {
            if (btn2Time != 0f)
            {
                newNote2.GetComponentInChildren<NoteManager>().noteLength = btn2Time;
                newNote2.GetComponentInChildren<NoteManager>().SetNoteState();
                string noteInfo = "Btn2|" + newNote2.transform.localPosition.y + "|" + btn2Time;
                noteCnt += 1;
                noteList.Add(noteInfo);
                Line2NoteSpone = false;
            }

            BtnCollider2.enabled = false;
            Btn2Anim.SetBool("ButtonPush", false);
            StartCoroutine(ColorDisabled(Effect2));
            btn2Time = 0f;
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
}
