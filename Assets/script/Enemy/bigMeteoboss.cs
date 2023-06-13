using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bigMeteoboss : BossEnmey
{
    private void Start()
    {
        base.bossType = BossType.BigMeteo;
    }

    // Start is called before the first frame update
    public override void Update()
    {
        base.Update();
        this.transform.Rotate(Vector3.back);
    }
}
