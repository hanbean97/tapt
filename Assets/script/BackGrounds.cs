using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGrounds : MonoBehaviour
{
    SpriteRenderer sprR;
    Material mat;
    [SerializeField] float speed = 1;
    private void Awake()
    {
        sprR = GetComponent<SpriteRenderer>();
        mat = sprR.material;
    }
    void Update()
    {
        mat.mainTextureOffset += Vector2.up * speed * Time.deltaTime;
    }
}
