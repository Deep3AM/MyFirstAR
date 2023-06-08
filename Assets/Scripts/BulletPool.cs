using UnityEngine;

public class BulletPool : BasePool<Bullet>
{
    protected override void OnRelease(Bullet poolingObject)
    {
        var temp = poolingObject.GetComponent<Rigidbody>();
        temp.isKinematic = true;
        temp.detectCollisions = false;
        temp.transform.rotation = new Quaternion(0, 0, 0, 0);
        base.OnRelease(poolingObject);
    }
    protected override void OnGet(Bullet poolingObject)
    {
        var temp = poolingObject.GetComponent<Rigidbody>();
        temp.isKinematic = false;
        temp.detectCollisions = true;
        poolingObject.transform.localPosition = new Vector3(0, 2, 0.25f);
        base.OnGet(poolingObject);
    }
}
