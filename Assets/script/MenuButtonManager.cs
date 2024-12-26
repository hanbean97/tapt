using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonManager : MonoBehaviour
{
    public GameObject menu;
    public GameObject Soundsetting;
    public GameObject volume;
    public aeraT aera;
    public GameObject GameoverUI;

    GameObject[] volumeEnergy;
    public fadeInOut fadeInOut;
    // Start is called before the first frame update
    void Start()
    {
        volumeEnergy = new GameObject[volume.transform.childCount];
        for (int i = 0; i < volume.transform.childCount; i++)
        {
            volumeEnergy[i] = volume.transform.GetChild(i).gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    public void MenuCloseBT()
    {
        if (Soundsetting.activeSelf == true)
        {
            Soundsetting.gameObject.SetActive(false);
        }
        menu.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }
    public void MenuOpenBT()
    {
        menu.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }
    public void SoundsettingOpenBT()// 소리 설정이 켜질때 저장된 소리값만큼 표시
    {
        Soundsetting.gameObject.SetActive(true);
        for (int i = 0; i < GameManager.Instance.volumeEnergyIndex; i++)
        {
            volumeEnergy[i].SetActive(true);
        }
    }
    public void SoundsettingCloseBT()
    {
        Soundsetting.gameObject.SetActive(false);
    }
     
    public void MainMenuGoBT()
    {
        fadeInOut.gameObject.SetActive(true);
        fadeInOut.fadoutScene(0);
    }
    public void volumeUPBT()//소리 크기관리버튼
    {
        if (GameManager.Instance.volumeEnergyIndex < volume.transform.childCount)
        {
            volumeEnergy[GameManager.Instance.volumeEnergyIndex].SetActive(true);
            GameManager.Instance.volumeEnergyIndex++;
            SaveLoad.SaveGame();
        }
        SoundManager.Instance.VolumeChange(GameManager.Instance.volumeEnergyIndex);
    }
    public void volumeDOWNBT()
    {
        if (GameManager.Instance.volumeEnergyIndex > 0)
        {
            GameManager.Instance.volumeEnergyIndex--;
            volumeEnergy[GameManager.Instance.volumeEnergyIndex].SetActive(false);
            SaveLoad.SaveGame();
        }
        SoundManager.Instance.VolumeChange(GameManager.Instance.volumeEnergyIndex);
    }

    public void ReStartGame()// 블록과 체력을 맨처음 상태로 돌리고 그대로 시작
    {
        AdManager.Instance.ShowRewardedAd();
        GameManager.Instance.hp = GameManager.Instance.startHp;
        GameManager.Instance.GameOver = false;
        GameManager.Instance.gamestart = true;
        aera.aeraAllClear();
        SaveLoad.SaveGame();
        GameoverUI.SetActive(false);
    }
    public void NewGameStart()//게임 초기화후 시작화면으로
    {
        SaveLoad.SaveGame();
        fadeInOut.gameObject.SetActive(true);
        fadeInOut.fadoutScene(0);
    }
}
