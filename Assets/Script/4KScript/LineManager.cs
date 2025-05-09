using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineManager : MonoBehaviour
{
    public bool lineRunning = false;


    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log(gameObject.name + ": " + collision.gameObject.name);
    }
}
