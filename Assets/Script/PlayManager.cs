using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayManager : MonoBehaviour
{
    [Header("Move")]
    public NoteMoveManager noteMoveManager;

    [Header("Music")]
    public SelectMusicManager selectMusicManager;
    //public MusicElement currentTrack;
    public AudioClip currentMusic;

    [Header("Play")]
    public Image fadeOut_screen;

    [Header("Test")]
    public bool test_play;

    private void Start()
    {
        if(!test_play)
        {
            //selectMusicManager = GameObject.Find("SelectMusicManager").GetComponent<SelectMusicManager>();
            currentMusic = SelectMusicManager.instance.select_audio;
        }

        StartCoroutine(FadeOutFirst());
    }


    IEnumerator FadeOutFirst()
    {
        yield return new WaitForSeconds(1.0f);
        fadeOut_screen.DOColor(new Color(0f, 0f, 0f, 0f), 3.0f);
        yield return new WaitForSeconds(4f);
        noteMoveManager.FirstPosWithSpeed();
        noteMoveManager.running = true;
        SoundManager.instance.SetBgm(currentMusic);
        StartCoroutine(Delay5SecondPlay());
    }

    IEnumerator Delay5SecondPlay()
    {
        yield return new WaitForSeconds(5.0f);
        SoundManager.instance.PlayBgm(true);
    }
}
