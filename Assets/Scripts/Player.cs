using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform centerEyeTransform;
    public bool canHide = false;
    private bool isHiding;
    public GameObject black;
    public int attachableNum = 0;
    public Grid currentGrid;
    public Enemy enemy;

    private void Update()
    {
        transform.position = new Vector3(centerEyeTransform.position.x, 0, centerEyeTransform.position.z);

        if (OVRInput.Get(OVRInput.Button.Two))
        {
            black.SetActive(true);
        }
        else if (!OVRInput.GetUp(OVRInput.Button.Two))
        {
            black.SetActive(false);
        }


        //just debugging
        if (Input.GetKeyDown(KeyCode.Space))
        {
            black.SetActive(true);
            enemy.StopEnemyCoroutine(false);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            black.SetActive(false);
        }


    }
}
