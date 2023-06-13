using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentSingleton<T> : Singltons<T> where T : Singltons<T>
{
    protected override void Awake()
    {
        base.Awake();

        DontDestroyOnLoad(gameObject);
    }
}
