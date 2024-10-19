using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aeraT : MonoBehaviour
{
    //����� �����Ҽ��ִ� ���� ũ��
    public int width;
    public int height;
    //������
    static Transform[,] tgrid;// �ڽĵ�(��ȯ�� ��ġ)�� ������ Ʈ������
    private bool[,] checker;
    Transform CurrentTouch;
    public GameObject RedBlock;
    public GameObject GreyBlock;
    public GameObject effectblock;
    //������ �����ִ°��� �����ϴ� ���� ������Ʈ
    GameObject[,] redB;
    GameObject[,] GreyB;
    // ��ġ�� ���� ���� ������
    public previewbluck setdata;
    Vector3 Mouseposition;
    // ���� �����Ͽ� ���Ҷ� ���� ����ϱ����� Ŭ����
    public BlockSave blockSave;
    // �������Ž� ȿ�� 
    public GameObject LineClear;
    [SerializeField] float xLineoffset;
    [SerializeField] float yLineoffset;
    public GameObject GameoverUI;
    public GameObject UI;
    int Combo = 0;
    void Start()
    {
        width = GameManager.Instance.width;
        height = GameManager.Instance.height;
        tgrid = new Transform[width, height];
        checker = new bool[width, height];
        redB = new GameObject[width, height];
        GreyB = new GameObject[width, height];
        if (GameManager.Instance.Loadch == true)
        {
            Combo = GameManager.Instance.nowcombo;
        }
        int a = 0;
        for (int j = 0; j < height; j++)
        {
            for (int i = 0; i < width; i++)
            {
                tgrid[i, j] = this.transform.GetChild(a);
                checker[i, j] = false;
                redB[i, j] = Instantiate(RedBlock, tgrid[i, j]);
                GreyB[i, j] = Instantiate(GreyBlock, tgrid[i, j]);
                redB[i, j].SetActive(false);
                GreyB[i, j].SetActive(false);
                //�ε� ������ ������ �߰��� ������ �Է��Ѵ�
                if (GameManager.Instance.Loadch == true)
                {
                    if (GameManager.Instance.redBch[i, j] == true)
                    {
                        redB[i, j].SetActive(true);
                    }
                    if (GameManager.Instance.greyBch[i, j] == true)
                    {
                        GreyB[i, j].SetActive(true);
                    }
                    if (GameManager.Instance.chchck[i, j] == true)
                    {
                        checker[i, j] = true;
                    }
                }
                else
                {
                    GameManager.Instance.GameOver = false;
                }
                a++;
            }
        }


    }
    // Update is called once per frame
    void Update()
    {
#if MOBILE_INPUT
        if(Input.touchCount >0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos;

            switch(touch.phase)
            {
                case TouchPhase.Began:
                    Vector2 touchPosToVector2 = new Vector2(touch.position.x, touch.position.y);
                    touchPos = Camera.main.ScreenToWorldPoint(touchPosToVector2);
                    RaycastHit2D hit = Physics2D.Raycast(touchPos, transform.forward, 15);
                    if (hit.collider != null && UI.activeSelf != true &&  GameoverUI.activeSelf != true)
                    {
                        CurrentTouch = hit.transform;
                        instbluck(hit.transform);// ��ġ�κ� 
                        CheckIfPlayerLost();// ���ӿ���üũ

                    }
                    break;
            }
            Input.touchCount =0;
        }
#endif


        if (Input.GetMouseButtonDown(0) && GameManager.Instance.GameOver == false) // ��ġ���̷� �ٲܿ���
        {
            Mouseposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(Mouseposition, transform.forward, 15f);

            if (hit.collider != null && UI.activeSelf != true)
            {
                CurrentTouch = hit.transform;
                instbluck(hit.transform);// ��ġ�κ� 
                
                CheckIfPlayerLost();// ���ӿ���üũ
            }
        }

        if (setdata.saveBTch == true && GameManager.Instance.GameOver == false)//����� ���̺� ���� ��� ���ӿ��� üũ
        {
            CheckIfPlayerLost();
            Savearea();//����� ���������� �����͸� ����
            SaveLoad.SaveGame();
        }
    }

    void instbluck(Transform curren)// ��� ��ȯ �Լ�
    {
        for (int j = 0; j < height; j++)
        {
            for (int i = 0; i < width; i++)
            {
                if (tgrid[i, j] == curren && checker[i, j] == false)// ��ġ�� �κ� ã��
                {
                    bool dontinst = true;
                    foreach (Vector2Int offs in setdata.activepiece.cells)// ��ġ���� üũ
                    {// Ȱ���Ұ��� ���� ����ٰ�
                        if ((offs.x + i) < 0 || (offs.x + i) >= width || (offs.y + j) < 0 || (offs.y + j) >= height)
                        {
                            dontinst = false;
                        }

                        if ((offs.x + i) >= 0 && (offs.x + i) < width && (offs.y + j) >= 0 && (offs.y + j) < height)
                        {
                            if (checker[offs.x + i, offs.y + j] == true)
                            {
                                dontinst = false;
                            }
                        }
                    }

                    if (dontinst == true)//��ġ 
                    {
                        foreach (Vector2Int offs in setdata.activepiece.cells)
                        {
                            if ((offs.x + i) >= 0 && (offs.x + i) < width && (offs.y + j) >= 0 && (offs.y + j) < height)
                            {
                                if (checker[offs.x + i, offs.y + j] == false)
                                {
                                    if (offs.x == 0 && offs.y == 0)//��� ��ġ
                                    {
                                        checker[i, j] = true;
                                        redB[i, j].SetActive(true);

                                        Instantiate(effectblock, redB[i, j].transform.position, Quaternion.identity);
                                    }
                                    else
                                    {
                                        checker[offs.x + i, offs.y + j] = true;
                                        GreyB[offs.x + i, offs.y + j].SetActive(true);
                                        Instantiate(effectblock, GreyB[offs.x + i, offs.y + j].transform.position, Quaternion.identity);
                                    }
                                }
                            }
                        }
                        //���� ��ġ ����ȿ��
                        setdata.SpawnPiece(); // ��������� ������� �ʱ�ȭ
                        ClearBluck(); //���� üũ�� ����
                        bossSkill();// ������ų
                        Savearea();// ��ġ�Ҷ� ���� ���ӸŴ����� ��Ȳ�� ����
                        SaveLoad.SaveGame();
                       
                    }
                    else
                    {
                        //��ġ���н� ��Ÿ���� ȿ��
                    }
                }
                else if (tgrid[i, j] == curren && checker[i, j] == true)
                {
                    // ����ִ°� �߸���ġȿ��= ��ġ ���� ȿ��
                }
            }
        }
    }




    void ClearBluck()// ����üũ+����
    {
        int widthsum = 0;//������ üũ
        int heightsum = 0;// ������ üũ
        int bonuspoint = 0;// ���ŵ� ���� �߰�����
        bool combosuccess = false;// �޺� ��������
        // ������ ������ ����Ŵ
        bool[] Wclearline = new bool[height];
        bool[] Hclearline = new bool[width];
        int totalpoint =0;
        for (int j = 0; j < height; j++)
        {
            for (int i = 0; i < width; i++)//�������� üũ
            {
                if (i == 0)// ���������� üũ�ҽ� 0���� �ʱ�ȭ
                {
                    Wclearline[j] = false;
                    widthsum = 0;
                }

                if (checker[i, j] == true)
                {
                    widthsum++;//�����ٿ��ִ� ����� ������ŭ +
                }
                if (widthsum == width)// �����ٰ� ũ�Ⱑ ������ �ش���� ���� ȿ��
                {
                    Wclearline[j] = true;
                    bonuspoint++;// �����ѹ��� ������ ���Ž� ���ʽ�����
                    GameManager.Instance.AddPoint();// ���� ���Ž� 1�� + Ŭ����� ȿ��
                    totalpoint++;
                    GameObject xlineeffect = Instantiate(LineClear, new Vector3(tgrid[i, j].transform.position.x + xLineoffset, tgrid[i, j].transform.position.y, tgrid[i, j].transform.position.z), Quaternion.identity);
                    Destroy(xlineeffect, 0.3f);//�������� ȿ�� ��ȯ
                    combosuccess = true;//�޺� ����
                }

            }
        }
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)//�������� üũ
            {
                if (j == 0)// ���������� üũ�ҽ� 0���� �ʱ�ȭ
                {
                    Hclearline[i] = false;
                    heightsum = 0;
                }
                if (checker[i, j] == true)
                {
                    heightsum++;// ������ üũ
                }
                if (heightsum == height)// �����ٰ� ũ�Ⱑ ������ �ش���� ���� ȿ��
                {
                    Hclearline[i] = true;
                    bonuspoint++;// �����ѹ��� ������ ���Ž� ���ʽ�����
                    GameManager.Instance.AddPoint();// ���� ���Ž� 1�� 
                    totalpoint++;
                    GameObject hlineeffect = Instantiate(LineClear, new Vector3(tgrid[i, j].transform.position.x, tgrid[i, j].transform.position.y + yLineoffset, tgrid[i, j].transform.position.z),
                          Quaternion.Euler(0, 0, 90));//�������� ȿ�� ��ȯ
                    Destroy(hlineeffect, 0.3f);
                    combosuccess = true;

                }
            }
        }
        if (bonuspoint > 2)// 3���̻� �ѹ��� ���Ž� 2�� ���� ���� �� 6���� ���� �ѹ�����������
        {
            for (int i = 0; i < bonuspoint; i++)
            {
                GameManager.Instance.AddPoint();
                totalpoint++;
            }
        }
        if (combosuccess == true)// �޺�
        {
            Combo++;
        }
        else// �޺� ���н� �ʱ�ȭ
        {
            Combo = 0;
        }
        if (Combo > 2)// �޺������ϸ� �߰����� 1��
        {
            GameManager.Instance.AddPoint();
            totalpoint++;
        }

        for (int j = 0; j < height; j++)// ����� ��Ȱ��ȭ
        {
            for (int i = 0; i < width; i++)
            {
                if (Hclearline[i] == true)//�ش���� ��Ȱ��ȭ
                {
                    if (redB[i, j].activeSelf == true)// ������� ���Ž� �߰� ���� 1��+ �뷱�� ���������� ��� �ּ�ó��
                    {
                        // GameManager.Instance.AddPoint(); totalpoint++;
                    }
                    GreyB[i, j].SetActive(false);
                    redB[i, j].SetActive(false);
                    checker[i, j] = false;
                }
                if (Wclearline[j] == true)//�ش���� ��Ȱ��ȭ
                {
                    if (redB[i, j].activeSelf == true)
                    {
                        //  GameManager.Instance.AddPoint(); totalpoint++;
                    }
                    GreyB[i, j].SetActive(false);
                    redB[i, j].SetActive(false);
                    checker[i, j] = false;
                }
            }
        }
        if (totalpoint!=0)
        {
            EnemysManager.Instance.currentEnemyDamage(totalpoint);
        }
        

    }

    void CheckIfPlayerLost()//�ϳ��� �־���� ���ӿ��� ���� üũ
    {
        bool GameoverCheck = true;
        for (int j = 0; j < height; j++)
        {
            for (int i = 0; i < width; i++)
            {
                for (int r = 0; r < Data.RotationMatrix.Length; r++)//����� 4������ �������鼭 üũ
                {
                    bool caninstblock = false;// ��ϴ��� ���� ����
                    foreach (Vector2Int offs in setdata.activepiece.cells)//������ ��Ͼ��� ������ �����鼭 üũ
                    {
                        if ((offs.x + i) < 0 || (offs.x + i) >= width || (offs.y + j) < 0 || (offs.y + j) >= height)
                        {
                            caninstblock = true;// ���� �����ϸ� ���԰����ϴټ� �˸�
                        }

                        if ((offs.x + i) >= 0 && (offs.x + i) < width && (offs.y + j) >= 0 && (offs.y + j) < height)
                        {
                            if (checker[offs.x + i, offs.y + j] == true)
                            {
                                caninstblock = true;
                            }
                        }
                    }
                    bool caninstsaveblock = false;//���̺� ��ϴ��� ���� ���� , ���̺����� ���°�찡 ������ ����üũ
                    if (blockSave.piece.cells != null)
                    {

                        foreach (Vector2Int offs in blockSave.piece.cells)//���̺� ������ ������ �����鼭 üũ
                        {
                            if ((offs.x + i) < 0 || (offs.x + i) >= width || (offs.y + j) < 0 || (offs.y + j) >= height)
                            {
                                caninstsaveblock = true;
                            }

                            if ((offs.x + i) >= 0 && (offs.x + i) < width && (offs.y + j) >= 0 && (offs.y + j) < height)
                            {
                                if (checker[offs.x + i, offs.y + j] == true)
                                {
                                    caninstsaveblock = true;
                                }
                            }
                        }
                    }

                    if (caninstblock == false || caninstsaveblock == false)//�����ϳ��� �����ϸ� ���Ӱ��
                    {
                        GameoverCheck = false;
                    }
                    setdata.activepiece.Rotater(1);// �����Ѻ�� ���ư��鼭 üũ
                    if (blockSave.piece.cells != null)// ����� ����� ������ ����� ���� ���ư��鼭 üũ
                    {
                        blockSave.piece.Rotater(1);
                    }
                }
            }
        }

        if (GameoverCheck == true)// ���ӿ��� ȿ��
        {
            GameManager.Instance.GameOver = true;
            GameManager.Instance.gamestart = false;
            GameoverUI.SetActive(true);
            Debug.Log(" Gameover");
        }
    }
    public void aeraAllClear()//��� ���� ��Ȱ��ȭ
    {
        for (int j = 0; j < height; j++)
        {
            for (int i = 0; i < width; i++)
            {
                redB[i, j].SetActive(false);
                GreyB[i, j].SetActive(false);
                checker[i, j] = false;
            }
        }
    }
    public void Savearea()// ������ �����͸� ���ӸŴ����� ������
    {
        for (int j = 0; j < height; j++)
        {
            for (int i = 0; i < width; i++)
            {
                GameManager.Instance.redBch[i, j] = false;
                GameManager.Instance.greyBch[i, j] = false;
                GameManager.Instance.chchck[i, j] = false;

                if (redB[i, j].activeSelf == true)
                {
                    GameManager.Instance.redBch[i, j] = true;
                    GameManager.Instance.chchck[i, j] = true;
                }
                if (GreyB[i, j].activeSelf == true)
                {
                    GameManager.Instance.greyBch[i, j] = true;
                    GameManager.Instance.chchck[i, j] = true;
                }
            }
        }
        GameManager.Instance.bTable = setdata.blocktable;
        GameManager.Instance.preview = (int)setdata.activepiece.bluckdata.BType;
        GameManager.Instance.savepiece = (int)blockSave.piece.bluckdata.BType;
        GameManager.Instance.nowcombo = Combo;
    }

    void bossSkill()
    {
        if (EnemysManager.Instance.bosss[EnemysManager.Instance.bossindex].EnemyDeth == false && EnemysManager.Instance.bossactiv == true)
        {
            switch (EnemysManager.Instance.bosss[EnemysManager.Instance.bossindex].bossType)// ���� ���� ���� ��ų
            {
                case BossType.sinmon:
                    simonSkill();
                    break;
                case BossType.BigMeteo:
                    bigMeteobossSkill();
                    break;
            }
        }
    }

    void bigMeteobossSkill()
    {
        setdata.activepiece.Initialize(setdata.datablock[0]);
        setdata.blockview();
        for (int i =1; i < GameManager.Instance.height; i++)
        {
            if (checker[0, i] == false)
            {
                checker[0, i] = true;
                GreyB[0, i].SetActive(true);
            }
            if (checker[GameManager.Instance.width - 1, i] == false)
            {
                checker[GameManager.Instance.width - 1, i] = true;
                GreyB[GameManager.Instance.width - 1, i].SetActive(true);
            }
        }
    }
    void simonSkill()
    {
        int simonskillblockx;
        int simonskillblocky;
        bool skillsuccess = false;
        while(skillsuccess == false)
        {
            simonskillblockx= Random.Range(0, GameManager.Instance.width);
            simonskillblocky = Random.Range(0, GameManager.Instance.height);
            if(checker[simonskillblockx, simonskillblocky] == false)
            {
                checker[simonskillblockx, simonskillblocky] = true;
                GreyB[simonskillblockx, simonskillblocky].SetActive(true);
                skillsuccess = true;
            }
        }
    }

}


