using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum bluckType
{
    O,//1�� 
    I,//2�� ����
    II,//��3�� ����
    L,//3�� L��
    T,//4�� T��
    X,//5�� ���ڰ�
    None //������
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


