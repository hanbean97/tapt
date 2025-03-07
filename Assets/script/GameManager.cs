using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singltons<GameManager>
{
    private int Point;//점수
    public int viewpoint { get { return Point; } set { Point = value; } }
   [SerializeField]private int playerHp;
    public int hp { get { return playerHp; } set { playerHp = value; } }

    public bool GameOver;
    // 블록이 있는곳을 체크
    public bool[,] redBch;
    public bool[,] greyBch;
    public bool[,] chchck;
    public int width = 6;
    public int height = 5;
    public int preview;
    public int savepiece;
    public int startHp = 4;
    public List<int> bTable = new List<int>();// 프리뷰에 저장된 다음에 나올 블럭들을 저장
    public int nowcombo;
    public int volumeEnergyIndex;
    public bool gamestart;
    public bool Loadch;// 뉴게임인지 새게임인지 체크
    public int Enemyhp;
    public int nowlevel;
    public int nowEnemy;
    public int nowBoss;
    public bool enemytype;
    public int bossmeetpoint;
    public int MaxRankList =10;
    public List<(string,int)> RankScore;//순위 이름 점수
    public bool OneSaveLife = false;
    public bool notfirstPlayer = false;
    public void AddPoint()
    {
        Point++;
    }
    protected override void Awake()
    {
        base.Awake();
        gamestart = false;
        redBch = new bool[width, height];
        greyBch = new bool[width, height];
        chchck = new bool[width, height];

        DontDestroyOnLoad(gameObject);
        BasicSaveData settingSaveData = SaveLoad.BasicLoadGame();
        GameManager.Instance.PlayerSettingLoad(settingSaveData);
        GmaeSaveData tData = SaveLoad.LoadGame();
        loadSetGameData(tData);
    }
  
    public void PlayerSettingLoad(BasicSaveData bData)
    {
        if (bData != null)
        {
            volumeEnergyIndex = bData.volume;
            RankScore = bData.ranker;
            notfirstPlayer = bData.Firstch;
        }
        else
        {
            volumeEnergyIndex = 2;
            RankScore = new List<(string, int)>();
            notfirstPlayer = false;
        }
    }
    public void loadSetGameData(GmaeSaveData tData)
    {
        if (tData != null)
        {
            redBch = tData.saveredB;
            greyBch = tData.saveGreyB;
            chchck = tData.check;
            preview = tData.previewbluckpiece;
            savepiece = tData.BlockSavepice;
            hp = tData.HP;
            bTable = tData.blockTable;
            nowcombo = tData.ComboSave;
            volumeEnergyIndex = tData.volum;
            viewpoint = tData.nowpoint;
            GameOver = tData.gameover;
            Enemyhp = tData.EnemyHp;
            nowlevel = tData.Level;
            nowEnemy = tData.nowenemy;
            nowBoss = tData.nowboss;
            enemytype = tData.EnemyType;
            bossmeetpoint = tData.bosspoint;
            RankScore = tData.ranker;
            OneSaveLife = tData.onesavelifech;
        } 
        else
        {// 기존 정보 초기화후 다시 설정
            redBch = null;
            greyBch = null;
            chchck = null;
            bTable = null;
            redBch = new bool[width, height];
            greyBch = new bool[width, height];
            chchck = new bool[width, height];
            playerHp = startHp;
            nowcombo = 0;
            viewpoint = 0;
            GameOver = false;
            Enemyhp = 0;
            nowlevel = 0;
            nowEnemy = 0;
            nowBoss = 0;
            enemytype = false;
            bossmeetpoint = 0;
            OneSaveLife = false;
        }
    }
}
