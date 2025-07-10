using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SelectMusicManager : MonoBehaviour
{
    public string music_Name;
    public string music_Score;

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

    public void SelectMusic(string select_music_Name, string select_music_score)
    {
        instance.music_Name = select_music_Name;
        instance.music_Score = select_music_score;
        SceneManager.LoadScene("4KGame");
    }

}
