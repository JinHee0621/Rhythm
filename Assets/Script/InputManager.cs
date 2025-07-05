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
        noteRes = noteMoveManager.speed;
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
}
