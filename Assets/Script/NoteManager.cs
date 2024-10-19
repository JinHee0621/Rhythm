using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteManager : MonoBehaviour
{
    [SerializeField]
    private float hitTime = 0f;
    public bool isRecordNote;
    public bool isLongNote;
    public float noteLength = 0f;
    private bool hitNote = false;
    private void Awake()
    {
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
            if (!collision.tag.Equals("MissLine"))
            {
                hitNote = collision.gameObject.GetComponent<HitPointManager>().HitEffect(gameObject.transform.GetChild(0).transform, isLongNote);
                Debug.Log(hitNote);
            }
            else
            {
                if (!hitNote)
                {
                    hitNote = false;
                }
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(!isRecordNote)
        {
            if (!collision.tag.Equals("MissLine"))
            {
                CheckCurrect(collision.gameObject.GetComponent<HitPointManager>().CheckCurrect(gameObject.transform.GetChild(1).transform, isLongNote, hitNote));
            }
        }
    }

    void CheckCurrect(bool isHit)
    {
        if (isHit) gameObject.SetActive(false);
    }
}
