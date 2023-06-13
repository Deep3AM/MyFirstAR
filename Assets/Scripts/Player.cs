using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform centerEyeTransform;
    public bool canHide = false;
    private bool isHiding;
    public GameObject black;
    public int attachableNum = 0;
    public Enemy enemy;

    private void Update()
    {
        transform.position = new Vector3(centerEyeTransform.position.x, 0, centerEyeTransform.position.z);
    }

    public void Hide()
    {
        black.SetActive(true);
    }

    public void NonHide()
    {
        black.SetActive(false);
    }
}
