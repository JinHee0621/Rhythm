using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("BGM")]
    GameObject bgmObject;
    public AudioClip bgmClip;
    public float bgmVolume;
    AudioSource bgmPlayer;

    [Header("SFX")]
    GameObject sfxObject;
    public AudioClip[] sfxClips;
    public float sfxVolume;
    public int channels;
    AudioSource[] sfxPlayers;
    int channelIndex;

    [Header("Test")]
    public bool test_bgm;


    public enum Sfx { }

    private void Awake()
    {
        instance = this;
        Init();
        if(test_bgm)
        {
            PlayBgm(true);
        }
    }

    void Init()
    {
        if (OptionManager.instance != null)
        {
            bgmVolume = OptionManager.instance.musicVolume / 10f;
            sfxVolume = OptionManager.instance.sfxVolume / 10f;
        }

        //BGM Init
        bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;
        //bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;
        bgmPlayer.clip = bgmClip;

        //SFX init
        sfxObject = new GameObject("SfxPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channels];

        for(int index=0; index < sfxPlayers.Length; index++)
        {
            sfxPlayers[index] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[index].playOnAwake = false;
            sfxPlayers[index].volume = sfxVolume;
        }

    }

    public void ReInitVolume()
    {
        bgmPlayer.volume = bgmVolume;
        for(int index=0; index < sfxPlayers.Length; index++)
        {
            sfxPlayers[index].volume = sfxVolume;
        }
    }


    public void SetBgm(AudioClip target)
    {
        bgmPlayer.clip = target;
    }

    public void PlayBgm(bool isPlay)
    {
        if(isPlay)
        {
            bgmPlayer.Play();
        } else
        {
            bgmPlayer.Stop();
        }
    }

    public void FadePlayBgm(bool isPlay)
    {
        if(isPlay)
        {
            bgmPlayer.volume = 0f;
            bgmPlayer.Play();
            StopCoroutine(FadeOutBgm());
            StartCoroutine(FadeInBgm());
        } else
        {
            StopCoroutine(FadeInBgm());
            StartCoroutine(FadeOutBgm());
        }
    }

    IEnumerator FadeInBgm()
    {
        yield return new WaitForSeconds(0.25f);
        if (bgmPlayer.volume < bgmVolume)
        {
            bgmPlayer.volume += 0.05f;
            StartCoroutine(FadeInBgm());
        } else
        {
            bgmPlayer.volume = bgmVolume;
        }
    }

    IEnumerator FadeOutBgm()
    {
        yield return new WaitForSeconds(0.25f);
        if (bgmPlayer.volume > 0f)
        {
            bgmPlayer.volume -= 0.05f;
            StartCoroutine(FadeOutBgm());
        }
        else
        {
            bgmPlayer.volume = 0f;
            bgmPlayer.Stop();
        }
    }

    public void PauseBgm(bool isPause)
    {
        if (isPause)
        {
            bgmPlayer.Pause();
        }
        else
        {
            bgmPlayer.UnPause();
        }
    }


    public void PlaySfx(Sfx sfx)
    {
        for(int index=0; index<sfxPlayers.Length; index++)
        {
            int loopIndex = (index + channelIndex) % sfxPlayers.Length;

            if (sfxPlayers[loopIndex].isPlaying)
                continue;

            channelIndex = loopIndex;
            sfxPlayers[loopIndex].clip = sfxClips[(int)sfx];
            sfxPlayers[loopIndex].Play();
            break;
        }
    }
}
