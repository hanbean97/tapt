using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fixedview : MonoBehaviour
{
    [SerializeField] int setWidth = 1080;//원하는 설정 너비
    [SerializeField]int setHeight = 1920;//원하는 설정 높이
    int deviceWidth = Screen.width; // 기기너비
    int deviceHeight = Screen.height;//기기 높이
    private void Start()
    {
        SetReasolution1();
    }
    private void Update()
    {
       // SetReasolution2();
    }
    public void SetReasolution1()
    {
       
       
        Screen.SetResolution(setWidth, (int)(((float)deviceHeight / deviceWidth) * setWidth), true);//SetResolution
       //Screen.SetResolution((int)(((float)deviceWidth / deviceHeight) * setHeight), setHeight, true);//SetResolution 높이중점
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
        Screen.SetResolution(setWidth, setHeight, true);
        float r0 = (float)setWidth / setHeight;
        float r1 = (float)deviceWidth/deviceHeight;

        if(r0<r1)
        {
            float w = r0 / r1;
            Camera.main.rect = new Rect((1f-w)/2,0,w,1);
        }
        else
        {
            float h = r1 / r0;
            Camera.main.rect = new Rect(0,(1f-h)/2,1,h);
        }


    }

    public void SetReasolution3()
    {
        Camera camera = GetComponent<Camera>();
        Rect rect = camera.rect;
        float scaleheight = ((float)Screen.width / Screen.height) / ((float)9/ 16);
        float scalewidth = 1f / scaleheight;
        if(scaleheight <1)
        {
            rect.height = scaleheight;
            rect.y = (1f-scaleheight)/2f;
        }
        else
        {
            rect.width = scalewidth;
            rect.x = (1f - scalewidth) / 2f;
        }
        camera.rect = rect; 
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
