using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPointManager : MonoBehaviour
{
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
                checkEffect = true;
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
                    checkEffect = true;
                    return true;
                }
                else
                {
                    // Miss Note
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


    public bool CheckCurrect(Transform Note, bool isLongNote)
    {
        if (!isLongNote) StartCoroutine(OffEffect());
        else
        {
            ParticleEffect.SetActive(false);
        }

        checkEffect = false;

        if(!isLongNote)
        {
            Transform NotePosition = Note.GetChild(0).transform;
            if (NotePosition.position.y - Line.position.y < 1.5f && NotePosition.position.y - Line.position.y > -1.5f)
            {
                return true;
            }
            else
            {
                return false;
            }
        } else
        {
            Transform NoteEndPosition = Note.GetChild(1).transform;
            if (NoteEndPosition.position.y - Line.position.y < 1.5f && NoteEndPosition.position.y - Line.position.y > -1.5f)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    
    IEnumerator OffEffect()
    {
        yield return new WaitForSeconds(0.5f);
        ParticleEffect.SetActive(false);
    }
}
