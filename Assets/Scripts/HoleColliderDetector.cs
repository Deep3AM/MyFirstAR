using UnityEngine;

public class HoleColliderDetector : MonoBehaviour
{
    public bool isInArea = false;
    public Grid thisGrid;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().currentGrid = thisGrid;
            other.gameObject.GetComponent<Player>().canHide = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().canHide = false;
        }
    }
}
