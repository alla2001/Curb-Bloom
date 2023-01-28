using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : SingletonMonoBehaviour<T>
{
    private static T _instance;

    public static T instance
    { get { return _instance ?? (!isApplicationQuitting ? new GameObject("_" + typeof(T)).AddComponent<T>() : null); } }

    public static T CreateInstance()
    { return instance; }

    public static bool hasInstance
    { get { return _instance != null; } }

    public static bool isApplicationQuitting { get; protected set; }

    protected virtual void Awake()
    {
        if (_instance != null)
        {
            //Debug.LogError(name + ".Awake() error: already initialised as " + _instance.name);
            Destroy(gameObject);
            return;
        }

        _instance = (T)this;
        Initialise();
    }

    protected virtual void Initialise()
    { }

    protected virtual void OnApplicationQuit()
    {
        isApplicationQuitting = true;
    }

    protected virtual void OnDestroy()
    {
        if (_instance == this) _instance = null;
    }
}