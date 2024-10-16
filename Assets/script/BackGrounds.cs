using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGrounds : MonoBehaviour
{
    [SerializeField] Sprite[] SpaceWorld;
    SpriteRenderer sprR;
    [SerializeField] float speed =1;
    int nowBackstage=0;
    int nowLevel =0;
    private void Awake()
    {
        sprR = GetComponent<SpriteRenderer>();
    }
    void Update()
    {

    }
}
