using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simonBoss : BossEnmey
{
    [SerializeField] Transform[] ball;
    [SerializeField] float balldistance;
    [SerializeField] float ballspeed;
    float Sinmove;
    byte bo4;
    private void Start()
    {
        base.bossType = BossType.sinmon;
    }
    public override void Update()
    {
        Sinmove = Mathf.Sin(Time.deltaTime);
        base.Update();
        for (int i = 0; i < ball.Length; i++)
        {
            switch(i%2)
            {
                case 0:
                    //n-dis
                    ball[i].position = new Vector2(ball[i].position.x + Sinmove, ball[i].position.y + Sinmove);

                    ball[i].GetComponent<SpriteRenderer>().sortingOrder = 0;
                    break;
                case 1:
                    ball[i].position = new Vector2(ball[i].position.x - Sinmove, ball[i].position.y - Sinmove);
                    if(Sinmove>0)
                    {
                        ball[i].GetComponent<SpriteRenderer>().sortingOrder = 2;
                    }     
                    break; 

            }

           // ball[i].transform 
        }
       
    }

    float DisCalcul()
    {
        return 0;
    }
}
