using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] Button exitBT;
    [SerializeField] Button reBT;
    [SerializeField] Image textbar;
    Color barcolor;
    float barcolortime;
    void Start()
    {
        barcolor = new Color(textbar.color.r,textbar.color.g, textbar.color.b, 0);
        volumeEnergy = new GameObject[volume.transform.childCount];
        for (int i = 0; i < volume.transform.childCount; i++)
        {
            volumeEnergy[i] = volume.transform.GetChild(i).gameObject;
        }
        //�ؽ�Ʈ Ư�����ڸ� �Է��ϰ� �ϴ� �ڵ�
        nametextfield.onValueChanged.AddListener((w) => nametextfield.text = Regex.Replace(w, @"[^0-9a-zA-Z]",""));
    }
    public void Update()
    {
        barcolortime = Mathf.Sin(Time.time*10);
        textbar.color = Color.Lerp(Color.red, barcolor,barcolortime);
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
    public void SoundsettingOpenBT()// �Ҹ� ������ ������ ����� �Ҹ�����ŭ ǥ��
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
    public void volumeUPBT()//�Ҹ� ũ�������ư
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

    public void ReStartGame()// ��ϰ� ü���� ��ó�� ���·� ������ �״�� ����
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
    public void FinishGame()//���� ��
    {
        SaveLoad.SaveGame();
        if (GameManager.Instance.RankScore.Count < GameManager.Instance.MaxRankList||
            GameManager.Instance.viewpoint > GameManager.Instance.RankScore[GameManager.Instance.RankScore.Count-1].Item2 
          )
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
        GameManager.Instance.RankScore.Sort((a, b) => b.Item2.CompareTo(a.Item2));
        if (GameManager.Instance.RankScore.Count > GameManager.Instance.MaxRankList)
        {
            GameManager.Instance.RankScore.RemoveAt(10);
        }
        exitBT.enabled = false;
        reBT.enabled = false;
        SaveLoad.BasicSaveGame();
        BasicSaveData settingSaveData = SaveLoad.BasicLoadGame();
        GameManager.Instance.PlayerSettingLoad(settingSaveData);
        RankRegist.SetActive(false);
        fadeInOut.gameObject.SetActive(true);
        fadeInOut.fadoutScene(0);
    }
}
