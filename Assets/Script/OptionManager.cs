using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionManager : MonoBehaviour
{
    public static OptionManager instance;

    public float frameRate = 60f;
    public float musicVolume;
    public float sfxVolume;
    public float noteSpeed = 1f;
    public float userSync;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
    }

    private void Update()
    {
        //InputBtn1(Input.GetKey(inputBtnKey1));
        if (Input.GetKeyDown(KeyCode.LeftBracket))
        {
            if (noteSpeed > 1f)
            {
                noteSpeed -= 0.1f;
            } else
            {
                noteSpeed = 1f;
            }

        }

        if(Input.GetKeyDown(KeyCode.RightBracket))
        {
            if(noteSpeed < 3f)
            {
                noteSpeed += 0.1f;
            } else
            {
                noteSpeed = 3f;
            }
        }

    }
}
