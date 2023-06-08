using Oculus.Interaction;
using UnityEngine;

public class RayHitObject : MonoBehaviour
{
    private RayInteractor interactor;
    public GameObject redCube;
    public GameObject greenCube;
    public GameObject spawnCube;
    public Player player;
    private bool haveFront = false;
    private bool haveRear = false;
    private bool haveRight = false;
    private bool haveLeft = false;
    private bool isWin = false;
    public GameObject winUIObject;
    public AudioClip attachAudio;
    AudioSource audioSource;
    private void Awake()
    {
        interactor = GetComponent<RayInteractor>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(interactor.Ray, out hit, 5f, LayerMask.GetMask("Wall")))
        {
            if (hit.collider.CompareTag("Front"))
            {
                if (!haveFront && player.attachableNum > 0)
                {
                    if (!greenCube.activeInHierarchy)
                    {
                        greenCube.SetActive(true);
                    }
                    greenCube.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z - 0.25f);
                    if (greenCube.transform.localRotation != Quaternion.Euler(new Vector3(0, 0, 0)))
                        greenCube.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    if (OVRInput.Get(OVRInput.Button.One))
                    {
                        player.attachableNum--;
                        GameObject go = Instantiate(spawnCube);
                        go.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z - 0.25f);
                        go.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                        haveFront = true;
                        audioSource.PlayOneShot(attachAudio);
                        CheckWin();
                    }
                }
                else
                {
                    if (!redCube.activeInHierarchy)
                    {
                        Debug.Log("Front");
                        redCube.SetActive(true);
                    }
                    redCube.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z - 0.25f);
                    if (redCube.transform.localRotation != Quaternion.Euler(new Vector3(0, 0, 0)))
                        redCube.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                }
            }
            else if (hit.collider.CompareTag("Rear"))
            {
                if (!haveRear && player.attachableNum > 0)
                {
                    if (!greenCube.activeInHierarchy)
                    {
                        greenCube.SetActive(true);
                    }
                    greenCube.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z + 0.25f);
                    if (greenCube.transform.localRotation != Quaternion.Euler(new Vector3(0, 180, 0)))
                        greenCube.transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
                    if (OVRInput.Get(OVRInput.Button.One))
                    {
                        player.attachableNum--;
                        GameObject go = Instantiate(spawnCube);
                        go.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z + 0.25f);
                        go.transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
                        haveRear = true;
                        audioSource.PlayOneShot(attachAudio);
                        CheckWin();

                    }
                }
                else
                {
                    if (!redCube.activeInHierarchy)
                    {
                        Debug.Log("Rear");
                        redCube.SetActive(true);
                    }
                    redCube.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z + 0.25f);
                    if (redCube.transform.localRotation != Quaternion.Euler(new Vector3(0, 180, 0)))
                        redCube.transform.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
                }
            }

            else if (hit.collider.CompareTag("Right"))
            {
                if (!haveRight && player.attachableNum > 0)
                {
                    if (!greenCube.activeInHierarchy)
                    {
                        greenCube.SetActive(true);
                    }
                    greenCube.transform.position = new Vector3(hit.point.x - 0.25f, hit.point.y, hit.point.z);
                    if (greenCube.transform.localRotation != Quaternion.Euler(new Vector3(0, 90, 0)))
                        greenCube.transform.localRotation = Quaternion.Euler(new Vector3(0, 90, 0));
                    if (OVRInput.Get(OVRInput.Button.One))
                    {
                        player.attachableNum--;
                        GameObject go = Instantiate(spawnCube);
                        go.transform.position = new Vector3(hit.point.x - 0.25f, hit.point.y, hit.point.z);
                        go.transform.localRotation = Quaternion.Euler(new Vector3(0, 90, 0));
                        haveRight = true;
                        audioSource.PlayOneShot(attachAudio);
                        CheckWin();

                    }
                }
                else
                {
                    if (!redCube.activeInHierarchy)
                    {
                        Debug.Log("Right");
                        redCube.SetActive(true);
                    }
                    redCube.transform.position = new Vector3(hit.point.x - 0.25f, hit.point.y, hit.point.z);
                    if (redCube.transform.localRotation != Quaternion.Euler(new Vector3(0, 90, 0)))
                        redCube.transform.localRotation = Quaternion.Euler(new Vector3(0, 90, 0));
                }

            }
            else if (hit.collider.CompareTag("Left"))
            {
                if (!haveLeft && player.attachableNum > 0)
                {
                    if (!greenCube.activeInHierarchy)
                    {
                        greenCube.SetActive(true);
                    }
                    greenCube.transform.position = new Vector3(hit.point.x + 0.25f, hit.point.y, hit.point.z);
                    if (greenCube.transform.localRotation != Quaternion.Euler(new Vector3(0, 270, 0)))
                        greenCube.transform.localRotation = Quaternion.Euler(new Vector3(0, 270, 0));


                    if (OVRInput.Get(OVRInput.Button.One))
                    {
                        player.attachableNum--;
                        GameObject go = Instantiate(spawnCube);
                        go.transform.position = new Vector3(hit.point.x + 0.25f, hit.point.y, hit.point.z);
                        go.transform.localRotation = Quaternion.Euler(new Vector3(0, 270, 0));

                        haveLeft = true;
                        audioSource.PlayOneShot(attachAudio);
                        CheckWin();
                    }
                }
                else
                {
                    if (!redCube.activeInHierarchy)
                    {
                        Debug.Log("Left");
                        redCube.SetActive(true);
                    }
                    redCube.transform.position = new Vector3(hit.point.x + 0.25f, hit.point.y, hit.point.z);
                    if (redCube.transform.localRotation != Quaternion.Euler(new Vector3(0, 270, 0)))
                        redCube.transform.localRotation = Quaternion.Euler(new Vector3(0, 270, 0));
                }

            }
            else
            {
                if (redCube.activeInHierarchy)
                {
                    redCube.SetActive(false);
                }
                if (greenCube.activeInHierarchy)
                {
                    greenCube.SetActive(false);
                }
            }
        }
    }
    private void CheckWin()
    {
        if (haveFront && haveRear && haveRight & haveLeft && !isWin)
        {
            isWin = true;
            winUIObject.SetActive(true);

        }
    }
}
