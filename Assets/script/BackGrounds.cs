using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGrounds : MonoBehaviour
{
    [SerializeField] Sprite[] SpaceWorld;
    [SerializeField] Transform[] groundsObj;
    SpriteRenderer sprR;
    [SerializeField] float speed =1;
    float sprScale;
    int nowBackstage=0;
    int nowLevel =0;
    private void Awake()
    {
        //한ㅘㄹ

        sprR = GetComponent<SpriteRenderer>();
        sprScale = SpaceWorld[1].bounds.size.y * groundsObj[1].localScale.y;
      
    }
    void Update()
    {
        for (int i = 0; i < 3; i++)
        {
            groundsObj[i].Translate(Vector2.down * speed * Time.deltaTime);//스프라이트 스케일
            if (groundsObj[i].position.y < -sprScale)
            {
                groundsObj[i].position = new Vector3(groundsObj[i].position.x , sprScale*2, groundsObj[i].position.z);
            }
        }

    }
    private void OnBecameInvisible()
    {
        
    }
}
