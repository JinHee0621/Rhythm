using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayManager : MonoBehaviour
{
    [Header("Music")]
    public SelectMusicManager selectMusicManager;
    public string musicName;

    [Header("Play")]
    public Image fadeOut_screen;

    [Header("Test")]
    public bool test_play;

    private void Start()
    {
        if(!test_play)
        {
            selectMusicManager = GameObject.Find("SelectMusicManager").GetComponent<SelectMusicManager>();
            musicName = selectMusicManager.music_Name;
        }

        StartCoroutine(FadeOutFirst());
    }


    IEnumerator FadeOutFirst()
    {
        yield return new WaitForSeconds(1.0f);
        fadeOut_screen.DOColor(new Color(0f, 0f, 0f, 0f), 3.0f);
        yield return new WaitForSeconds(3.6f);
        SoundManager.instance.PlayBgm(true);
    }
}
