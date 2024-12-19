using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : PersistentSingleton<SoundManager>
{
    [SerializeField] float Volume;
    [Header("BGM")]
    [SerializeField] AudioClip[] bgmcilp;
    AudioSource bgmPlayer;
    [Header("SFX")]
    [SerializeField] AudioClip[] sfxcilp;
    public int channels;//동시에 소리가 나올수있는 최대수
    int channelIndex;
    AudioSource[] sfxPlayer;

    public enum bgm { StartScreen,Boss};
    public enum Sfx { }

    private void Start()
    {
        Init();
    }

    void Init()
    {
        // 배경음 플레이어 초기화
        GameObject bgmObj = new GameObject("BgmPlayer");
        bgmObj.transform.parent = transform;
        bgmPlayer = bgmObj.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.volume = Volume;
        bgmPlayer .clip = bgmcilp[0];
    
        // 효과음 플레이어 초기화
        GameObject sfxObj = new GameObject("sfxPlayer");
        sfxObj.transform.parent = transform;
        sfxPlayer = new AudioSource[channels];
        for (int i = 0; i < sfxPlayer.Length; i++)
        {
            sfxPlayer[i] = sfxObj.AddComponent<AudioSource>();
            sfxPlayer[i].playOnAwake=false;
            sfxPlayer[i].volume = Volume;
        }
    }
    public void PlaySfx(Sfx sfx)
    {
        for(int index =0; index < sfxPlayer.Length; index++)
        {
            int LoopIndex = (index + channelIndex) % sfxPlayer.Length;
            if (sfxPlayer[LoopIndex].isPlaying)
                continue;

            channelIndex = LoopIndex;
            sfxPlayer[LoopIndex].clip = sfxcilp[(int)sfx];
            sfxPlayer[LoopIndex].Play();
            break;
        }
    }
    public void changeBGM()
    {
        bgmPlayer.clip = bgmcilp[0];
    }
    public void VolumeChange(int volume)
    {
        bgmPlayer.volume = volume;
    }
}
