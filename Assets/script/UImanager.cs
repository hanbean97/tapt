using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    public TextMeshProUGUI PointUI;
    public TextMeshProUGUI Combo;
    [SerializeField] Image HpBar;
    float nowHp =1;//??hp
    [SerializeField] Image setTimer;
    [SerializeField] aeraT aera;
    [SerializeField] float settime;
    float NowSetTime;
    float Stimer=0;
    [SerializeField] Image[] playerHpObj;
    int playhp=0;
    public bool isCombo = false;
    bool isviewCombo = false;
    [SerializeField]float combowiewtime= 1f;
     float combotime;
    void Start()
    {
        NowSetTime = settime;
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
                if(aera.OneClickBossOnOff == true)
                {
                    NowSetTime = 2f;
                }
                else
                {
                    NowSetTime = settime;
                }
            }

            if (Stimer >= NowSetTime)
            {
                Stimer = 0;
                aera.playerDamage();
                setTimer.color = Color.white;
                if (aera.OneClickBossOnOff == true)
                {
                    NowSetTime = 2f;
                }
                else
                {
                    NowSetTime = settime;
                }
            }
            setTimer.fillAmount =(1f-( Stimer / NowSetTime));
            setTimer.color = Color.Lerp(Color.red, Color.white, setTimer.fillAmount); ;
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
        if (isviewCombo == false &&isCombo == true)
        {
            isCombo = false;
            isviewCombo = true;
            Combo.gameObject.SetActive(true);
        }

        if(isviewCombo == true)
        {
            combotime += Time.deltaTime;
            Combo.transform.localScale = Vector2.Lerp(Vector2.one, Vector2.one * 1.5f, combotime / combowiewtime);
            Combo.color = new Color(Combo.color.r, Combo.color.g, Combo.color.b, combotime / combowiewtime);
            if (isCombo == true)
            {
                isCombo = false;
                combotime = 0;
            }

            if (combotime >= combowiewtime)
            {
                Combo.transform.localScale = Vector2.one;
                Combo.color = new Color(Combo.color.r, Combo.color.g, Combo.color.b, 0);
                combotime = 0;
                isCombo = false;
                Combo.gameObject.SetActive(false);
                isviewCombo = false;
            }
        }
      
    }
}
