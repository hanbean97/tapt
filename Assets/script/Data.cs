using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data
{
    public static readonly float cos = Mathf.Cos(Mathf.PI / 2);
    public static readonly float sin = Mathf.Sin(Mathf.PI / 2);
    public static readonly float[] RotationMatrix = new float[] { cos, sin, -sin, cos };
   
    public static readonly Dictionary<bluckType, Vector2Int[]> Cell = new Dictionary<bluckType, Vector2Int[]>()
    {
        { bluckType.O ,new  Vector2Int[]{ new Vector2Int(0,0)} },
        { bluckType.I,new Vector2Int[] { new Vector2Int(0,0), new Vector2Int(0, 1) } },
        { bluckType.II, new  Vector2Int[] {  new Vector2Int(0,0), new Vector2Int(0, 1), new Vector2Int(0, -1) } },
        { bluckType.L, new  Vector2Int[] {  new Vector2Int(0,0), new Vector2Int(0, 1), new Vector2Int(1, 0) } },
        { bluckType.T, new  Vector2Int[] {  new Vector2Int(0,0), new Vector2Int(0, 1), new Vector2Int(1, 0), new Vector2Int(0,-1 ) } },
        { bluckType.X,new  Vector2Int[]{ new Vector2Int(0,0), new Vector2Int(0, 1), new Vector2Int(1, 0), new Vector2Int(0, -1),new Vector2Int(-1, 0) }  },
        { bluckType.None, null }
    };
    
}
