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
        if (GameManager.Instance.rankname.Length != 0)
        {
            for (int i = 0; i < GameManager.Instance.rankname.Length; i++)
            {
                GameObject ranker = Instantiate(RankPrefab, RankMenu);
                TextMeshPro[] RankText = ranker.GetComponentsInChildren<TextMeshPro>();
                RankText[0].text = GameManager.Instance.rankname[i];
                RankText[1].text = GameManager.Instance.highscore[i].ToString();
            }
        }
    }
}
