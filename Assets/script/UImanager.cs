using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UImanager : MonoBehaviour
{
    public TextMeshProUGUI PointUI;
    public TextMeshProUGUI Combo;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        PointUI.text = GameManager.Instance.viewpoint.ToString();
        Combo.text = "Combo :"+GameManager.Instance.nowcombo.ToString();
    }
}
