using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockEffect : MonoBehaviour
{
    float effecttime;
    SpriteRenderer effecblock;//��������Ʈ ��ϸ�翡 ���� ���� ����� �迭�� �����Ѵ��� ����� ȸ���ε����� ��ϸ���� ������ �׸��� �������� ȣ��
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
        effect.localScale = new Vector3(effect.localScale.x + (speed * Time.deltaTime), effect.localScale.y + (speed * Time.deltaTime), effect.localScale.z);//��Ÿ Ÿ������ ����Ͽ����� �����ӵ��� ����
        effecblock.color = new Color(effecblock.color.r, effecblock.color.g, effecblock.color.b, ((Lifetime - effecttime) / Lifetime));
        if (effecttime > Lifetime)
        {
            Destroy(this.gameObject);
        }
    }

}
