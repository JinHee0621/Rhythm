using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    public static OptionManager instance;

    public GameObject optionSet;
    public GameObject optionCursor;
    public int cursorPointer;

    private float[] cursor1MovePoint = { 85f, -40f, -165f};
    private float[] cursor2MovePoint = { };
    private float[] cursor3MovePoint = { };


    public int frameRate = 60;
    public float musicVolume;
    public float sfxVolume;
    public float noteSpeed;
    public float userSync;

    public bool currentOption;
    public bool currentInGame;

    public string gameType = "4K";
    [SerializeField]
    private LoadRecordDataManager loadRecordDataManager;

    [SerializeField]
    private NoteMoveManager noteMoveManager;

    [Header("Key Setting")]
    public KeyCode input4KBtnKey_1 = KeyCode.D;
    public KeyCode input4KBtnKey_2 = KeyCode.F;
    public KeyCode input4KBtnKey_3 = KeyCode.J;
    public KeyCode input4KBtnKey_4 = KeyCode.K;


    private void Awake()
    {
        currentOption = false;
        currentInGame = false;
        cursorPointer = 0;

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
        noteSpeed = 1.5f;
    }

    private void Update()
    {
        /*
        KeyCode keyCode = DetectPressedKeyCode();
        if (keyCode != KeyCode.None)
        {
            Debug.Log(keyCode);
        }*/
        if(currentOption)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                currentOption = false;
            }

            if (Input.GetKey(KeyCode.UpArrow) == true)
            {

            }
            else if (Input.GetKey(KeyCode.DownArrow) == true)
            {

            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {
            }



            if (Input.GetKeyDown(KeyCode.LeftBracket))
            {
                ChangeSpeed(false);
            }

            if (Input.GetKeyDown(KeyCode.RightBracket))
            {
                ChangeSpeed(true);
            }

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                ReloadGame();
            }
        } else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                currentOption = true;
            }
        }
    }

    public void MoveOptionCursor(int type)
    {
        //type : 0 = up, 1 = down, 2 = return
        if(type == 0)
        {

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
