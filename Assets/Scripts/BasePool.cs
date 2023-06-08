using UnityEngine;
using UnityEngine.Pool;

public abstract class BasePool<T> : MonoBehaviour where T : MonoBehaviour, IBasePoolingObject<T>
{
    public IObjectPool<T> pool;
    [SerializeField] private T pObject;
    protected virtual void Awake()
    {
        pool = new ObjectPool<T>(
            Create,
            OnGet,
            OnRelease,
            OnDestroyForPool,
            defaultCapacity: 5,
            maxSize: 100
            );
    }

    protected virtual T Create()
    {
        T poolingObject = Instantiate(pObject);
        poolingObject.SetPool(pool);
        return poolingObject;
    }

    protected virtual void OnGet(T poolingObject)
    {
        poolingObject.gameObject.SetActive(true);
    }

    protected virtual void OnRelease(T poolingObject)
    {
        poolingObject.gameObject.SetActive(false);
        poolingObject.gameObject.transform.localPosition = Vector3.zero;
    }

    protected virtual void OnDestroyForPool(T poolingObject)
    {
        Destroy(poolingObject.gameObject);
    }

    public virtual void GetFromPool()
    {
        GameObject temp = pool.Get().gameObject;
    }
}
