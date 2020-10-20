using UnityEngine;
using System.Collections;

public class SingletonMonoBehaviour<T> : MonoBehaviour
                               where T : MonoBehaviour
{
    protected static T _instance = null;
    public static T instance
    {
        get { return _instance; }
    }

    protected virtual void OnEnable()
    {
        if (_instance != null)
        {
            Debug.LogErrorFormat("{0}. Only 1 instance of singleton is allowed!", this.GetType().Name);
        }
        _instance = this as T;
    }

    protected virtual void OnDisable()
    {
        if (_instance == null)
        {
            Debug.LogErrorFormat("{0}.  Instance should not be null!", this.GetType().Name);
        }
        _instance = null;
    }
}
