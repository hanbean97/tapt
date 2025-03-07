using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescriptionSc : MonoBehaviour
{
    [SerializeField] GameObject Tutori;
    private void Start()
    {
        if(GameManager.Instance.notfirstPlayer == false)
        {
            Tutori.SetActive(true);
            GameManager.Instance.notfirstPlayer = true;
            SaveLoad.BasicSaveGame();
        }
    }

    public void CloseTuto()
    {
        Tutori.SetActive(false);
    }
}
