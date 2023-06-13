using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSave : MonoBehaviour// ����� �����س��� Ŭ����
{
    public Piece piece { get; set; }
    public GameObject upblock;
    public GameObject downblock;
    public GameObject leftblock;
    public GameObject rightblock;
    public GameObject mainblock;
    public Databiuck[] datablocks;

    public void Awake()
    {
        for (int i = 0; i < datablocks.Length; i++)
        {
            datablocks[i].Initialize();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        piece = new Piece();
        mainblock.SetActive(false);
        upblock.SetActive(false);
        downblock.SetActive(false);
        leftblock.SetActive(false);
        rightblock.SetActive(false);
        Databiuck startdata = datablocks[(int)bluckType.None];
        piece.Initialize(startdata);
        if (GameManager.Instance.Loadch == true && GameManager.Instance.savepiece != (int)bluckType.None)//������ �ε��
        {
            for (int i = 0; i < Data.Cell.Count; i++)// Databiuck�� Btype���� ����� ����� ã��
            {
                if (GameManager.Instance.savepiece == i)
                {
                    Databiuck data = datablocks[i];
                    piece.Initialize(data);
                    blockview();
                }
            }
        }
      
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void blockview()
    {
        mainblock.SetActive(false);
        upblock.SetActive(false);
        downblock.SetActive(false);
        leftblock.SetActive(false);
        rightblock.SetActive(false);
        for (int i = 0; i < piece.cells.Length; i++)
        {
            int x = piece.cells[i].x;
            int y = piece.cells[i].y;

            switch (x, y)// ���� ��� �����ֱ�
            {
                case (0, 0):
                    mainblock.SetActive(true);
                    break;
                case (0, 1):
                    upblock.SetActive(true);
                    break;
                case (0, -1):
                    downblock.SetActive(true);
                    break;
                case (1, 0):
                    rightblock.SetActive(true);
                    break;
                case (-1, 0):
                    leftblock.SetActive(true);
                    break;
            }
        }
    }

    public void Saveblock(Piece piecedata)// ���̺� ��Ͽ� ���� �ǽ��� �ִ´�
    {
        piece.blocksave(piecedata);
        blockview();
    }
    public void Changeblock(Piece piecedata)//���̺� ����� ������ ����Pece�������ϰ� ���� ������ �޴´�
    {
        Piece virtualpiece = new Piece();
        virtualpiece.blocksave(piecedata);
        piecedata.blocksave(piece);
        piece.blocksave(virtualpiece);
        blockview();
    }
}
