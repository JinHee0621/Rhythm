 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionManager : MonoBehaviour
{
    public static OptionManager instance;

    public int frameRate = 60;
    public float musicVolume;
    public float sfxVolume;
    public float noteSpeed = 1f;
    public float userSync;

    public bool currentInGame = false;

    public string gameType = "4K";
    [SerializeField]
    private LoadRecordDataManager loadRecordDataManager;

    [SerializeField]
    private NoteMoveManager noteMoveManager;


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
        if (Input.GetKeyDown(KeyCode.LeftBracket))
        {
            ChangeSpeed(false);
        }

        if(Input.GetKeyDown(KeyCode.RightBracket))
        {
            ChangeSpeed(true);
        }
    }

    public void ChangeSpeed(bool upSpeed)
    {

        if (upSpeed)
        {
            if (noteSpeed < 3f)
            {
                noteSpeed += 0.1f;
            }
            else
            {
                noteSpeed = 3f;
            }
        } else
        {
            if (noteSpeed > 1f)
            {
                noteSpeed -= 0.1f;
            }
            else
            {
                noteSpeed = 1f;
            }
        }

        if(currentInGame)
        {
            noteMoveManager.speed = noteSpeed;
            loadRecordDataManager.ResetNoteBySpeed();
        }
    }

    public void ChageInGame(bool inGame)
    {
        currentInGame = inGame;
        if(currentInGame)
        {
            loadRecordDataManager = GameObject.Find("LoadManager").GetComponentInChildren<LoadRecordDataManager>();
            noteMoveManager = GameObject.Find("NoteLine_Base").GetComponent<NoteMoveManager>();
        }
    }
}
