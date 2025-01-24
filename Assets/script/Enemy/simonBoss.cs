using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simonBoss : BossEnmey
{
    [SerializeField] Transform[] ball;
    [SerializeField] float balldistance;
    [SerializeField] float ballspeed;
    private void Start()
    {
        base.bossType = BossType.sinmon;
    }
    public override void Update()
    {
        base.Update();
        for (int i = 0; i < ball.Length; i++)
        {
            switch(i%2)
            {
                case 0:
                    //n-dis
                   // Mathf.SmoothStep(,,);
                    break;
                case 1:
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
