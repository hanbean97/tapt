using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bigMeteoboss : BossEnmey
{
    [SerializeField] Transform target;
    [SerializeField] aeraT aera;
    private void Start()
    {
        base.bossType = BossType.BigMeteo;
    }

    // Start is called before the first frame update
    public override void Update()
    {
        base.Update();
        this.transform.Rotate(Vector3.back);
        if(target.position.y+0.5f>transform.position.y)
        {
            Scenemamhincrit.Instance.StartShake(1f, 1f);
            aera.playerDamage();
            EnemyDamage(ViewMaxHp);
            aera.playerDamage();
        }
    }
}
