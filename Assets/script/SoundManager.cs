using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : PersistentSingleton<SoundManager>
{
    [SerializeField] float Volume;
    [Header("BGM")]
    [SerializeField] AudioClip bgmcilp;
    AudioSource bgmPlayer;
    [Header("SFX")]
    [SerializeField] AudioClip sfxcilp;
    public int channels;
    int channelIndex;
    AudioSource[] sfxPlayer;

    private void Start()
    {
        Init();
    }
    void Init()
    {
        GameObject bgmObj = new GameObject("BgmPlayer");
        bgmObj.transform.parent = transform;
        bgmPlayer = bgmObj.AddComponent<AudioSource>();


    }
}
