using UnityEngine;
using System;

public abstract class SingletonMonoBehavior<T> : UnityEngine.MonoBehaviour where T : UnityEngine.MonoBehaviour
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                Type t = typeof(T);

                instance = (T)FindObjectOfType(t);
                if (instance == null)
                {
                    Debug.LogError(t + " Don't attach Gameobject");
                }
            }
            return instance;
        }
    }

    virtual protected void Awake()
    {
        // search attached other gamebject
        // Is attached , Destroy obj
        CheckInstance();

    }

    protected bool CheckInstance()
    {
        if(instance == null)
        {
            instance = this as T;
            return true;
        }else if(Instance == null)
        {
            return true;
        }
        Destroy(this);
        return false;

    }
}
