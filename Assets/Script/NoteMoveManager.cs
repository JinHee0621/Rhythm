using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteMoveManager : MonoBehaviour
{
    public float speed; 
    private float yMove;
    private float default_pos;
    //private float time = 0f;

    public bool perse = false;
    public bool running;
    // Start is called before the first frame update
    void Awake()
    {
        //speed *= -1;
        //yMove = speed * Time.deltaTime;
        default_pos = gameObject.transform.position.y;
        //Drop Moving
    }

    public void FirstPosWithSpeed()
    {
        default_pos = (default_pos + (9f * (speed - 1)) + (5f * (speed - 1))); // 8.6 : (First Load Gap) , 5 : ( Time By Move Gap ) 
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, default_pos, gameObject.transform.position.z);
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
        //this.transform.Translate(new Vector3(0f, yMove, 0f));
        this.transform.Translate(new Vector3(0f, -5f * speed, 0f) * Time.deltaTime);
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
