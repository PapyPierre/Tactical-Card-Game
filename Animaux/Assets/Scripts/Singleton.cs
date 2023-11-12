using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    public static T instance;

    public virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
        }
        else
        {
            Debug.LogWarning($"Two instance of {instance.name} in the scene, gameObject have been destroyed");
            Destroy(gameObject);
        }
    }
}