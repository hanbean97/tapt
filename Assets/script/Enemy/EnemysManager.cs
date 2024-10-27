using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemysManager : Singltons<EnemysManager>
{
    public Enemy[] enemies;
    public BossEnmey[] bosss;
    List<int> enemiesindextable = new List<int>();
    List<int> bosssindextable =new List<int>();
    public int nowEnemyindex = 0;
    public int bossindex =0;
    bool enemyactiv;
    public bool bossactiv;
    [SerializeField] int bossMeetNeed = 2;
    public bool bossMeet = false;
    public int bossmeetpoint;
    [Range(0,15)]
    public int level;// ÀúÀå
    public int nowEnemyHp;
    public int nowEnemyMaxHp;
    public bool firstgam;
    void Start()
    {
        enemyactiv = false;
        bossactiv = false;
        firstgam = false;
        if (GameManager.Instance.Loadch == true)
        {
            bossMeet = GameManager.Instance.enemytype;
            bossmeetpoint = GameManager.Instance.bossmeetpoint;
            level = GameManager.Instance.nowlevel;
            nowEnemyindex = GameManager.Instance.nowEnemy;
            bossindex = GameManager.Instance.nowBoss;
            nowEnemyHp = GameManager.Instance.Enemyhp;
           
        }
        else
        {
            bossMeet = false;
            bossmeetpoint =0;
            level = 0;
            nowEnemyindex = 0;
            bossindex = 0;
            nowEnemyHp = 0;
         
        }
    }

    // Update is called once per frame
    void Update()
    {
       if(GameManager.Instance.gamestart == true)
        {
            if (enemies[nowEnemyindex].gameObject.activeSelf == false && enemyactiv == false && bossMeet == false)
            {
            
                enemies[nowEnemyindex].gameObject.SetActive(true);
                enemyactiv = true;
            }

           if(enemies[nowEnemyindex].EnemyDeth == true && enemyactiv == true && bossMeet == false)
           {
                nowEnemyindex = Random.Range(0, enemies.Length);
                enemyactiv =false;
                level++;
                bossmeetpoint++;
                if (bossmeetpoint == bossMeetNeed)
                {
                    bossMeet = true;
                }
            }

           if (bosss[bossindex].gameObject.activeSelf == false && bossMeet == true && bossactiv == false)
            {

                bosss[bossindex].gameObject.SetActive(true);
                bossactiv = true;
            }

           if (bosss[bossindex].EnemyDeth == true && bossactiv == true && bosss[bossindex].gameObject.activeSelf == false)
            {
                bossindex++;// 0, 1
                if(bossindex > bosss.Length-1)//
                {
                    bossindex = 0;
                }
                bossactiv = false;
                bossMeet = false; 
                bossmeetpoint = 0;
                level++;
            }
        }
        NowEnemyHpView();
    }

    public void currentEnemyDamage(int damage)
    {
        if(enemies[nowEnemyindex].gameObject.activeSelf == true)
        {
            enemies[nowEnemyindex].EnemyDamage(damage);
        }
        if(bosss[bossindex].gameObject.activeSelf == true)
        {
            bosss[bossindex].EnemyDamage(damage);
        }    
    }
    public void NowEnemyHpView()
    {
        if (enemies[nowEnemyindex].gameObject.activeSelf == true)
        {
            nowEnemyMaxHp = enemies[nowEnemyindex].ViewMaxHp;
            nowEnemyHp = enemies[nowEnemyindex].ViewHp;
        }
        if (bosss[bossindex].gameObject.activeSelf == true)
        {
            nowEnemyMaxHp = bosss[bossindex].ViewMaxHp;
            nowEnemyHp = bosss[bossindex].ViewHp;
        }
    }
}
