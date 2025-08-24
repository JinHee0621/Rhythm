using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SelectMusicManager : MonoBehaviour
{
    //public string music_Name;
    //public string music_Score;
    public int music_index = 0;
    public MusicElement select_track;
    public string select_name;
    public AudioClip select_audio;

    public int score;
    public float accuracy;
    public bool isPlayed;

    public static SelectMusicManager instance { get; set; }

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

    public void SetMusic(MusicElement target, int index, int currScore, float currAcc)
    {
        instance.music_index = index;
        instance.select_name = target.music_name;
        instance.select_audio = target.music;
        instance.score = currScore;
        instance.accuracy = currAcc;
    }

    public void CheckResult(int currScore, float currAcc)
    {
        if(instance.score < currScore)
        {
            instance.score = currScore;
        }

        if(instance.accuracy < currAcc)
        {
            instance.accuracy = currAcc;
        }

        isPlayed = true;
    }

    public void SelectMusic()
    {
        SceneManager.LoadScene("4KGame");
    }
}
