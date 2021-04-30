using UnityEngine;

public class PoolFactory : MonoBehaviour
{
    private int defaultPoolSize = 5;
    public int DefaultPoolSize
    {
        get => defaultPoolSize;
        set => defaultPoolSize = value;
    }
    
    public Pool<T> CreatePool<T>(GameObject referenceGameObject) where T : Component
    {
        return new Pool<T>(referenceGameObject, defaultPoolSize);
    }
}