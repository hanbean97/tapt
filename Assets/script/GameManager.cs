using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : PersistentSingleton<GameManager>
{
    private int Point;//����
    public int viewpoint { get { return Point; } set { Point = value; } }
    private int playerHp;
    public int hp { get { return playerHp; } set { Point = value; } }

    public bool GameOver;
    // ����� �ִ°��� üũ
    public bool[,] redBch;
    public bool[,] greyBch;
    public bool[,] chchck;
    public int width = 6;
    public int height = 5;
    public int preview;
    public int savepiece;
    public int startHp = 3;
    public List<int> bTable = new List<int>();// �����信 ����� ������ ���� ������ ����
    public int nowcombo;
    public int volumeEnergyIndex;
    public bool gamestart;
    public bool Loadch;// ���������� ���������� üũ
    float starttime;
    public void AddPoint()
    {
        Point++;
    }

    void Start()
    {
        gamestart = false;
        redBch = new bool[width, height];
        greyBch = new bool[width, height];
        chchck = new bool[width, height];
      
    }
    void Update()
    {
       
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
            hp = startHp;
            nowcombo = 0;
            volumeEnergyIndex = 3;
            viewpoint = 0;
            GameOver = false;
        }
    }
}
