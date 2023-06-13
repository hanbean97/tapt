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
    // DontDestrotOnLoad(gameObject); - 다음씬에 가지고 가고싶을때
    }
    // Update는 내부에서만 접근자(자식)은 쓸수없음
    
    protected virtual void OnDestroy()
    {
        if(instance == this)
        {
            instance= null;
        }
    }


}
