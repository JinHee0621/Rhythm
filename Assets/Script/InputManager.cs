using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private float noteRes;
    public NoteMoveManager noteMoveManager;

    private float default_coll_size = 3f;

    void Start()
    {
        noteRes = noteMoveManager.speed / (4f);
        default_coll_size += noteMoveManager.speed;
    }

    IEnumerator ColorDisabled(SpriteRenderer target)
    {
        yield return new WaitForSeconds(0.16f);
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

    public void ColliderEnabled(BoxCollider2D coll)
    {
        coll.enabled = true;
        if (!coll.GetComponent<HitPointManager>().longCheck) coll.size = new Vector2(coll.size.x, 1f);
        StartCoroutine(ColliderResize(coll));
    }

    public void ColliderDisabled(BoxCollider2D coll)
    {
        coll.enabled = false;
    }


    IEnumerator ColliderResize(BoxCollider2D coll)
    {
        if (coll.size.y >= default_coll_size)
        {
            yield return new WaitForSeconds(0.016f);
            //When Player hit Long Note then collider is enabled
            if (!coll.GetComponent<HitPointManager>().longCheck) coll.enabled = false;
            yield return null;
        }
        else
        {
            float nextSizeY = coll.size.y;
            nextSizeY += noteRes;
            coll.size = new Vector2(coll.size.x, nextSizeY);
            yield return new WaitForSeconds(0.016f);
            StartCoroutine(ColliderResize(coll));
        }
    }

    //Change Collider -> Ray
    public void RayEnabled(BoxCollider2D coll)
    {
        GameObject collObj = coll.gameObject;
        Vector2 startPos = collObj.transform.position;
        // 2f -> speed by range
        RaycastHit2D hit = Physics2D.Raycast(startPos, Vector2.up, 2f, LayerMask.GetMask("Note"));
        if (hit.collider != null)
        {
            //Debug.DrawRay(startPos, Vector2.up * 2f, Color.red);
            float distance = hit.distance;
            int hitLine = hit.transform.GetComponentInChildren<NoteManager>().lineNum;
            bool hitResult = false;
            if (!hit.transform.GetComponentInChildren<NoteManager>().isLongNote)
            {
                hitResult = hit.transform.GetComponentInChildren<NoteManager>().RayHit(true, distance);
            } else
            {
                hitResult = hit.transform.GetComponentInChildren<NoteManager>().RayLongHit(true, true, distance);
            }
            if (hitResult) HitEffect(hitLine, false);
        }
    }

    public void LongRayEnabled(BoxCollider2D coll)
    {
        GameObject collObj = coll.gameObject;
        Vector2 startPos = collObj.transform.position;
        RaycastHit2D hit = Physics2D.Raycast(startPos, Vector2.up, 2f, LayerMask.GetMask("Note"));
        if (hit.collider != null)
        {
            if (hit.transform.GetComponentInChildren<NoteManager>().isLongNote)
            {
                int hitLine = hit.transform.GetComponentInChildren<NoteManager>().lineNum;
                if(hit.transform.GetComponentInChildren<NoteManager>().longhitNote)
                {
                    hit.transform.GetComponentInChildren<NoteManager>().LongNoteCombo();
                    HitEffect(hitLine, true);
                }
            }
        }
    }
     
    public void LongRayDisabled(BoxCollider2D coll)
    {
        GameObject collObj = coll.gameObject;
        Vector2 startPos = collObj.transform.position;
        RaycastHit2D hitTop = Physics2D.Raycast(startPos, Vector2.up, 2f, LayerMask.GetMask("NoteTop"));
        RaycastHit2D hit = Physics2D.Raycast(startPos, Vector2.up, 2f, LayerMask.GetMask("Note"));
        if (hitTop.collider != null && hit.collider != null)
        {
            if (hitTop.transform.GetComponentInParent<NoteManager>().isLongNote)
            {
                if (hitTop.transform.GetComponentInParent<NoteManager>().longhitNote)
                {
                    float distance = hitTop.distance;
                    HitEffect(hitTop.transform.GetComponentInParent<NoteManager>().lineNum, true);
                    hitTop.transform.GetComponentInParent<NoteManager>().RayLongHit(true, false, distance);
                }
            }
        }
        else if (hitTop.collider == null && hit.collider != null)
        {
            if (hit.transform.GetComponentInChildren<NoteManager>().isLongNote)
            {
                if (hit.transform.GetComponentInChildren<NoteManager>().longhitNote)
                {
                    float distance = 100f;
                    HitEffect(hit.transform.GetComponentInChildren<NoteManager>().lineNum, true);
                    hit.transform.GetComponentInChildren<NoteManager>().RayLongHit(true, false, distance);
                }
            }
        }

    }


    virtual public void HitEffect(int BtnNum, bool isLong)
    {
        Debug.Log("HitEffect");
    }

}