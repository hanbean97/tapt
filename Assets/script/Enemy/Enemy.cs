using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]int StartEnemyHp;
    [SerializeField] int EnemyHp;
    public int ViewHp { get { return EnemyHp; } }
    public int ViewMaxHp;
    public Transform movePosition;
    public Transform sponPosition;
    protected  bool movech =false;
    public bool EnemyDeth = false;
    protected Animator ani;
    bool isDamag = false;
    float damagetime;
    [SerializeField] float speed;
    float nowPos;
    // Start is called before the first frame update


    public virtual void OnEnable()
    {
        ViewMaxHp = StartEnemyHp + (int)(StartEnemyHp * EnemysManager.Instance.level*0.5f);
        EnemyHp = ViewMaxHp;
        if(EnemysManager.Instance.firstgam == false &&GameManager.Instance.Loadch== true)
        {
            EnemyHp = GameManager.Instance.Enemyhp;
            EnemysManager.Instance.firstgam = true;
        }
        EnemyDeth = false;
        movech = false;
        this.transform.position = sponPosition.transform.position;
        ani = GetComponent<Animator>();
        if(speed == 0)
        {
            speed = 5;
        }
    }

    // Update is called once per frame
    public virtual void Update()
    {

        if (movech == false)
        {
            this.transform.position =new Vector2(this.transform.position.x, this.transform.position.y - (speed * Time.deltaTime));
            float dis = this.transform.position.y - movePosition.transform.position.y;
            if (dis < 0.5)
            {
                movech = true;
            }
        }
        if(isDamag == true)// 진동효과
        {
            damagetime += Time.deltaTime;
            this.transform.position= new Vector2(nowPos+(10*(Mathf.Sin(150* Time.time) )* Time.deltaTime), this.transform.position.y);
        if (damagetime> 1f)
            {
                
                isDamag = false;
                damagetime = 0f;
             this.transform.position = new Vector2(nowPos, this.transform.position.y);
            }
        }

    }


    public void EnemyDamage(int damage)
    {
        EnemyHp -= damage;
        nowPos = this.transform.position.x;
        isDamag = true;
        if (EnemyHp <= 0 && EnemyDeth == false)
        {
            EnemyDeth = true;
            this.gameObject.SetActive(false);
        }
    }

}


public enum EnemyType
{
    nomal,
    boss,
}
public enum BossType
{
    sinmon,
    BigMeteo,
    none
}