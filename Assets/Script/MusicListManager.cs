using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MusicListManager : MonoBehaviour
{
    public SelectMusicManager selectMusicManager;
    //private Dictionary<int, string> musicDic = new Dictionary<int, string>();
    public Text select_Music_Name;
    public Text select_Music_Score;
    public Text select_Music_Diff;
    public Text select_Music_Acc;
       
    public int current_music = 0;
    public GameObject musicListCont;
    public List<MusicElement> musicList = new List<MusicElement>();
    private bool moveList = false;

     void Start()
    {
        InitMusicList();
        SetMusicInfo(musicList[0]);
    }

    void Update()
    {
        //Select Music when push arrow button 
        if (Input.GetKey(KeyCode.UpArrow) == true)
        {
            SelectBeforeMusic();
        } else if(Input.GetKey(KeyCode.DownArrow) == true)
        {
            SelectNextMusic();
        } else if(Input.GetKeyDown(KeyCode.Return))
        {
            SelectMusic();
        }
    }

    public void InitMusicList()
    {
        float nextYPos = 0f;
        for (int i = 0; i < musicList.Count; i++)
        {
            //music position first init 50f,0f,0f -> I don't know why..
            musicList[i].gameObject.transform.localPosition = new Vector3(50f, nextYPos, 0f);
            nextYPos -= 90f;
        }
    }

    public void SelectBeforeMusic()
    {
        if(!moveList)
        {
            if (current_music > 0)
            {
                current_music -= 1;
                musicListCont.transform.DOLocalMoveY(musicListCont.transform.localPosition.y - 180f, 0.5f);
            }
            else current_music = 0;
            moveList = true;
            StartCoroutine(MoveMusicList());
        }
    }

    public void SelectNextMusic()
    {
        if(!moveList)
        {
            if (current_music < (musicList.Count - 1))
            {
                current_music += 1;
                musicListCont.transform.DOLocalMoveY(musicListCont.transform.localPosition.y + 180f, 0.5f);
            }
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
        curr.transform.DOLocalMove(new Vector3(-50f, curr.transform.localPosition.y, curr.transform.localPosition.z),1.5f);
        MoveOtherMusicPos();
    }

    public void MoveOtherMusicPos()
    {
        for (int i = 0; i < musicList.Count; i++)
        {
            if(i != current_music)
            {
                if(Math.Abs(i - current_music) == 2)
                {
                    musicList[i].transform.DOLocalMove(new Vector3(75f, musicList[i].transform.localPosition.y, musicList[i].transform.localPosition.z), 1.5f);
                } else if(Math.Abs(i - current_music) == 1)
                {
                    musicList[i].transform.DOLocalMove(new Vector3(45f, musicList[i].transform.localPosition.y, musicList[i].transform.localPosition.z), 1.5f);
                } else
                {
                    musicList[i].transform.DOLocalMove(new Vector3(105f, musicList[i].transform.localPosition.y, musicList[i].transform.localPosition.z), 1.5f);
                } 
            }
        }
    }

    IEnumerator MoveMusicList()
    {
        //Update Method is too fast, so put 0.5 sec delay 
        MusicElement curr = musicList[current_music];
        SetMusicInfo(curr);
        yield return new WaitForSeconds(0.5f);
        moveList = false;
    }

    public void SelectMusic()
    {
        GameObject selected = musicList[current_music].gameObject;
        StartCoroutine(SelectAnim(selected));
    }

    IEnumerator SelectAnim(GameObject selected)
    {
        selected.transform.DOLocalMove(new Vector3(-60f, selected.transform.localPosition.y, selected.transform.localPosition.z), 0.25f);
        yield return new WaitForSeconds(0.25f);
        selected.transform.DOLocalMove(new Vector3(350f, selected.transform.localPosition.y, selected.transform.localPosition.z), 0.75f);
        SelectMusicManager.Instance.SelectMusic(select_Music_Name.text, select_Music_Score.text);
    }

}
