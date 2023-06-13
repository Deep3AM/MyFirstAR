using UnityEngine;

public class HoleColliderDetector : MonoBehaviour
{
    public Grid thisGrid;
    public bool isTrigger = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isTrigger = true;
            thisGrid.isHideable = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            thisGrid.isHideable = false;
        }
    }
}
