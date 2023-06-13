using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum bluckType
{
    O,//1개 
    I,//2개 일자
    II,//긴3개 일자
    L,//3개 L자
    T,//4개 T자
    X,//5개 십자가
    None //블럭없음
}

[System.Serializable]
public class Databiuck 
{
    public Vector2Int[] typecell;
    public bluckType BType;
  
    public void Initialize()
    {
        typecell = Data.Cell[BType];
    }
}


