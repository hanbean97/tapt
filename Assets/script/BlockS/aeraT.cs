using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class aeraT : MonoBehaviour
{
    //?????? ???????????? ???? ????
    public int width;
    public int height;
    //??????
    static Transform[,] tgrid;// ??????(?????? ????)?? ?????? ????????
    private bool[,] checker;
    Transform CurrentTouch;
    public GameObject RedBlock;
    public GameObject GreyBlock;
    public GameObject effectblock;
    //???????? ???????????? ???????? ???? ????????
    GameObject[,] redB;
    GameObject[,] GreyB;
    // ?????? ???? ?????? ??????
    public previewbluck setdata;
    Vector3 Mouseposition;
    // ?????? ???????? ?????? ???? ???????????? ??????
    public BlockSave blockSave;
    // ?????????? ???? 
    public GameObject LineClear;
    [SerializeField] float xLineoffset;
    [SerializeField] float yLineoffset;
    public GameObject GameoverUI;
    public GameObject UI;
    int Combo = 0;
    bool isgameover = false;
    public bool setBlock;
    bool iscombo;
    bool scoresuppression=false;
    bool oneclickBossOn=false;
    public bool OneClickBossOnOff { get { return oneclickBossOn; } }
    [SerializeField] UImanager UImana;
    [SerializeField] TMP_Text NewScore;
    [SerializeField] TMP_Text LastScore;
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
        else
        {
            GameManager.Instance.hp = GameManager.Instance.startHp;
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
                //???? ?????? ?????? ?????? ?????? ????????
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
                        instbluck(hit.transform);// ???????? 
                        CheckIfPlayerLost();// ????????????

                    }
                    break;
            }
            Input.touchCount =0;
        }
