using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
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
    [SerializeField] GameObject RankRegist;
    [SerializeField] TMP_InputField nametextfield;
    // Start is called before the first frame update
    void Start()
    {
        volumeEnergy = new GameObject[volume.transform.childCount];
        for (int i = 0; i < volume.transform.childCount; i++)
        {
            volumeEnergy[i] = volume.transform.GetChild(i).gameObject;
        }
        //ÅØ½ºÆ® Æ¯Á¤¹®ÀÚ¸¸ ÀÔ·ÂÇÏ°Ô ÇÏ´Â ÄÚµå
        nametextfield.onValueChanged.AddListener((w) => nametextfield.text = Regex.Replace(w, @"[^0-9a-zA-Z°¡-ÆR]",""));
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
    public void SoundsettingOpenBT()// ¼Ò¸® ¼³Á¤ÀÌ ÄÑÁú¶§ ÀúÀåµÈ ¼Ò¸®°ª¸¸Å­ Ç¥½Ã
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
    public void volumeUPBT()//¼Ò¸® Å©±â°ü¸®¹öÆ°
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

    public void ReStartGame()// ºí·Ï°ú Ã¼·ÂÀ» ¸ÇÃ³À½ »óÅÂ·Î µ¹¸®°í ±×´ë·Î ½ÃÀÛ
    {
        AdManager.Instance.ShowRewardedAd();
        GameManager.Instance.hp = GameManager.Instance.startHp;
        GameManager.Instance.GameOver = false;
        GameManager.Instance.gamestart = true;
        aera.aeraAllClear();
        SaveLoad.SaveGame();
        GameoverUI.SetActive(false);
        GameManager.Instance.OneSaveLife =true;
    }
    public void FinishGame()//°ÔÀÓ ³¡
    {
        SaveLoad.SaveGame();
        if(GameManager.Instance.viewpoint > GameManager.Instance.RankScore[GameManager.Instance.RankScore.Count-1].Item2
            || GameManager.Instance.RankScore.Count < GameManager.Instance.MaxRankList)
        {
            RankRegist.SetActive(true);
        }
        else
        {
            fadeInOut.gameObject.SetActive(true);
            fadeInOut.fadoutScene(0);
        }
    }

    public void RegistAndHome()
    {
        if(nametextfield.text =="")
        {
            nametextfield.text = "aaa";
        }
        GameManager.Instance.RankScore.Add((nametextfield.text,GameManager.Instance.viewpoint));
        GameManager.Instance.RankScore.Sort((a, b) => a.Item2.CompareTo(b.Item2));
       SaveLoad.SaveGame();
        RankRegist.SetActive(false);
        fadeInOut.gameObject.SetActive(true);
        fadeInOut.fadoutScene(0);
    }
}
