using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGrounds : MonoBehaviour
{
    [SerializeField] Sprite[] SpaceWorld;
    SpriteRenderer sprR;
    Vector2 offsets;
    [SerializeField] float speed =1;
    int nowBackstage=0;
    int nowLevel =0;
    private void Awake()
    {
        sprR = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
       if(GameManager.Instance.gamestart == true &&nowLevel != EnemysManager.Instance.level)
        {
            nowBackstage++;
            if(nowBackstage >= SpaceWorld.Length)
            {
                nowBackstage = 0;
            }
            sprR.sprite = SpaceWorld[nowBackstage];
            nowLevel = EnemysManager.Instance.level;
        }
       offsets.y += speed*Time.deltaTime;
        sprR.material.SetTextureOffset("_MainTex",offsets);
    }
}
