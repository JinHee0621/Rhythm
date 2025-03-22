using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicListManager : MonoBehaviour
{
    //private Dictionary<int, string> musicDic = new Dictionary<int, string>();
     
    public int current_music = 0;
    public List<MusicElement> musicList = new List<MusicElement>();
    void Start()
    {
        Debug.Log(musicList[0]);    
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow) == true)
        {
            if (current_music > 0) current_music -= 1;
            else current_music = 0;
            SelectMusic(current_music);
        } else if(Input.GetKey(KeyCode.DownArrow) == true)
        {
            if (current_music < musicList.Count) current_music += 1;
            else current_music = musicList.Count;
            SelectMusic(current_music);
        }
    }

    public void SelectMusic(int index)
    {
        Debug.Log(current_music);
    }

    public void InitMusicIndex()
    {

    }

}
