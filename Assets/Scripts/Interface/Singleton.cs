using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    static public T instance { get; private set; }

    public void Awake()
    {
        if (instance != null && instance != gameObject)
        {
            Destroy(gameObject);
        }
        
        instance = this as T;
        DontDestroyOnLoad(gameObject);
        
    }
}
