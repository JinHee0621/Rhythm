using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class OptionManager : MonoBehaviour
{
    public static OptionManager instance;

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


}
