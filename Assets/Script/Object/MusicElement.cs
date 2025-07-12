using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicElement : MonoBehaviour
{
    public string music_name;
    public int difficulty;
    public float accuracy;
    public int music_score;
    public Image music_cover;

    public Sprite coverImage;
    public AudioClip music;
    private void Start()
    {
        music_cover.sprite = coverImage;
        Debug.Log(gameObject.transform.GetChild(0).transform.GetChild(0).name);
    }

    public void MusicInit(string name, int diff, float acc, int score, string image_name)
    {
        this.music_name = name;
        this.difficulty = diff;
        this.accuracy = acc;
        this.music_score = score;
    }
}
