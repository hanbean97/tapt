using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GmaeSaveData // ������ �����͸� ������ Ŭ����
{
    public bool[,] saveredB;
    public bool[,] saveGreyB;
    public int previewbluckpiece;// �̺κ� ���� �ؾ���
    public int BlockSavepice;
    public bool[,] check;
    public int HP;
    public int nowpoint;
    public int ComboSave;
    public List<int> blockTable = new List<int>();
    public int volum;
    public bool gameover;
    public GmaeSaveData()
    {
        saveredB = new bool[GameManager.Instance.width, GameManager.Instance.height];
        saveGreyB = new bool[GameManager.Instance.width, GameManager.Instance.height];
        check = new bool[GameManager.Instance.width, GameManager.Instance.height];
        saveredB = GameManager.Instance.redBch;
        saveGreyB = GameManager.Instance.greyBch;
        check = GameManager.Instance.chchck;
        previewbluckpiece = GameManager.Instance.preview;
        BlockSavepice = GameManager.Instance.savepiece;
        HP = GameManager.Instance.hp;
        nowpoint = GameManager.Instance.viewpoint;
        blockTable = GameManager.Instance.bTable;
        ComboSave = GameManager.Instance.nowcombo;
        volum = GameManager.Instance.volumeEnergyIndex;
        gameover = GameManager.Instance.GameOver;
    }

}