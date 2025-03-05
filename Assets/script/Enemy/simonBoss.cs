using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simonBoss : BossEnmey
{
    [SerializeField] Transform[] ball;
    [SerializeField] float ballspeed;
    [SerializeField] float nextPostime;
    float timer;
    Vector2[] nextPos = new Vector2[4];
    private void Start()
    {
        base.bossType = BossType.sinmon;
        for (int i = 0; i < ball.Length; i++)
        {
            nextPos[i] = new Vector2(Random.Range(-0.5f, 6.5f), Random.Range(-0, 5.5f));
        }
    }
    public override void Update()
    {
        base.Update();
        BallMove();
    }

    void BallMove()
    {
        timer += Time.deltaTime;
        if (timer> nextPostime)
        {
            for (int i = 0; i < ball.Length; i++)
            {
                nextPos[i] = new Vector2(Random.Range(-0.5f,6.5f), Random.Range(-0,5.5f));  
            }
            timer = 0;
        }

        for (int i = 0; i < ball.Length; i++)
        {
            ball[i].position = Vector2.Lerp(ball[i].position, nextPos[i], ballspeed * Time.deltaTime);;
        }
    }
}
