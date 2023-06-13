using UnityEngine;

public class TestScript : MonoBehaviour
{
    [SerializeField] WorldGenerator worldGenerator;
    // Start is called before the first frame update
    void Start()
    {
        worldGenerator.GenerateTerrain(5, 5);
        foreach (Grid g in worldGenerator.grids)
        {
            g.ForDebugging();
        }
    }
}
