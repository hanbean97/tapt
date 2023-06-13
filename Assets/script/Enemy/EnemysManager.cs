using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemysManager : Singltons<EnemysManager>
{
    public Enemy[] enemies;
    public BossEnmey[] bosss;
    List<int> enemiesindextable = new List<int>();
    List<int> bosssindextable =new List<int>();
    int nowEnemyindex = 0;
    public int bossindex =0;
    bool enemyactiv;
    public bool bossactiv;
    [SerializeField] int bossMeetNeed = 2;
    bool bossMeet = false;
    [SerializeField] int bossmeetpoint;
    [Range(0,15)]
    public int level;// ÀúÀå
    // Start is called before the first frame update
    void Start()
    {
        enemyactiv = false;
        bossactiv = false; 
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
                bossindex++;
                if(bossindex > bosss.Length)
                {
                    bossindex = 0;
                }
                bossactiv = false;
                bossMeet = false; 
                bossmeetpoint = 0;
                level++;
            }
        }
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
}
