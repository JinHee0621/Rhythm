using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionManager : MonoBehaviour
{
    public static OptionManager instance;

    public int frameRate = 60;
    public float musicVolume;
    public float sfxVolume;
    public float noteSpeed = 1.5f;
    public float userSync;

    public bool currentInGame = false;

    public string gameType = "4K";
    [SerializeField]
    private LoadRecordDataManager loadRecordDataManager;

    [SerializeField]
    private NoteMoveManager noteMoveManager;

    [Header("Key Setting")]
    public KeyCode input4KBtnKey_1 = KeyCode.A;
    public KeyCode input4KBtnKey_2 = KeyCode.S;
    public KeyCode input4KBtnKey_3 = KeyCode.Semicolon;
    public KeyCode input4KBtnKey_4 = KeyCode.Quote;


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
        /*
        KeyCode keyCode = DetectPressedKeyCode();
        if (keyCode != KeyCode.None)
        {
            Debug.Log(keyCode);
        }*/

        if (Input.GetKeyDown(KeyCode.LeftBracket))
        {
            ChangeSpeed(false);
        }

        if(Input.GetKeyDown(KeyCode.RightBracket))
        {
            ChangeSpeed(true);
        }

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            ReloadGame();
        }
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ChangeSpeed(bool upSpeed)
    {

        if (upSpeed)
        {
            if (noteSpeed < 2.5f)
            {
                noteSpeed += 0.1f;
            }
            else
            {
                noteSpeed = 2.5f;
            }
        } else
        {
            if (noteSpeed > 0.5f)
            {
                noteSpeed -= 0.1f;
            }
            else
            {
                noteSpeed = 0.5f;
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

    public void KeySetting()
    {
            KeyCode keyCode = DetectPressedKeyCode();
    if (keyCode != KeyCode.None)
    {
    	Debug.Log(keyCode);	// 감지된 키코드를 로그로 남긴다.
    }
    }

    private KeyCode DetectPressedKeyCode()
    {
        foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(kcode))
            {
                return kcode;
            }
        }
        return KeyCode.None;
    }
}
