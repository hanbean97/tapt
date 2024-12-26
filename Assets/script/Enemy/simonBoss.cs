using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simonBoss : BossEnmey
{
    [SerializeField] GameObject[] ball;
    private void Start()
    {
        base.bossType = BossType.sinmon;
    }
    public override void Update()
    {
        base.Update();
        
    }
}
