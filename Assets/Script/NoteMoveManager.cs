using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteMoveManager : MonoBehaviour
{
    public float speed; 
    private float yMove;
    private float default_pos;
    // Start is called before the first frame update
    void Awake()
    {
        speed *= -1;
        yMove = speed * Time.deltaTime;
        default_pos = gameObject.transform.position.y;
        //Drop Moving
    }
    // Update is called once per frame
    void Update()
    {   
        this.transform.Translate(new Vector3(0f, yMove));
    }

    public float Print_default_pos()
    {
        return default_pos;
    }
}
