using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public fadeInOut fade;
    public GameObject Rankingboard;
    public Button LoadButton;
    public GameObject Soundpanel;
    public GameObject volume;
    GameObject[] volumeEnergy;
    void Start()
    {
        AdManager.Instance.ShowBanner();
        volumeEnergy = new GameObject[volume.transform.childCount];
        for (int i = 0; i < volume.transform.childCount; i++)
        {
            volumeEnergy[i] = volume.transform.GetChild(i).gameObject;
        }
        if (GameManager.Instance.GameOver == true)
        {
            LoadButton.interactable = false;
        }
        else
        {
            LoadButton.interactable = true;
        }
    }

    public void NewGameBT()
    {
        GameManager.Instance.loadSetGameData(null);
        GameManager.Instance.Loadch = false;// ?????????? ????
        fade.fadoutScene(1);
        GameManager.Instance.gamestart = true;
    }
    public void LoadGameBT()
    {
        GmaeSaveData gmaeSaveData = SaveLoad.LoadGame();
        if (gmaeSaveData != null)
        {
            GameManager.Instance.Loadch = true;
            GameManager.Instance.loadSetGameData(gmaeSaveData);
            fade.fadoutScene(1);
        }
        GameManager.Instance.gamestart = true;
    }
    public void ExitGameBT()
    {
        Application.Quit();
    }

    public void RankingOpenBT()
    {
        Rankingboard.SetActive(true);
    }
    public void RankingCloseBT()
    {
        Rankingboard.SetActive(false);
    }

    public void OpenSoundPanel()
    {
        Soundpanel.SetActive(true);
        for (int i = 0; i < GameManager.Instance.volumeEnergyIndex; i++)
        {
            volumeEnergy[i].SetActive(true);
        }
    }
    public void CloseSoundPanel()
    {
        Soundpanel.SetActive(false);
    }
    public void volumeUPBT()//家府 农扁包府滚瓢
    {
        if (GameManager.Instance.volumeEnergyIndex < volume.transform.childCount)
        {
            volumeEnergy[GameManager.Instance.volumeEnergyIndex].SetActive(true);
            GameManager.Instance.volumeEnergyIndex++;
            SaveLoad.BasicSaveGame();
        }
        SoundManager.Instance.VolumeChange(GameManager.Instance.volumeEnergyIndex);
    }
    public void volumeDOWNBT()
    {
        if (GameManager.Instance.volumeEnergyIndex > 0)
        {
            GameManager.Instance.volumeEnergyIndex--;
            volumeEnergy[GameManager.Instance.volumeEnergyIndex].SetActive(false);
            SaveLoad.BasicSaveGame();
        }
        SoundManager.Instance.VolumeChange(GameManager.Instance.volumeEnergyIndex);
    }

}
