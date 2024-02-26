using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    private void Awake()
    {
        if (instance != null)
            Debug.LogError($"Multiple {instance} is running");
        instance = GetComponent<T>();
    }

    public static T Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<T>();
            return instance;
        }
    }
}
