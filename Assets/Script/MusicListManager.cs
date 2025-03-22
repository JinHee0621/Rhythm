using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicListManager : MonoBehaviour
{
    //private Dictionary<int, string> musicDic = new Dictionary<int, string>();
    public Text select_Music_Name;
    public Text select_Music_Score;
    public Text select_Music_Diff;
    public Text select_Music_Acc;
       
    public int current_music = 0;
    public List<MusicElement> musicList = new List<MusicElement>();
    private bool moveList = false;

     void Start()
    {
        SetMusicInfo(musicList[0]);
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow) == true)
        {
            SelectBeforeMusic();
        } else if(Input.GetKey(KeyCode.DownArrow) == true)
        {
            SelectNextMusic();
        }
    }

    public void SelectBeforeMusic()
    {
        if(!moveList)
        {
            if (current_music > 0) current_music -= 1;
            else current_music = 0;
            moveList = true;
            StartCoroutine(MoveMusicList());
        }
    }

    public void SelectNextMusic()
    {
        if(!moveList)
        {
            if (current_music < (musicList.Count - 1)) current_music += 1;
            else current_music = (musicList.Count - 1);
            moveList = true;
            StartCoroutine(MoveMusicList());
        }
    }

    public void SetMusicInfo(MusicElement curr)
    {
        select_Music_Name.text = curr.music_name;
        select_Music_Diff.text = curr.difficulty.ToString();
        select_Music_Acc.text = curr.accuracy.ToString() + "%";
        select_Music_Score.text = curr.music_score.ToString();
    }


    IEnumerator MoveMusicList()
    {
        MusicElement curr = musicList[current_music];
        SetMusicInfo(curr);
        yield return new WaitForSeconds(0.5f);
        moveList = false;
    }

}
