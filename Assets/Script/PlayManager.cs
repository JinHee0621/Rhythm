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

    private void Start()
    {
        selectMusicManager = GameObject.Find("SelectMusicManager").GetComponent<SelectMusicManager>();
        musicName = selectMusicManager.music_Name;
        StartCoroutine(FadeOutFirst());
    }


    IEnumerator FadeOutFirst()
    {
        fadeOut_screen.enabled = true;
        fadeOut_screen.color = new Color(0f, 0f, 0f, 1f);
        yield return new WaitForSeconds(3.0f);
        fadeOut_screen.DOColor(new Color(0f,0f,0f,0f), 1.0f);
        fadeOut_screen.enabled = false;
        SoundManager.instance.PlayBgm(true);
    }
}
