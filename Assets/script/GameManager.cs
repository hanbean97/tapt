using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singltons<GameManager>
{
    private int Point;//����
    public int viewpoint { get { return Point; } set { Point = value; } }
   [SerializeField]private int playerHp;
    public int hp { get { return playerHp; } set { playerHp = value; } }

    public bool GameOver;
    // ����� �ִ°��� üũ
    public bool[,] redBch;
    public bool[,] greyBch;
    public bool[,] chchck;
    public int width = 6;
    public int height = 5;
    public int preview;
    public int savepiece;
    public int startHp = 4;
    public List<int> bTable = new List<int>();// �����信 ����� ������ ���� ������ ����
    public int nowcombo;
    public int volumeEnergyIndex;
    public bool gamestart;
    public bool Loadch;// ���������� ���������� üũ
    float starttime;
    public int Enemyhp;
    public int nowlevel;
    public int nowEnemy;
    public int nowBoss;
    public bool enemytype;
    public int bossmeetpoint;
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
        }
        else
        {// ���� ���� �ʱ�ȭ�� �ٽ� ����
            redBch = null;
            greyBch = null;
            chchck = null;
            bTable = null;
            redBch = new bool[width, height];
            greyBch = new bool[width, height];
            chchck = new bool[width, height];
            playerHp = startHp;
            nowcombo = 0;
            volumeEnergyIndex = 3;
            viewpoint = 0;
            GameOver = false;
            Enemyhp = 0;
            nowlevel = 0;
            nowEnemy = 0;
            nowBoss = 0;
            enemytype = false;
            bossmeetpoint = 0;
        }
    }
}
