using System.Collections;
using UnityEngine;

public class WebCamRenderer : MonoBehaviour
{
    public RenderTexture webcamRenderTexture;
    WebCamTexture webCamTexture;
    public CustomNNSolver depthFromImage;
    void Start()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        for (int i = 0; i < devices.Length; i++)
        {
            Debug.Log(devices[i].name);
        }
        Debug.Log(devices[1]);
        webCamTexture = new WebCamTexture("OBS Virtual Camera", 1920, 1080, 60);
        webCamTexture.Play();
    }

    // Update is called once per frame
    void Update()
    {
        Graphics.Blit(webCamTexture, webcamRenderTexture);
    }

    IEnumerator InitWebCam()
    {
        yield return new WaitForSeconds(0.5f);
        depthFromImage.InputTexture = webcamRenderTexture;
    }
}
