using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fixedview : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        SetReasolution();
    }
    public void SetReasolution()
    {
        int setWidth = 1920;//���ϴ� ���� �ʺ�
        int setHeight = 1080;//���ϴ� ���� ����

        int deviceWidth = Screen.width; // ���ʺ�
        int deviceHeight = Screen.height;//��� ����

        Screen.SetResolution(setWidth, (int)(((float)deviceHeight / deviceWidth) * setWidth), true);//SetResolution
     //   Screen.SetResolution((int)(((float)deviceWidth / deviceHeight) * setHeight), setHeight, true);//SetResolution ��������
        if ((float)setWidth / setHeight < (float)deviceWidth / deviceHeight)//����� �ػ󵵺� ��ū���
        {
            float newWidth = ((float)setWidth / setWidth) / ((float)deviceWidth / deviceHeight);//���ο�ʺ�
            Camera.main.rect = new Rect((1f - newWidth) / 2f, 0f, newWidth, 1f);// ���ο� Rect����
        }
        else // ������ ���� �� ��ū���
        {
            float newHeight = ((float)deviceWidth / deviceHeight) / ((float)setWidth / setHeight); // ���ο����
            Camera.main.rect = new Rect(0f, (1f - newHeight) / 2f,1f, newHeight);// ���ο� Rect����
        }




    }    
}
