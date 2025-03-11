using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicListManager : MonoBehaviour
{
    private Dictionary<int, string> musicList = new Dictionary<int, string>();
    public int current_music = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectMusic()
    {
        Debug.Log(current_music);
    }

    public void InitMusicIndex()
    {

    }

}
