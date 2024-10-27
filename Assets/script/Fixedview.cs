using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fixedview : MonoBehaviour
{
    [SerializeField] int setWidth = 1080;//���ϴ� ���� �ʺ�
    [SerializeField]int setHeight = 1920;//���ϴ� ���� ����
    int deviceWidth = Screen.width; // ���ʺ�
    int deviceHeight = Screen.height;//��� ����
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
       //Screen.SetResolution((int)(((float)deviceWidth / deviceHeight) * setHeight), setHeight, true);//SetResolution ��������
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