#endif


        if (Input.GetMouseButtonDown(0) && GameManager.Instance.GameOver == false) // ?????????? ????????
        {
            Mouseposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(Mouseposition, transform.forward, 15f);

            if (hit.collider != null && UI.activeSelf != true)
            {
                CurrentTouch = hit.transform;
                instbluck(hit.transform);// ???????? 
                
                CheckIfPlayerLost();// ????????????
            }
        }

        if (setdata.saveBTch == true && GameManager.Instance.GameOver == false)//?????? ?????? ???? ???? ???????? ????
        {
            CheckIfPlayerLost();
            Savearea();//?????? ?????????? ???????? ????
            SaveLoad.SaveGame();
        }
    }

    void instbluck(Transform curren)// ???? ???? ????
    {
        for (int j = 0; j < height; j++)
        {
            for (int i = 0; i < width; i++)
            {
                if (tgrid[i, j] == curren && checker[i, j] == false)// ?????? ???? ????
                {
                    bool dontinst = true;
                    foreach (Vector2Int offs in setdata.activepiece.cells)// ???????? ????
                    {// ?????????? ???? ????????
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

                    if (dontinst == true)//???? 
                    {
                        foreach (Vector2Int offs in setdata.activepiece.cells)
                        {
                            if ((offs.x + i) >= 0 && (offs.x + i) < width && (offs.y + j) >= 0 && (offs.y + j) < height)
                            {
                                if (checker[offs.x + i, offs.y + j] == false)
                                {
                                    if (offs.x == 0 && offs.y == 0)//???? ????
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
                        //???? ???? ????????
                        isSetB();
                        setdata.SpawnPiece(); // ???????????? ???????? ??????
                        ClearBluck(); //???? ?????? ????
                        bossSkill();// ????????
                        Savearea();// ???????? ???? ???????????? ?????? ????
                        SaveLoad.SaveGame();
                       
                    }
                    else
                    {
                        //?????????? ???????? ????
                        Scenemamhincrit.Instance.StartShake(1f, 1f);
                    }
                }
                else if (tgrid[i, j] == curren && checker[i, j] == true)
                {
                    // ?????????? ????????????= ???? ???? ????
                    Scenemamhincrit.Instance.StartShake(1f,1f);
                }
            }
        }
    }




    void ClearBluck()// ????????+????
    {
        int widthsum = 0;//?????? ????
        int heightsum = 0;// ?????? ????
        int bonuspoint = 0;// ?????? ???? ????????
        bool combosuccess = false;// ???? ????????
        // ?????? ?????? ??????
        bool[] Wclearline = new bool[height];
        bool[] Hclearline = new bool[width];
        int totalpoint =0;
        for (int j = 0; j < height; j++)
        {
            for (int i = 0; i < width; i++)//???????? ????
            {
                if (i == 0)// ?????????? ???????? 0???? ??????
                {
                    Wclearline[j] = false;
                    widthsum = 0;
                }

                if (checker[i, j] == true)
                {
                    widthsum++;//???????????? ?????? ???????? +
                }
                if (widthsum == width)// ???????? ?????? ?????? ???????? ???? ????
                {
                    Wclearline[j] = true;
                    bonuspoint++;// ?????????? ?????? ?????? ??????????
                    totalpoint++;
                    GameObject xlineeffect = Instantiate(LineClear, new Vector3(tgrid[i, j].transform.position.x + xLineoffset, tgrid[i, j].transform.position.y, tgrid[i, j].transform.position.z), Quaternion.identity);
                    Destroy(xlineeffect, 0.3f);//???????? ???? ????
                    combosuccess = true;//???? ????
                }

            }
        }
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)//???????? ????
            {
                if (j == 0)// ?????????? ???????? 0???? ??????
                {
                    Hclearline[i] = false;
                    heightsum = 0;
                }
                if (checker[i, j] == true)
                {
                    heightsum++;// ?????? ????
                }
                if (heightsum == height)// ???????? ?????? ?????? ???????? ???? ????
                {
                    Hclearline[i] = true;
                    bonuspoint++;// ?????????? ?????? ?????? ??????????
                    totalpoint++;
                    GameObject hlineeffect = Instantiate(LineClear, new Vector3(tgrid[i, j].transform.position.x, tgrid[i, j].transform.position.y + yLineoffset, tgrid[i, j].transform.position.z),
                          Quaternion.Euler(0, 0, 90));//???????? ???? ????
                    Destroy(hlineeffect, 0.3f);
                    combosuccess = true;

                }
            }
        }
        if (bonuspoint > 2)// 3?????? ?????? ?????? 2?? ???? ?????? ?? 6???? ???? ??????????????
        {
            for (int i = 0; i < bonuspoint; i++)
            {
                totalpoint++;
            }
        }
        if (combosuccess == true)// ????
        {
            Combo++;
            UImana.isCombo = true;
        }
        else// ???? ?????? ??????
        {
            Combo = 0;
        }
        if (Combo > 2)// ???????????? ???????? 1??
        {
            totalpoint++;
        }

        for (int j = 0; j < height; j++)// ?????? ????????
        {
            for (int i = 0; i < width; i++)
            {
                if (Hclearline[i] == true)//???????? ????????
                {
                    if (redB[i, j].activeSelf == true)// ???????? ?????? ???? ???? 1??+ ?????? ?????????? ???? ????????
                    {
                        // GameManager.Instance.AddPoint(); totalpoint++;
                    }
                    GreyB[i, j].SetActive(false);
                    redB[i, j].SetActive(false);
                    checker[i, j] = false;
                }
                if (Wclearline[j] == true)//???????? ????????
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
            if(scoresuppression ==false)
            {
                for (int i =0; i<totalpoint; i++)
                {
                    GameManager.Instance.AddPoint();
                }
                EnemysManager.Instance.currentEnemyDamage(totalpoint);
            }
            else
            {
                GameManager.Instance.AddPoint();
                EnemysManager.Instance.currentEnemyDamage(1);
            }
        }
        

    }
    public bool IsCombo()
    {
        iscombo = true;   
        return iscombo;
    }

    void CheckIfPlayerLost()//?????? ???????? ???????? ???? ????
    {
        bool GameoverCheck = true;
        for (int j = 0; j < height; j++)
        {
            for (int i = 0; i < width; i++)
            {
                for (int r = 0; r < Data.RotationMatrix.Length; r++)//?????? 4?????? ?????????? ????
                {
                    bool caninstblock = false;// ???????? ???? ????
                    foreach (Vector2Int offs in setdata.activepiece.cells)//?????? ???????? ?????? ???????? ????
                    {
                        if ((offs.x + i) < 0 || (offs.x + i) >= width || (offs.y + j) < 0 || (offs.y + j) >= height)
                        {
                            caninstblock = true;// ???? ???????? ?????????????? ????
                        }

                        if ((offs.x + i) >= 0 && (offs.x + i) < width && (offs.y + j) >= 0 && (offs.y + j) < height)
                        {
                            if (checker[offs.x + i, offs.y + j] == true)
                            {
                                caninstblock = true;
                            }
                        }
                    }
                    bool caninstsaveblock = false;//?????? ???????? ???? ???? , ???????????? ?????????? ?????? ????????
                    if (blockSave.piece.cells != null)
                    {

                        foreach (Vector2Int offs in blockSave.piece.cells)//?????? ???????? ?????? ???????? ????
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

                    if (caninstblock == false || caninstsaveblock == false)//???????????? ???????? ????????
                    {
                        GameoverCheck = false;
                    }
                    setdata.activepiece.Rotater(1);// ?????????? ?????????? ????
                    if (blockSave.piece.cells != null)// ?????? ?????? ?????? ?????? ?????? ?????????? ????
                    {
                        blockSave.piece.Rotater(1);
                    }
                }
            }
        }

        if (GameoverCheck == true)// ???????? ????
        {
            GameOver();
        }
    }
   
    public void playerDamage()
    {
        GameManager.Instance.hp--;
        if (GameManager.Instance.hp<1)
        {
            GameOver();
        }
    }
    public void GameOver()
    {
        GameManager.Instance.GameOver = true;
        GameManager.Instance.gamestart = false;
        GameoverUI.SetActive(true);
        if (GameManager.Instance.viewpoint > GameManager.Instance.highscore[0])
        {
            NewScore.gameObject.SetActive(true);
        }
        else
        {
            NewScore.gameObject.SetActive(false);
        }
        LastScore.text = $"{GameManager.Instance.viewpoint}";
        Debug.Log(" Gameover");
    }
    public bool isSetB()
    {
        setBlock = true;
        return setBlock;
    }
    public void aeraAllClear()//?? ???
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
    public void Savearea()// ?????? ???????? ???????????? ??????
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
            switch (EnemysManager.Instance.bosss[EnemysManager.Instance.bossindex].bossType)// ???? ???? ???? ????
            {
                case BossType.sinmon:
                    simonSkill();
                    break;
                case BossType.BigMeteo:
                    bigMeteobossSkill();
                    break;
                case BossType.oneclick:
                    OneClickSkill();
                    oneclickBossOn = true;
                    break;
            }
        }

        if(EnemysManager.Instance.bossactiv == false)
        {
            oneclickBossOn = false;
            scoresuppression = false;
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
    
    void OneClickSkill()
    {
        setdata.activepiece.Initialize(setdata.datablock[0]);
        setdata.blockview();
        if (oneclickBossOn == false)
            return;
        scoresuppression = true;
        Vector2Int skillrange = new Vector2Int(Random.Range(0, GameManager.Instance.width), Random.Range(0, GameManager.Instance.height));
        for (int j = 0; j < height; j++)
        {
            for (int i = 0; i < width; i++)
            {
                if (i == skillrange.x && j == skillrange.y)
                {
                    redB[i, j].SetActive(false);
                    GreyB[i, j].SetActive(false);
                    checker[i, j] = false;
                    continue;
                }
                    
                redB[i, j].SetActive(true);
                GreyB[i, j].SetActive(true);
                checker[i, j] = true;
            }
        }
        Scenemamhincrit.Instance.StartShake(1,0.4f);
    }
    
}


