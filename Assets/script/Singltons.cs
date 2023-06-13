using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singltons<T> : MonoBehaviour where T : Singltons<T>
{
    private static T instance;
    public static T Instance
    {
        get { return instance; }
    }
    protected virtual void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = (T)this;
    // DontDestrotOnLoad(gameObject); - �������� ������ ���������
    }
    // Update�� ���ο����� ������(�ڽ�)�� ��������
    
    protected virtual void OnDestroy()
    {
        if(instance == this)
        {
            instance= null;
        }
    }


}
