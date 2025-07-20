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

    public void SetMusic(MusicElement target, int index)
    {
        instance.music_index = index;
        instance.select_name = target.music_name;
        instance.select_audio = target.music;
    }


    public void SelectMusic()
    {
        SceneManager.LoadScene("4KGame");
    }

}
