using UnityEngine;

public abstract class SingletonBehavior<S> : MonoBehaviour where S : Component
{
    public static S instance;

    protected virtual void Awake()
    {
        if (instance != null & instance != this)
        {
            Debug.LogWarning("Duplication for singleton found: " + instance.name + ". Destroying this object: " + gameObject.name);
            Destroy(gameObject);
        }

        else
        {
            instance = this as S;
        }
    }

    protected bool IsStray()
    {
        return this != instance;
    }
}
