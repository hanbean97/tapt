using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fixedview : MonoBehaviour
{
    [SerializeField] int setWidth = 1080;//원하는 설정 너비
    [SerializeField]int setHeight = 1920;//원하는 설정 높이

    void Awake()
    {
        SetReasolution1();
    }
    private void Update()
    {
        SetReasolution1();
    }
    public void SetReasolution1()
    {
        int deviceWidth = Screen.width; // 기기너비
        int deviceHeight = Screen.height;//기기 높이
      //Screen.SetResolution(setWidth, (int)(((float)deviceHeight / deviceWidth) * setWidth), true);//SetResolution
       Screen.SetResolution((int)(((float)deviceWidth / deviceHeight) * setHeight), setHeight, true);//SetResolution 높이중점
        if ((float)setWidth / setHeight < (float)deviceWidth / deviceHeight)//기기의 해상도비가 더큰경우
        {
            float newWidth = ((float)setWidth / setWidth) / ((float)deviceWidth / deviceHeight);//새로운너비
            Camera.main.rect = new Rect((1f - newWidth) / 2f, 0f, newWidth, 1f);// 새로운 Rect적용
        }
        else // 게임의 해상도 비가 더큰경우
        {
            float newHeight = ((float)deviceWidth / deviceHeight) / ((float)setWidth / setHeight); // 새로운높이
            Camera.main.rect = new Rect(0f, (1f - newHeight) / 2f,1f, newHeight);// 새로운 Rect적용
        }
    }
    public void SetReasolution2()
    {
        float res = ((float)setWidth / setHeight);

        int deviceWidth = Screen.width;
        int deviceHeight = Screen.height;
        float deviceRes = ((float)deviceWidth / deviceHeight);

       Screen.SetResolution(setWidth,(int)(((float)deviceHeight/deviceWidth)*setWidth),true);

        if(res <deviceRes)
        {
            float newWidth = res/deviceWidth;
            Camera.main.rect = new Rect((1f-newWidth)/2f,0f,newWidth,1f);
        }
        else
        {
            float newHeight = deviceRes / res;
            Camera.main.rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight);
        }
    }
    /*
    #region CoverImage
    [Header("[Cover]")]
    public RectTransform topCover;
    public RectTransform bottomCover;

    private void CoverInit()
    {
        var rect = Camera.main.rect;

        topCover.anchorMin = new Vector2(0f, 1f - rect.y);
        topCover.anchorMax = Vector2.one;
        topCover.anchoredPosition = Vector2.zero;

        bottomCover.anchorMin = Vector2.zero;
        bottomCover.anchorMax = new Vector2(1f, rect.y);
        bottomCover.anchoredPosition = Vector2.zero;
    }
    #endregion*/
}
