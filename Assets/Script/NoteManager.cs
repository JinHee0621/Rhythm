using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    public ScoreManager scoreManager;
    
    //[SerializeField]
    //private float hitTime = 0f;
    public bool isRecordNote;
    public bool isLongNote;
    public float noteLength = 0f;
    

    [SerializeField]
    private bool noteChecked = false;

    private bool longhitNote = false;
    private bool hitNote = false;

    private void Start()
    {
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        SetNoteState();
        if (gameObject.transform.GetChild(1).transform.position.y - gameObject.transform.GetChild(0).transform.position.y == 0.25f)
        {
            isLongNote = false;
        } else
        {
            isLongNote = true;
        }
    }
    public void SetNoteState()
    {
        Vector3 reSizeVec = new Vector3(1.25f, 0.25f, 1f);
        if (noteLength >= 50) reSizeVec.y = 0.02f * noteLength; 
        gameObject.transform.localScale = reSizeVec;

        Vector3 reSetPosition = new Vector3(0f, reSizeVec.y / 2, 0f);
        //Debug.Log(reSetPosition);
        gameObject.transform.localPosition = reSetPosition;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(!isRecordNote)
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
        if (!isRecordNote)
        {
            if (noteChecked)
            {
                if(hitNote)
                {
                    if (collision.tag.Equals("BtnLine"))
                    {
                        Debug.Log(collision.gameObject.transform);
                        CheckCurrect(collision.gameObject.GetComponent<HitPointManager>().CheckCurrect(gameObject.transform, isLongNote));
                    }
                }
                            
                if (collision.tag.Equals("MissLine"))
                {
                    if(!isLongNote)
                    {
                        CheckCurrect(false);
                    } else
                    {
                        if(!hitNote) CheckCurrect(false);
                    }
                }
            }
        }
    }

    void CheckCurrect(bool isHit)
    {
        if (isHit)
        {
            scoreManager.AddScore();
            scoreManager.AddCombo();
            gameObject.SetActive(false);
        } else
        {
            scoreManager.ResetCombo();
        }
    }
}
