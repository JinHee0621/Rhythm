using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public NotePoolManager notePoolManager;
    public ScoreManager scoreManager;

    //[SerializeField]
    //private float hitTime = 0f;
    public int noteId;
    public bool isRecordNote;
    public bool isLongNote;
    public float noteLength = 0.25f;

    public int lineNum = 0;
    public bool isChecked = false;
    public bool longhitNote = false;


    private void Start()
    {
        notePoolManager = GameObject.Find("NotePoolManager").GetComponent<NotePoolManager>();
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        SetNoteState();
    }

    public void SetNoteState()
    {
        Vector3 reSizeVec = new Vector3(1.25f, noteLength, 1f);

        gameObject.transform.localScale = reSizeVec;

        Vector3 reSetPosition = new Vector3(0f, reSizeVec.y / 2, 0f);
        gameObject.transform.localPosition = reSetPosition;
        if (gameObject.transform.GetChild(1).transform.position.y - gameObject.transform.GetChild(0).transform.position.y == 0.25f)
        {
            isLongNote = false;
        }
        else
        {
            isLongNote = true;
        }
    }


    /*
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!isRecordNote && !collision.tag.Equals("Note"))
        {
            if (collision.tag.Equals("MissLine"))
            {
                if (!isLongNote)
                {
                    NoteCheckCurrect(false, 100f);
                }
                else
                {
                    if (!longhitNote)
                    {
                        NoteCheckCurrect(false, 100f);
                    }
                }
            }
        }
    }*/

    public bool RayHit(bool isHit, float distance)
    {
        bool hitResult = false;
        hitResult = NoteCheckCurrect(isHit, distance);
        return hitResult;
    }

    public bool RayLongHit(bool isHit, bool isInput, float distance)
    {
        bool hitReuslt = true;
        if(isInput && !longhitNote)
        {
            longhitNote = true;
        } else if(!isInput && longhitNote)
        {
            hitReuslt = NoteCheckCurrect(isHit, distance);
        }
        return hitReuslt;
    }
   
    public bool NoteCheckCurrect(bool isHit, float range)
    {
        bool hitResult = false;
        float hitAcc = scoreManager.CheckAccuracy(range);
        scoreManager.AddScore(hitAcc);
        if (hitAcc != 0)
        {
            scoreManager.AddCombo();
            hitResult = true;
        } else
        {
            scoreManager.ResetCombo();
        }
        notePoolManager.Check(transform.parent.gameObject);
        return hitResult;
    }

    public void ReInitNote()
    {
        lineNum = 0;
        isChecked = false;
        isLongNote = false;
        longhitNote = false;
    }

}
