using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SoundManager : PersistentSingleton<SoundManager>
{
    [Header("BGM")]
    [SerializeField] AudioClip[] bgmcilp;
    AudioSource bgmPlayer;
    [Header("SFX")]
    [SerializeField] AudioClip[] sfxcilp;
    public int channels;
    int channelIndex;
    AudioSource[] sfxPlayer;
    float onekan =0.14f;

    public enum Bgm { StartScreen,Loding,Boss,Usually };
    public enum Sfx { LevelUp, Attack, Laser ,Die, Talk,Select,BSave,FailB}

    private void Start()
    {
        Init();
        VolumeChange(GameManager.Instance.volumeEnergyIndex);
        changeBGM(Bgm.StartScreen);
    }

    void Init()
    {
        float Volume = GameManager.Instance.volumeEnergyIndex * onekan;
        GameObject bgmObj = new GameObject("BgmPlayer");
        bgmObj.transform.parent = transform;
        bgmPlayer = bgmObj.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.volume = Volume;
        bgmPlayer .clip = bgmcilp[0];
    
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
        int LoopIndex=0;
        for (int index =0; index < sfxPlayer.Length; index++)
        {
            LoopIndex = (index + channelIndex) % sfxPlayer.Length;
            if (sfxPlayer[LoopIndex].isPlaying)
                continue;

            channelIndex = LoopIndex;
            sfxPlayer[LoopIndex].clip = sfxcilp[(int)sfx];
            sfxPlayer[LoopIndex].Play();
            break;
        }
    }
    public void changeBGM(Bgm bgm)
    {
        bgmPlayer.clip = bgmcilp[(int)bgm];
        bgmPlayer.Play();
    }
    public Bgm NowBGM()
    {
        for(int i=0; i< bgmcilp.Length;i++)
        {
            if(bgmPlayer.clip == bgmcilp[i])
            {
                return (Bgm)i;
            }
        }
        return Bgm.StartScreen;
    }

    public void StopBGMToggle(bool Onoffes)
    {
        if (Onoffes == true)
        {
            bgmPlayer.Play();
        }
        else
        {
            bgmPlayer.Stop();
        }
    }
    public void VolumeChange(int volume)
    {
        bgmPlayer.volume = (volume*onekan);
        for (int i = 0; i < sfxPlayer.Length; i++)
        {
            sfxPlayer[i].volume = (volume * onekan);
        }
    }

}
