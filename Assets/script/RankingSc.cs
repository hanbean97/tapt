using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RankingSc : MonoBehaviour
{
    [SerializeField] GameObject RankPrefab;
    [SerializeField] Transform RankMenu;
    private void Start()
    {
        if (GameManager.Instance.RankScore != null && GameManager.Instance.RankScore.Count != 0)
        {
            for (int i = 0; i < GameManager.Instance.RankScore.Count; i++)
            {
                GameObject ranker = Instantiate(RankPrefab, RankMenu);
                TextMeshPro[] RankText = ranker.GetComponentsInChildren<TextMeshPro>();
                RankText[0].text = GameManager.Instance.RankScore[i].Item1;
                RankText[1].text = GameManager.Instance.RankScore[i].Item2.ToString();
            }
        }
    }
}
