using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissLineManager : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.name.Equals("NoteTopPosition"))
        {
            collision.GetComponentInParent<NoteManager>().NoteCheckCurrect(false, 100f);
        }
    }
}
