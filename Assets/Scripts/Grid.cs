using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public enum GridCategory
{
    NONE = 0,
    HIDE = 1,
    ATTACHABLE = 2
}
public class Grid : MonoBehaviour
{
    private bool isNotDigged = true;
    private bool isOnMouse = false;
    public bool isNotSelectable;
    public Material whiteMaterial;
    public Material redMaterial;
    public HoleColliderDetector holeColliderDetector;
    private Player player;
    private int blockHP = 16;
    public bool isHideable = true;
    public GridCategory gridCategory;
    private IEnumerator diggingCo = null;
    public List<GameObject> hpBlocks;
    public GameObject hideBox;
    public AudioClip attachAudio;
    private AudioSource audioSource;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {

    }

    private void OnMouseEnter()
    {
        OnHover();
    }
    private void OnMouseExit()
    {
        OnUnHover();
    }
    private void OnMouseUp()
    {
        OnSelected();
    }
    private void OnMouseDown()
    {

    }
    public void OnHover()
    {
        if (isNotSelectable) return;
        if (!isNotDigged) return;
        GetComponent<Renderer>().material = redMaterial;
    }
    public void OnUnHover()
    {
        if (isNotSelectable) return;
        if (!isNotDigged) return;
        isOnMouse = false;
        GetComponent<Renderer>().material = whiteMaterial;
        if (diggingCo != null)
        {
            StopCoroutine(diggingCo);
            audioSource.Stop();
            diggingCo = null;
            blockHP = 16;
            foreach (GameObject g in hpBlocks)
            {
                g.SetActive(false);
            }
        }
    }
    public void OnSelected()
    {
        if (isNotSelectable) return;
        if (isNotDigged)
        {
            if (diggingCo == null)
            {
                diggingCo = DiggingCoroutine();
                StartCoroutine(diggingCo);
            }
        }
    }
    public void OnUnselected()
    {
        if (isNotSelectable) return;
        if (!isNotDigged) return;
        if (diggingCo != null)
        {
            StopCoroutine(diggingCo);
            audioSource.Stop();
            diggingCo = null;
            blockHP = 16;
            foreach (GameObject g in hpBlocks)
            {
                g.SetActive(false);
            }
        }
    }
    IEnumerator DiggingCoroutine()
    {
        audioSource.Play();
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            Debug.Log(blockHP);
            blockHP--;
            hpBlocks[15 - blockHP].SetActive(true);
            if (blockHP <= 0)
            {
                audioSource.Stop();
                isNotDigged = false;
                GetComponent<Renderer>().enabled = false;
                if (gridCategory == GridCategory.HIDE)
                {
                    isHideable = true;
                    hideBox.SetActive(true);
                }
                else if (gridCategory == GridCategory.ATTACHABLE)
                {
                    player.attachableNum++;
                    audioSource.PlayOneShot(attachAudio);
                }
                if (diggingCo != null)
                {
                    StopCoroutine(diggingCo);
                    audioSource.Stop();
                    diggingCo = null;
                }
            }
        }
    }

    public IEnumerator HideCoroutine()
    {
        isHideable = false;
        yield return new WaitForSeconds(30);
        isHideable = true;
    }
}
