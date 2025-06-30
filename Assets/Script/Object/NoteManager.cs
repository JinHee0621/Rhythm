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
    

    [SerializeField]
    private bool noteChecked = false;

    private bool longhitNote = false;
    private bool hitNote = false;

    private void Start()
    {
        notePoolManager = GameObject.Find("NotePoolManager").GetComponent<NotePoolManager>();
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        SetNoteState();
    }

    public void SetNoteState()
    {
        //Vector3 reSizeVec = new Vector3(1.25f, 0.25f, 1f)
        Vector3 reSizeVec = new Vector3(1.25f, noteLength, 1f);

        //if (noteLength >= 150) reSizeVec.y = 0.01f * noteLength; 
        gameObject.transform.localScale = reSizeVec;

        Vector3 reSetPosition = new Vector3(0f, reSizeVec.y / 2, 0f);
        //Debug.Log(reSetPosition);
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(!isRecordNote && !collision.tag.Equals("Note"))
        {
            //When Note Hit MissLine
            if(!noteChecked)
            {
                if (collision.tag.Equals("MissLine"))
                {
                    hitNote = false;
                    noteChecked = true;
                }
                else
                {
                    if(!isLongNote)
                    {
                        hitNote = collision.gameObject.GetComponent<HitPointManager>().HitEffect(gameObject.transform);
                        noteChecked = true;
                    } else
                    {
                        hitNote = collision.gameObject.GetComponent<HitPointManager>().LongHitEffect(gameObject.transform, longhitNote);
                        longhitNote = true;
                        noteChecked = true;
                    }
                }
            }
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!isRecordNote && !collision.tag.Equals("Note"))
        {
            if (noteChecked)
            {
                if(hitNote)
                {
                    if (collision.tag.Equals("BtnLine"))
                    {
                        //NoteCheckCurrect(collision.gameObject.GetComponent<HitPointManager>().CheckCurrect(gameObject.transform, isLongNote), (gameObject.transform.position.y));
                        if(!isLongNote)
                        {
                            //collision.enabled = false;
                            NoteCheckCurrect(collision.gameObject.GetComponent<HitPointManager>().CheckCurrect(gameObject.transform), (gameObject.transform.position.y));
                        } else
                        {
                            Transform noteEnd = gameObject.transform.GetChild(1).transform;
                            NoteCheckCurrect(collision.gameObject.GetComponent<HitPointManager>().LongCheckCurrect(gameObject.transform), (noteEnd.position.y));
                        }
                    }
                }
                            
                if (collision.tag.Equals("MissLine"))
                {
                    if(!isLongNote)
                    {
                        NoteCheckCurrect(false, 100f);
                    } else
                    {
                        if (!hitNote)
                        {
                            NoteCheckCurrect(false, 100f);
                        }
                    }
                }
            }
        }
    }
   
    void NoteCheckCurrect(bool isHit, float range)
    {
        float hitAcc = scoreManager.CheckAccuracy(range);
        if (hitAcc != 0)
        {
            scoreManager.AddScore(hitAcc);
            scoreManager.AddCombo();
        } else
        {
            scoreManager.ResetCombo();
        }
        notePoolManager.Check(transform.parent.gameObject);
    }

    public void ReInitNote()
    {
        isChecked = false;
        noteChecked = false;
        isLongNote = false;
        hitNote = false;
    }

}
