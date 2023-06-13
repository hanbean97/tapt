using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockEffect : MonoBehaviour
{
    float effecttime;
    SpriteRenderer effecblock;//스프라이트 블록모양에 맞춰 새로 만들고 배열로 보관한다음 블록의 회전인덱스와 블록모양을 받은뒤 그모양과 방향으로 호출
    Transform effect;
    [SerializeField] float Lifetime = 0.5f;
    [SerializeField] float speed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        effect = GetComponent<Transform>();
        effecblock = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        effecttime += Time.deltaTime;
        effect.localScale = new Vector3(effect.localScale.x + (speed * Time.deltaTime), effect.localScale.y + (speed * Time.deltaTime), effect.localScale.z);//델타 타임으로 모바일에서도 같은속도로 조정
        effecblock.color = new Color(effecblock.color.r, effecblock.color.g, effecblock.color.b, ((Lifetime - effecttime) / Lifetime));
        if (effecttime > Lifetime)
        {
            Destroy(this.gameObject);
        }
    }

}
