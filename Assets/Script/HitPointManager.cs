using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPointManager : MonoBehaviour
{
    public Transform Line;
    public GameObject ParticleEffect;

    private bool LongKeyPushed = false;
    private bool checkEffect = false;

    public bool HitEffect(Transform NotePosition, bool isLongNote)
    {
        if(!checkEffect)
        {
            // Before Check Note
            Debug.Log(NotePosition.position.y - Line.position.y);
            if (NotePosition.position.y - Line.position.y < 1.5f && NotePosition.position.y - Line.position.y > -1.5f)
            {
                ParticleEffect.SetActive(true);
                if (isLongNote) LongKeyPushed = isLongNote;
                checkEffect = true;
                return true;
            } else
            {
                // Miss Note
                checkEffect = true;
                return false;
            }
        } else
        {
            //After Check Note
            if (LongKeyPushed) return true;
            //이거 해결
            else return false;
        }
    }

    public bool CheckCurrect(Transform NotePosition, bool isLongNote, bool isHit)
    {

        if(!isLongNote) StartCoroutine(OffEffect());
        else ParticleEffect.SetActive(false);

        if(isHit)
        {
            if (NotePosition.position.y - Line.position.y < 1.5f && NotePosition.position.y - Line.position.y > -1.5f)
            {
                Debug.Log("Hit");
                LongKeyPushed = false;
                return true;
            }
            else
            {
                Debug.Log("Miss");
                LongKeyPushed = false;
                return false;
            }
        } else
        {
            Debug.Log("Miss");
            LongKeyPushed = false;
            return false;
        }
    }
    
    IEnumerator OffEffect()
    {
        yield return new WaitForSeconds(0.5f);
        ParticleEffect.SetActive(false);
    }
}
