using System.Collections.Generic;
using System.Linq;
using Unity.Barracuda;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.Rendering;
using static Unity.Barracuda.BarracudaTextureUtils;


public class CustomNNSolver : MonoBehaviour
{
    private bool isFront = false;
    public float imageScale;
    public GameObject frontPlane;
    public GameObject rearPlane;
    public GameObject rightPlane;
    public GameObject leftPlane;
    public float wallSpawnMultiplier;
    public float sideWallSpawnMultiplier;
    public UnityEvent<float, float> OnPlaneSpawned;
    [Header("Object References")]
    public NNModel neuralNetworkModel;
    public Texture inputTexture;

    public Texture InputTexture
    {
        get => inputTexture;
        set => inputTexture = value;
    }

    private Model _model;
    private IWorker _engine;
    private RenderTexture _input, _output;
    private int _width, _height;
    int tryInt = 0;
    Texture2D _t2d;
    private void Start()
    {
        InitializeNetwork();
        AllocateObjects();
    }

    private void Update()
    {
        if (inputTexture == null)
            return;
        // Fast resize
        Graphics.Blit(inputTexture, _input);

        if (neuralNetworkModel == null)
            return;

        RunModel(_input);

        if (!isFront)
        {
            tryInt++;
            _t2d = new Texture2D(_output.width, _output.height, _output.graphicsFormat, TextureCreationFlags.None);
            RenderTexture.active = _output;
            _t2d.ReadPixels(new Rect(0, 0, _output.width, _output.height), 0, 0);
            _t2d.Apply();
            List<float> tempFloat = _t2d.GetRawTextureData<float>().ToList();
            Debug.Log(tempFloat.Max());
            if (tryInt == 10)
            {
                isFront = true;
                float width = tempFloat.Max() * Mathf.Tan(Mathf.Deg2Rad * 40) * wallSpawnMultiplier * sideWallSpawnMultiplier;
                Debug.Log(_width);
                frontPlane.SetActive(true);
                rearPlane.SetActive(true);
                rightPlane.SetActive(true);
                leftPlane.SetActive(true);
                frontPlane.transform.position = new Vector3(0, 0, tempFloat.Max() * wallSpawnMultiplier);
                rearPlane.transform.position = new Vector3(0, 0, -tempFloat.Max() * wallSpawnMultiplier);
                rightPlane.transform.position = new Vector3(width, 0, 0);
                leftPlane.transform.position = new Vector3(-width, 0, 0);
                OnPlaneSpawned.Invoke(tempFloat.Max() * wallSpawnMultiplier, width);
                gameObject.SetActive(false);
            }

        }
    }

    private void OnDestroy() => DeallocateObjects();

    /// <summary>
    /// Loads the <see cref="NNModel"/> asset in memory and creates a Barracuda <see cref="IWorker"/>
    /// </summary>
    private void InitializeNetwork()
    {
        if (neuralNetworkModel == null)
            return;

        // Load the model to memory
        _model = ModelLoader.Load(neuralNetworkModel);

        // Create a worker
        _engine = WorkerFactory.CreateWorker(_model, WorkerFactory.Device.GPU);

        // Get Tensor dimensionality ( texture width/height )
        // In Barracuda 1.0.4 the width and height are in channels 1 & 2.
        // In later versions in channels 5 & 6
#if _CHANNEL_SWAP
            _width = _model.inputs[0].shape[5];
            _height = _model.inputs[0].shape[6];
#else
        _width = _model.inputs[0].shape[5];
        _height = _model.inputs[0].shape[6];
#endif
    }

    /// <summary>
    /// Allocates the necessary <see cref="RenderTexture"/> objects.
    /// </summary>
    private void AllocateObjects()
    {
        if (inputTexture == null)
            return;

        // Check for accidental memory leaks
        if (_input != null) _input.Release();
        if (_output != null) _output.Release();

        // Declare texture resources
        _input = new RenderTexture(_width, _height, 0, RenderTextureFormat.ARGB32);
        _output = new RenderTexture(_width, _height, 0, RenderTextureFormat.RFloat);

        // Initialize memory
        _input.Create();
        _output.Create();
    }

    /// <summary>
    /// Releases all unmanaged objects
    /// </summary>
    private void DeallocateObjects()
    {
        _engine?.Dispose();
        _engine = null;

        if (_input != null) _input.Release();
        _input = null;

        if (_output != null) _output.Release();
        _output = null;

    }

    /// <summary>
    /// Performs Inference on the Neural Network Model
    /// </summary>
    /// <param name="source"></param>
    private void RunModel(Texture source)
    {
        using (var tensor = new Tensor(source, 3))
        {
            _engine.Execute(tensor);
        }

        // In Barracuda 1.0.4 the output of MiDaS can be passed  directly to a texture as it is shaped correctly.
        // In later versions we have to reshape the tensor. Don't ask why...
#if _CHANNEL_SWAP
            var to = _engine.PeekOutput().Reshape(new TensorShape(1, _width, _height, 1));
#else
        var to = _engine.PeekOutput();
#endif

        TensorToRenderTexture(to, _output, fromChannel: 0);
        to?.Dispose();
    }

}
