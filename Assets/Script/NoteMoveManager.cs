using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteMoveManager : MonoBehaviour
{
    public float speed; 
    private float yMove;
    private float default_pos;

    public bool perse = false;
    public bool running;
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
        if(running)
        {
            RunningGame();
        }

        //T Key : Pause Game
        if(Input.GetKeyDown(KeyCode.T) && running == true)
        {
            running = false;
        }
        else if(Input.GetKeyDown(KeyCode.T) && running == false)
        {
            StartCoroutine(Wait5Sceond());
        }
    }


    public void RunningGame()
    {
        this.transform.Translate(new Vector3(0f, yMove));
    }

    public float Print_default_pos()
    {
        return default_pos;
    }

    IEnumerator Wait5Sceond()
    {
        int waitTime = 0;
        for(waitTime = 0; waitTime < 5; waitTime++)
        {
            yield return new WaitForSecondsRealtime(1f);
        }
        running = true;
    }

}
