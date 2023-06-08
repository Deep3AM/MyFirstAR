using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour, IBasePoolingObject<Bullet>
{
    private IObjectPool<Bullet> bulletPool;
    private int damage = 10;
    public int Damage { get { return damage; } }

    public void SetPool(IObjectPool<Bullet> _pool)
    {
        bulletPool = _pool;
    }

    public void Release()
    {
        if (this.isActiveAndEnabled)
        {
            bulletPool.Release(this);
        }
    }
}
