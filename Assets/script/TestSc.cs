using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestSc : MonoBehaviour
{
    [Range(0,8)]
    [SerializeField] int[] MaxTest; 
    List<(string,int)> test = new List<(string, int)>() {("a",1),("z",2),("x",5),("q",0)};
    [SerializeField] GameObject uiui;
    void Start()
    {
        test.Sort((a,b)=> a.Item2.CompareTo(b.Item2));
        Debug.Log($"{test[0].Item1},{ test[0].Item2},{ test[1].Item1}, {test[1].Item2}");

        TextMeshProUGUI[]  chch = uiui.GetComponentsInChildren<TextMeshProUGUI>();
        Debug.Log(chch.Length);
       
    }
   
}
