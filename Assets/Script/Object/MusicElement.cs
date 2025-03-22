using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicElement : MonoBehaviour
{
    public string music_name;
    public int difficulty;
    public float accuracy;
    public int music_score;

    public Sprite music_image;

    public void MusicInit(string name, int diff, float acc, int score, string image_name)
    {
        this.music_name = name;
        this.difficulty = diff;
        this.accuracy = acc;
        this.music_score = score;
    }
}
