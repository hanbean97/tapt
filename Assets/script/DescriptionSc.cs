using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DescriptionSc : MonoBehaviour
{
    [SerializeField] GameObject Tutori;
    private void Start()
    {
       // if(GameManager.Instance.notfirstPlayer == false)
            Tutori.SetActive(true);
            GameManager.Instance.notfirstPlayer = true;
            SaveLoad.BasicSaveGame();
            Time.timeScale = 0;
    }

    public void CloseTuto()
    {
        Tutori.SetActive(false);
        Time.timeScale = 1f;
    }
}
