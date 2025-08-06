using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MusicListManager : MonoBehaviour
{
    [Header("MusicObject")]
    public GameObject musicElement;

    [Header("UI")]
    public GameObject musicTrackObj;
    public Image musicTrackImage;
    public Image fadeOutScreen;

    public GameObject misicInfoObj;
    public Text select_Music_Name;
    public Text select_Music_Score;
    public Text select_Music_Diff;
    public Text select_Music_Acc;

    [Header("List")]
    public int current_music = 0;
    public GameObject musicListCont;
    public List<MusicElement> musicList = new List<MusicElement>();
    private bool moveList = false;

    void Start()
    {
        StartCoroutine(FadeIn());
        TrackRotate();
    }

    public void TrackObjAnim(MusicElement curr)
    {
        musicTrackObj.transform.localPosition = new Vector3(-250f, 0, 0f);
        StopCoroutine(FixTrackPos());
        musicTrackImage.sprite = curr.coverImage;
        musicTrackObj.transform.DOLocalMove(new Vector3(50f, 0f, 0f), 1.5f);
        StartCoroutine(FixTrackPos());
    }

    IEnumerator FixTrackPos()
    {
        yield return new WaitForSeconds(1.5f);
        musicTrackObj.transform.localPosition = new Vector3(50f, 0f, 0f);
    }

    public void TrackRotate()
    {
        musicTrackObj.transform.DORotate(new Vector3(0, 0, -360), 10f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1);
    }

    public void ListStart()
    {
        InitMusicList();
        current_music = SelectMusicManager.instance.music_index;
        SetMusicInfo(musicList[0]);
        StartCoroutine(MoveListFirst());
    }

    public void MusicListAdd(MusicData data)
    {
        GameObject addData = Instantiate(musicElement, musicListCont.transform);

        int id;
        int.TryParse(data.musicId, out id);

        int diffIdx;
        int.TryParse(data.musicDiffType, out diffIdx);

        int diff;
        int.TryParse(data.musicDiff, out diff);

        int score;
        int.TryParse(data.score, out score);

        addData.GetComponentInChildren<MusicElement>().MusicInit(id, data.musicName, diffIdx, diff, data.accuracy, score);
        musicList.Add(addData.GetComponentInChildren<MusicElement>());
    }

    IEnumerator MoveListFirst()
    {
        float moveY = musicListCont.transform.localPosition.y + ((current_music) * 180f);
        musicListCont.transform.DOLocalMoveY(moveY, 0.5f);
        SetMusicInfo(musicList[current_music]);
        yield return new WaitForSeconds(0.1f);
    }

    void Update()
    {
        //Select Music when push arrow button 
        if (Input.GetKey(KeyCode.UpArrow) == true)
        {
            SelectBeforeMusic();
        }
        else if (Input.GetKey(KeyCode.DownArrow) == true)
        {
            SelectNextMusic();
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            SelectMusic();
        }
    }

    public void InitMusicList()
    {
        float nextYPos = 0f;
        for (int i = 0; i < musicList.Count; i++)
        {
            musicList[i].gameObject.transform.localPosition = new Vector3(50f, nextYPos, 0f);
            nextYPos -= 90f;
        }
    }

    public void SelectBeforeMusic()
    {
        if (!moveList)
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
        if (!moveList)
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
        TrackObjAnim(curr);
        select_Music_Name.text = curr.music_name;
        select_Music_Diff.text = curr.difficulty.ToString();
        select_Music_Acc.text = curr.accuracy.ToString();
        select_Music_Score.text = curr.music_score.ToString();
        curr.transform.DOLocalMove(new Vector3(-50f, curr.transform.localPosition.y, curr.transform.localPosition.z), 1.5f);

        SoundManager.instance.PlayBgm(false);
        SoundManager.instance.SetBgm(curr.music);
        SoundManager.instance.FadePlayBgm(true);

        MoveOtherMusicPos();
    }

    public void MoveOtherMusicPos()
    {
        for (int i = 0; i < musicList.Count; i++)
        {
            if (i != current_music)
            {
                if (Math.Abs(i - current_music) == 2)
                {
                    musicList[i].transform.DOLocalMove(new Vector3(75f, musicList[i].transform.localPosition.y, musicList[i].transform.localPosition.z), 1.5f);
                }
                else if (Math.Abs(i - current_music) == 1)
                {
                    musicList[i].transform.DOLocalMove(new Vector3(45f, musicList[i].transform.localPosition.y, musicList[i].transform.localPosition.z), 1.5f);
                }
                else
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
        SelectMusicManager.instance.SetMusic(selected.GetComponent<MusicElement>(), current_music);
        StartCoroutine(SelectAnim(selected));
    }

    IEnumerator SelectAnim(GameObject selected)
    {
        selected.transform.DOKill();
        selected.transform.DOLocalMove(new Vector3(-60f, selected.transform.localPosition.y, selected.transform.localPosition.z), 0.25f);
        musicTrackObj.transform.DOLocalMove(new Vector3(60f, 0f, 0f), 0.25f);
        yield return new WaitForSeconds(0.25f);
        selected.transform.DOLocalMove(new Vector3(350f, selected.transform.localPosition.y, selected.transform.localPosition.z), 0.75f);
        musicTrackObj.transform.DOLocalMove(new Vector3(-350f, 0f, 0f), 0.75f);
        yield return new WaitForSeconds(0.75f);
        StartCoroutine(FadeOut());
        yield return new WaitForSeconds(1f);
        SelectMusicManager.instance.SelectMusic();
    }

    IEnumerator FadeOut()
    {
        fadeOutScreen.DOColor(new Color(0f, 0f, 0f, 1f), 1f);
        yield return new WaitForSeconds(1f);
    }

    IEnumerator FadeIn()
    {
        fadeOutScreen.color = new Color(0f,0f,0f,1f);
        fadeOutScreen.DOColor(new Color(0f, 0f, 0f, 0f), 1f);
        yield return new WaitForSeconds(1f);
    }
}
