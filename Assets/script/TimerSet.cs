using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class TimerSet : MonoBehaviour
{
    [SerializeField] TMP_Text NowTime;
    string googleurl = "www.google.com";

    private async void Start()//
    {
        await WebTime1();
    }

    async Task WebTime1()
    {
        UnityWebRequest request = new UnityWebRequest();
        using (request = UnityWebRequest.Get(googleurl))
        {
            var oper= request.SendWebRequest();
            while (oper.isDone) 
            await Task.Yield(); 
        }
           
    }

    IEnumerator WebTime2()//코루틴 전용
    {
        UnityWebRequest request = new UnityWebRequest();
        using (request = UnityWebRequest.Get(googleurl))
        {
            yield return request.SendWebRequest();
            if(request.isNetworkError)
            {
                Debug.Log(request.error);
            }
            else
            {
                string data = request.GetResponseHeader("data");
                Debug.Log(data);
            }
        }
    }


}
