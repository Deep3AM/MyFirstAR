using UnityEngine;

public class EyeTrackingUI : MonoBehaviour
{
    public Transform centerEyeTransform;
    public float distance = 1;
    private void Update()
    {
        transform.position = centerEyeTransform.position
             + new Vector3(centerEyeTransform.forward.x, 0, centerEyeTransform.forward.z).normalized * distance;
        transform.LookAt(new Vector3(centerEyeTransform.position.x, transform.position.y, centerEyeTransform.position.z));
        transform.forward *= -1;
    }
}
