using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicElement : MonoBehaviour
{
    public int music_id;

    public string music_name;

    public string difficultyType;
    public int difficultyIdx;
    public int difficulty;

    public float accuracy;
    public int music_score;
    public Image music_cover;

    public Sprite coverImage;
    public AudioClip music;

    private string[] accuracyType = { "Easy", "Hard", "Very Hard" };

    private void Start()
    {
        music_cover.sprite = coverImage;
        //Debug.Log(gameObject.transform.GetChild(0).transform.GetChild(0).name);
    }

    public void MusicInit(int id, string name, int diffType, int diff, float acc, int score)
    {
        music_id = id;
        music_name = name;
        difficultyIdx = diffType;
        difficultyType = accuracyType[difficultyIdx];
        difficulty = diff;
        accuracy = acc;
        music_score = score;
        LoadCover();
        LoadAudio();
    }

    public void LoadCover()
    {
        coverImage = Resources.Load("Sprite/MusicImg/" + music_name, typeof(Sprite)) as Sprite;
        music_cover.sprite = coverImage;
    }

    public void LoadAudio()
    {
        music = Resources.Load("Music/BGM/" + music_name, typeof(AudioClip)) as AudioClip;
    }
}
