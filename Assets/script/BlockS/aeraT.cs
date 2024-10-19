using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aeraT : MonoBehaviour
{
    //블록의 생성할수있는 판의 크기
    public int width;
    public int height;
    //데이터
    static Transform[,] tgrid;// 자식들(소환할 위치)을 관리할 트랜스폼
    private bool[,] checker;
    Transform CurrentTouch;
    public GameObject RedBlock;
    public GameObject GreyBlock;
    public GameObject effectblock;
    //블럭들의 보여주는것을 관리하는 게임 오브젝트
    GameObject[,] redB;
    GameObject[,] GreyB;
    // 터치시 나올 블럭을 보여줌
    public previewbluck setdata;
    Vector3 Mouseposition;
    // 블럭을 저장하여 원할때 꺼내 사용하기위한 클래스
    public BlockSave blockSave;
    // 라인제거시 효과 
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
                //로드 정보가 있을시 추가로 정보를 입력한다
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
                        instbluck(hit.transform);// 터치부분 
                        CheckIfPlayerLost();// 게임오버체크

                    }
                    break;
            }
            Input.touchCount =0;
        }
#endif


        if (Input.GetMouseButtonDown(0) && GameManager.Instance.GameOver == false) // 터치레이로 바꿀예정
        {
            Mouseposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(Mouseposition, transform.forward, 15f);

            if (hit.collider != null && UI.activeSelf != true)
            {
                CurrentTouch = hit.transform;
                instbluck(hit.transform);// 터치부분 
                
                CheckIfPlayerLost();// 게임오버체크
            }
        }

        if (setdata.saveBTch == true && GameManager.Instance.GameOver == false)//블록을 세이브 했을 경우 게임오버 체크
        {
            CheckIfPlayerLost();
            Savearea();//블록을 스톡했을때 데이터를 저장
            SaveLoad.SaveGame();
        }
    }

    void instbluck(Transform curren)// 블록 소환 함수
    {
        for (int j = 0; j < height; j++)
        {
            for (int i = 0; i < width; i++)
            {
                if (tgrid[i, j] == curren && checker[i, j] == false)// 터치한 부분 찾기
                {
                    bool dontinst = true;
                    foreach (Vector2Int offs in setdata.activepiece.cells)// 배치실패 체크
                    {// 활성불가한 사항 여기다가
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

                    if (dontinst == true)//배치 
                    {
                        foreach (Vector2Int offs in setdata.activepiece.cells)
                        {
                            if ((offs.x + i) >= 0 && (offs.x + i) < width && (offs.y + j) >= 0 && (offs.y + j) < height)
                            {
                                if (checker[offs.x + i, offs.y + j] == false)
                                {
                                    if (offs.x == 0 && offs.y == 0)//블록 배치
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
                        //벽돌 배치 성공효과
                        setdata.SpawnPiece(); // 프리뷰블럭의 블록정보 초기화
                        ClearBluck(); //라인 체크후 제거
                        bossSkill();// 보스스킬
                        Savearea();// 배치할때 현재 게임매니저에 상황을 저장
                        SaveLoad.SaveGame();
                       
                    }
                    else
                    {
                        //배치실패시 나타나는 효과
                    }
                }
                else if (tgrid[i, j] == curren && checker[i, j] == true)
                {
                    // 블록있는곳 잘못터치효과= 배치 실패 효과
                }
            }
        }
    }




    void ClearBluck()// 라인체크+제거
    {
        int widthsum = 0;//가로줄 체크
        int heightsum = 0;// 세로줄 체크
        int bonuspoint = 0;// 제거된 라인 추가점수
        bool combosuccess = false;// 콤보 성공여부
        // 제거할 라인을 가리킴
        bool[] Wclearline = new bool[height];
        bool[] Hclearline = new bool[width];
        int totalpoint =0;
        for (int j = 0; j < height; j++)
        {
            for (int i = 0; i < width; i++)//가로줄을 체크
            {
                if (i == 0)// 다음라인을 체크할시 0으로 초기화
                {
                    Wclearline[j] = false;
                    widthsum = 0;
                }

                if (checker[i, j] == true)
                {
                    widthsum++;//가로줄에있는 블록의 갯수만큼 +
                }
                if (widthsum == width)// 가로줄과 크기가 같으면 해당라인 제거 효과
                {
                    Wclearline[j] = true;
                    bonuspoint++;// 라인한번에 여러개 제거시 보너스점수
                    GameManager.Instance.AddPoint();// 라인 제거시 1점 + 클리어시 효과
                    totalpoint++;
                    GameObject xlineeffect = Instantiate(LineClear, new Vector3(tgrid[i, j].transform.position.x + xLineoffset, tgrid[i, j].transform.position.y, tgrid[i, j].transform.position.z), Quaternion.identity);
                    Destroy(xlineeffect, 0.3f);//라인제거 효과 소환
                    combosuccess = true;//콤보 성공
                }

            }
        }
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)//세로줄을 체크
            {
                if (j == 0)// 다음라인을 체크할시 0으로 초기화
                {
                    Hclearline[i] = false;
                    heightsum = 0;
                }
                if (checker[i, j] == true)
                {
                    heightsum++;// 세로줄 체크
                }
                if (heightsum == height)// 세로줄과 크기가 같으면 해당라인 제거 효과
                {
                    Hclearline[i] = true;
                    bonuspoint++;// 라인한번에 여러개 제거시 보너스점수
                    GameManager.Instance.AddPoint();// 라인 제거시 1점 
                    totalpoint++;
                    GameObject hlineeffect = Instantiate(LineClear, new Vector3(tgrid[i, j].transform.position.x, tgrid[i, j].transform.position.y + yLineoffset, tgrid[i, j].transform.position.z),
                          Quaternion.Euler(0, 0, 90));//라인제거 효과 소환
                    Destroy(hlineeffect, 0.3f);
                    combosuccess = true;

                }
            }
        }
        if (bonuspoint > 2)// 3줄이상 한번에 제거시 2배 점수 계산상 총 6개의 줄을 한번에삭제가능
        {
            for (int i = 0; i < bonuspoint; i++)
            {
                GameManager.Instance.AddPoint();
                totalpoint++;
            }
        }
        if (combosuccess == true)// 콤보
        {
            Combo++;
        }
        else// 콤보 실패시 초기화
        {
            Combo = 0;
        }
        if (Combo > 2)// 콤보유지하면 추가점수 1점
        {
            GameManager.Instance.AddPoint();
            totalpoint++;
        }

        for (int j = 0; j < height; j++)// 블록을 비활성화
        {
            for (int i = 0; i < width; i++)
            {
                if (Hclearline[i] == true)//해당라인 비활성화
                {
                    if (redB[i, j].activeSelf == true)// 빨간블록 제거시 추가 점수 1점+ 밸런스 조정을위해 잠시 주석처리
                    {
                        // GameManager.Instance.AddPoint(); totalpoint++;
                    }
                    GreyB[i, j].SetActive(false);
                    redB[i, j].SetActive(false);
                    checker[i, j] = false;
                }
                if (Wclearline[j] == true)//해당라인 비활성화
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

    void CheckIfPlayerLost()//하나씩 넣어봐서 게임오버 여부 체크
    {
        bool GameoverCheck = true;
        for (int j = 0; j < height; j++)
        {
            for (int i = 0; i < width; i++)
            {
                for (int r = 0; r < Data.RotationMatrix.Length; r++)//블록의 4방향을 돌려가면서 체크
                {
                    bool caninstblock = false;// 블록대입 실패 여부
                    foreach (Vector2Int offs in setdata.activepiece.cells)//프리뷰 블록안의 정보를 넣으면서 체크
                    {
                        if ((offs.x + i) < 0 || (offs.x + i) >= width || (offs.y + j) < 0 || (offs.y + j) >= height)
                        {
                            caninstblock = true;// 대입 가능하면 대입가능하다소 알림
                        }

                        if ((offs.x + i) >= 0 && (offs.x + i) < width && (offs.y + j) >= 0 && (offs.y + j) < height)
                        {
                            if (checker[offs.x + i, offs.y + j] == true)
                            {
                                caninstblock = true;
                            }
                        }
                    }
                    bool caninstsaveblock = false;//세이브 블록대입 실패 여부 , 세이브블록이 없는경우가 있으서 따로체크
                    if (blockSave.piece.cells != null)
                    {

                        foreach (Vector2Int offs in blockSave.piece.cells)//세이브 블럭안의 정보를 넣으면서 체크
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

                    if (caninstblock == false || caninstsaveblock == false)//둘중하나라도 가능하면 게임계속
                    {
                        GameoverCheck = false;
                    }
                    setdata.activepiece.Rotater(1);// 가능한블록 돌아가면서 체크
                    if (blockSave.piece.cells != null)// 저장된 블록이 있으면 저장된 블럭도 돌아가면서 체크
                    {
                        blockSave.piece.Rotater(1);
                    }
                }
            }
        }

        if (GameoverCheck == true)// 게임오버 효과
        {
            GameManager.Instance.GameOver = true;
            GameManager.Instance.gamestart = false;
            GameoverUI.SetActive(true);
            Debug.Log(" Gameover");
        }
    }
    public void aeraAllClear()//블록 전부 비활성화
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
    public void Savearea()// 게임의 데이터를 게임매니져에 보낸다
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
            switch (EnemysManager.Instance.bosss[EnemysManager.Instance.bossindex].bossType)// 보드 관련 보스 스킬
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


