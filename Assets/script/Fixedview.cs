using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fixedview : MonoBehaviour
{
    [SerializeField] int setWidth = 1080;//���ϴ� ���� �ʺ�
    [SerializeField]int setHeight = 1920;//���ϴ� ���� ����

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
        int deviceWidth = Screen.width; // ���ʺ�
        int deviceHeight = Screen.height;//��� ����
      //Screen.SetResolution(setWidth, (int)(((float)deviceHeight / deviceWidth) * setWidth), true);//SetResolution
       Screen.SetResolution((int)(((float)deviceWidth / deviceHeight) * setHeight), setHeight, true);//SetResolution ��������
        if ((float)setWidth / setHeight < (float)deviceWidth / deviceHeight)//����� �ػ󵵺� ��ū���
        {
            float newWidth = ((float)setWidth / setWidth) / ((float)deviceWidth / deviceHeight);//���ο�ʺ�
            Camera.main.rect = new Rect((1f - newWidth) / 2f, 0f, newWidth, 1f);// ���ο� Rect����
        }
        else // ������ �ػ� �� ��ū���
        {
            float newHeight = ((float)deviceWidth / deviceHeight) / ((float)setWidth / setHeight); // ���ο����
            Camera.main.rect = new Rect(0f, (1f - newHeight) / 2f,1f, newHeight);// ���ο� Rect����
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
