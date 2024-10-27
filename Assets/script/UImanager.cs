using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    public TextMeshProUGUI PointUI;
    public TextMeshProUGUI Combo;
    [SerializeField] Image HpBar;
    float nowHp =1;//Çöhp
    [SerializeField] Image setTimer;
    [SerializeField] aeraT aera;
    [SerializeField] float settime;
    float Stimer=0;
    [SerializeField] Image[] playerHpObj;
    int playhp=0;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        PointUI.text = GameManager.Instance.viewpoint.ToString();
        ComboView();
        HPdelay();
        SetTimer();
        PlayerHpview();
    }
    void HPdelay()
    {
        if (EnemysManager.Instance.nowEnemyMaxHp!= 0)
        {
            nowHp = (float)EnemysManager.Instance.nowEnemyHp / EnemysManager.Instance.nowEnemyMaxHp;
          
            HpBar.fillAmount = Mathf.SmoothStep(HpBar.fillAmount, nowHp, Time.deltaTime*10f);
        }
    }
    void SetTimer()
    {
        if(GameManager.Instance.GameOver == false)
        {
            Stimer += Time.deltaTime;
            if (aera.setBlock == true)
            {
                Stimer = 0;
                aera.setBlock = false;
                setTimer.color = Color.white;
            }

            if (Stimer >= settime)
            {
                Stimer = 0;
                aera.playerDamage();
                setTimer.color = Color.white;
            }
            setTimer.fillAmount =(1f-( Stimer / settime));
            setTimer.color = Color.Lerp(setTimer.color, Color.red, setTimer.fillAmount); ;
        }
    }
    void PlayerHpview()
    {
       if(GameManager.Instance.hp != playhp)
        {
            playhp = GameManager.Instance.hp;
            for(int i =0; i< GameManager.Instance.startHp; i++)
            {
                if(i < playhp)
                { 
                    playerHpObj[i].gameObject.SetActive(true);
                }
                else
                {
                    playerHpObj[i].gameObject.SetActive(false);
                }
            }
        }
    }
    void ComboView()
    {
        Combo.text = "Combo :" + GameManager.Instance.nowcombo.ToString();
    }
}
