using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    public GameObject block;
    public GameObject environment;
    public Transform player;
    public List<Grid> grids;
    public Material debuggingKeyMaterial;
    public Material debuggingHideMaterial;
    private bool isWorldGenerated = false;

    public void GenerateTerrain(float maxFloat, float width)
    {
        int sizeZ = Mathf.RoundToInt(maxFloat) - 1;
        int sizeX = Mathf.RoundToInt(width) - 1;
        sizeZ *= 2;
        sizeX *= 2;
        for (int x = -sizeX / 2; x < sizeX / 2 + 1; x++)
        {
            for (int z = -sizeZ / 2; z < sizeZ / 2 + 1; z++)
            {
                GameObject tempBlock1 = Instantiate(block, new Vector3(x, -1, z), Quaternion.identity);
                tempBlock1.transform.SetParent(environment.transform);
                grids.Add(tempBlock1.GetComponent<Grid>());
                GameObject tempBlock2 = Instantiate(block, new Vector3(x + 0.5f, -1, z), Quaternion.identity);
                tempBlock2.transform.SetParent(environment.transform);
                grids.Add(tempBlock2.GetComponent<Grid>());
                GameObject tempBlock3 = Instantiate(block, new Vector3(x + 0.5f, -1, z + 0.5f), Quaternion.identity);
                tempBlock3.transform.SetParent(environment.transform);
                grids.Add(tempBlock3.GetComponent<Grid>());
                GameObject tempBlock4 = Instantiate(block, new Vector3(x, -1, z + 0.5f), Quaternion.identity);
                tempBlock4.transform.SetParent(environment.transform);
                grids.Add(tempBlock4.GetComponent<Grid>());
            }
        }
        List<int> keyRandNums = new List<int>();
        for (int i = 0; i < 4; i++)
        {
            int n = Random.Range(0, grids.Count);
            while (keyRandNums.Contains(n))
            {
                n = Random.Range(0, grids.Count);
            }
            keyRandNums.Add(n);
        }
        List<int> hideRandNums = new List<int>();
        for (int i = 0; i < grids.Count / 20; i++)
        {
            int n = Random.Range(0, grids.Count);
            while (hideRandNums.Contains(n))
            {
                n = Random.Range(0, grids.Count);
            }
            hideRandNums.Add(n);
        }
        for (int i = 0; i < grids.Count; i++)
        {
            if (keyRandNums.Contains(i))
            {
                grids[i].gridCategory = GridCategory.ATTACHABLE;
                grids[i].GetComponent<MeshRenderer>().material = debuggingKeyMaterial;

            }
            else if (hideRandNums.Contains(i))
            {
                grids[i].gridCategory = GridCategory.HIDE;
                grids[i].GetComponent<MeshRenderer>().material = debuggingHideMaterial;
            }
            else
            {
                grids[i].gridCategory = GridCategory.NONE;
            }
        }
    }
}
