using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using System.Net.Http;
using System;
public class TimerSet : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI NowTime;
    string googleurl = "https://www.google.com";
    DateTime currentTime;
    bool TimeGet = false;
    
        private async void Start()//
        {
            NowTime.text = "Loding";
            await WebTime1(googleurl);
        }


        async Task WebTime1(string url)
        {
           using (HttpClient client = new HttpClient())
           {
            try
            {
                HttpResponseMessage response = await client.GetAsync(googleurl);
                response.EnsureSuccessStatusCode();//200 Ok응답 확인
                if(response.Headers.Date.HasValue)
                {
                   DateTimeOffset serverTimeUtc = response.Headers.Date.Value;// UTC표준 시간
#if UNITY_STANDALONE_WIN
                    TimeZoneInfo koreaTimezone = TimeZoneInfo.FindSystemTimeZoneById("Korea Standard Time");//또는 ("Asia/Seoul")
#elif UNITY_STANDALONE_OSX
       TimeZoneInfo koreaTimezone = TimeZoneInfo.FindSystemTimeZoneById("Asia/Seoul");
#elif UNITY_IOS
 TimeZoneInfo koreaTimezone = TimeZoneInfo.FindSystemTimeZoneById("Korea Standard Time");
#endif
                    DateTime koreaTimeFromServer = TimeZoneInfo.ConvertTime(serverTimeUtc,koreaTimezone).DateTime;

                    Debug.Log(koreaTimeFromServer);
                    currentTime = koreaTimeFromServer;
                    TimeGet = true;


                }
                else
                {
                    Debug.Log("가져온 정보에 Date 헤더가 없 ");
                    NowTime.text = "GetTime_Fail";
                }
            }
            catch(HttpRequestException excep)
            {
                Debug.Log($"요청오류 : {excep.Message}");
                NowTime.text = "GetTime_Fail";
            }
            catch(Exception e)
            {
                Debug.Log($"일반오류 : {e.Message}");
                NowTime.text = "GetTime_Fail";
            }

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
                string date = request.GetResponseHeader("date");
                Debug.Log(date);
            }
        }
    }


    private void Update()
    {
        if (TimeGet == false) return;

        currentTime = currentTime.AddSeconds(Time.deltaTime);
        NowTime.text = $"{currentTime.Hour} : {currentTime.Minute.ToString("D2")}";
    }


}
