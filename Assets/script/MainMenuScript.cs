using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public fadeInOut fade;
    public GameObject Rankingboard;
    public Button LoadButton;
    void Start()
    {
        AdManager.Instance.ShowBanner();
    }

    void Update()
    {
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
}
