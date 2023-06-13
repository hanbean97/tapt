using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class previewbluck : MonoBehaviour
{
    public Databiuck[] datablock;
    public Piece activepiece { get; set; }
    public GameObject upblock;
    public GameObject downblock;
    public GameObject leftblock;
    public GameObject rightblock;
    public GameObject mainblock;
    int direct = 0;
    public BlockSave BlockSave;
    public List<int> blocktable = new List<int>();
    public bool saveBTch;
    public void Awake()
    {
        for (int i = 0; i < datablock.Length; i++)
        {
            datablock[i].Initialize();// datablock들의typecell 안에 데이터 넣기
        }
    }

    public void SpawnPiece()//새로운 블록 정보 갱신해야할때 호출
    {
        System.Random rnd = new System.Random();

        if (blocktable.Count == 0)//블록이 전부 나올때까지 같은게 안나오도록 설정
        {
            blocktable.AddRange(new List<int> { 0, 1, 2, 3, 4, 5 });
        }

        int randomindex = rnd.Next(blocktable.Count);
        int random = blocktable[randomindex];
        blocktable.RemoveAt(randomindex);
        Databiuck data = datablock[random];

        activepiece.Initialize(data);// 피스에 저장될 데이터를 보냄

        blockview();
    }

    // Start is called before the first frame update
    void Start()
    {
        activepiece = new Piece();
        if (GameManager.Instance.Loadch == false)
        {
            SpawnPiece();
        }
        else
        {
            if (GameManager.Instance.bTable != null)
            {
                blocktable = GameManager.Instance.bTable;
            }
            for (int i = 0; i < Data.Cell.Count; i++)// Piece안에Databiuck.BType의 enum의 정보를 가져와 찾는다
            {
                if (GameManager.Instance.preview == i)
                {
                    Databiuck data = datablock[i];
                    activepiece.Initialize(data);
                    blockview();
                }
            }
        }

        mainblock.SetActive(true);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) || direct < 0)
        {
            activepiece.Rotater(-1);
            blockview();
            direct = 0;
        }
        else if (Input.GetKeyDown(KeyCode.E) || direct > 0)
        {
            activepiece.Rotater(1);
            blockview();
            direct = 0;
        }

        if (saveBTch == true)//save 버튼이 눌린걸 aeraT에 알려주고 false
        {
            saveBTch = false;
        }
    }

    public void blockview()
    {
        upblock.SetActive(false);
        downblock.SetActive(false);
        leftblock.SetActive(false);
        rightblock.SetActive(false);
        for (int i = 0; i < activepiece.cells.Length; i++)
        {
            int x = activepiece.cells[i].x;
            int y = activepiece.cells[i].y;

            switch (x, y)// 다음 블록 보여주기
            {
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
    public void RoteBT(int direction)//버튼에서 매개변수로 받음
    {
        direct = direction;
    }

    public void BlockSaveBT()// 버튼 누를시 세이브블럭에 데이터가있는지 없는지 판단해서 함수를 호출
    {
        if (BlockSave.piece.bluckdata.BType == bluckType.None)
        {
            BlockSave.Saveblock(activepiece);
            SpawnPiece();
        }
        else
        {
            BlockSave.Changeblock(activepiece);
        }
        blockview();
        saveBTch = true;
    }



}
