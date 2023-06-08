using UnityEngine.Pool;

public interface IBasePoolingObject<T> where T : class
{
    void SetPool(IObjectPool<T> objectPool);

    void Release();
}
