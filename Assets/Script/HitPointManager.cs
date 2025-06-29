using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPointManager : MonoBehaviour
{
    public bool longCheck = false;
    public Transform Line;
    public GameObject ParticleEffect;

    private bool checkEffect = false;

    public bool HitEffect(Transform Note)
    {
        if(!checkEffect)
        {
            Transform NotePosition = Note.GetChild(0).transform;
            // Before Check Note
            if (NotePosition.position.y - Line.position.y < 1.5f && NotePosition.position.y - Line.position.y > -1.5f)
            {
                ParticleEffect.SetActive(true);
                //checkEffect Value is unnecessary
                //checkEffect = true;
                return true;
            } else
            {
                // Miss Note
                return false;
            }
        } else
        {
            return false;
        }
    }

    public bool LongHitEffect(Transform Note, bool LongHit)
    {
        if (!checkEffect)
        {
            Transform NoteStartPosition = Note.GetChild(0).transform;
            // Before Check Note
            if(!LongHit)
            {
                if (NoteStartPosition.position.y - Line.position.y < 1.5f && NoteStartPosition.position.y - Line.position.y > -1.5f)
                {
                    ParticleEffect.SetActive(true);
                    //checkEffect = true;
                    longCheck = true;
                    return true;
                }
                else
                {
                    // Miss Note
                    longCheck = false;
                    return false;
                }
            } else
            {
                return true;
            }
        }
        else
        {
            return false;
        }
    }

    public bool CheckCurrect(Transform Note)
    {
        checkEffect = false;

        StartCoroutine(OffEffect());
        Transform NotePosition = Note.GetChild(0).transform;
        if (NotePosition.position.y - Line.position.y < 1.5f && NotePosition.position.y - Line.position.y > -1.5f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool LongCheckCurrect(Transform Note)
    {
        checkEffect = false;
        ParticleEffect.SetActive(false);
        Transform NoteEndPosition = Note.GetChild(1).transform;
        longCheck = false;
        if (NoteEndPosition.position.y - Line.position.y < 1.5f && NoteEndPosition.position.y - Line.position.y > -1.5f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    IEnumerator OffEffect()
    {
        yield return new WaitForSeconds(0.5f);
        ParticleEffect.SetActive(false);
    }

    /*
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject);
    }
    */
}
