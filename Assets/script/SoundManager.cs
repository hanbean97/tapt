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
    public int channels;//���ÿ� �Ҹ��� ���ü��ִ� �ִ��
    int channelIndex;
    AudioSource[] sfxPlayer;

    private void Start()
    {
        Init();
    }
    void Init()
    {
        // ����� �÷��̾� �ʱ�ȭ
        GameObject bgmObj = new GameObject("BgmPlayer");
        bgmObj.transform.parent = transform;
        bgmPlayer = bgmObj.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.volume = Volume;
        // ȿ���� �÷��̾� �ʱ�ȭ
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
}
