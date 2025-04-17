using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SelectMusicManager : MonoBehaviour
{
    public string music_Name;
    public string music_Score;

    public static SelectMusicManager Instance { get; set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }

    public void SelectMusic(string select_music_Name, string select_music_score)
    {
        music_Name = select_music_Name;
        music_Score = select_music_score;
        SceneManager.LoadScene("4KGame");
    }
}
