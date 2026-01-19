using UnityEngine;

public class CleaningSponge : ToolAC, IInteractable
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float moveSpeed = 1f;
    public float torqueStrength = .5f;

    public bool beingDragged = false;
    private Vector3 positionToMove = Vector3.zero;

    private bool wet = false;
    private float wetRate = 20f;
    public float wetness = 30f;

    private float dryRate = 2f;

    public Material carMAT;
    public RenderTexture paintMask;
    public Texture brushMask;
    public float brushSize = 0.1f;
    public float completePercent = 80f;
    public string carTag;

    private SoundObject myAudioSource;

    private void Awake()
    {
        TryGetComponent(out myAudioSource);
    }

    private void Start()
    {
        ResetPaintMask();
        InvokeRepeating("CheckPaintPercent", 1f, 1f);
    }

    private void Update()
    {
        if (beingDragged)
        {
            positionToMove = PlayerGeneral.instance.spot.position - transform.position;
        }

        if (wetness > 0)
        {
            wetness -= dryRate * Time.deltaTime;
            wet = true;
        }

        if (wetness <= 0)
        {
            wet = false;
        }
    }

    private void FixedUpdate()
    {
        if (beingDragged)
        {
            rb.AddForce(positionToMove * moveSpeed, ForceMode.Force);

            Vector3 desiredUp = Vector3.up;
            Vector3 torqueVector = Vector3.Cross(transform.up, desiredUp) * torqueStrength;

            rb.AddTorque(torqueVector);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out WaterBucket bucket))
        {
            if (bucket.waterLeft <= 0 && CheckPaintPercent() < 100 && MissionManager.Instance.inMission && wetness <= 0)
            {
                MissionManager.Instance.currenMission.FailMission();
            }
            else
            {
                if (wetness < 100)
                {
                    wetness += wetRate;
                    bucket.waterLeft -= wetRate;
                    myAudioSource.CancelCurrentSound();
                    myAudioSource.PlaySoundIndex(2, 0.5f);
                }
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!wet)
        {
            myAudioSource.PlaySoundIndex(1, 1f);
            return;
        }

        if (collision.collider.CompareTag(carTag))
        {
            //Esto es para que si o si tengamos meshcollider
            MeshCollider meshCollider = collision.collider as MeshCollider;
            if (meshCollider == null || meshCollider.sharedMesh == null) return;

            foreach (ContactPoint contact in collision.contacts)
            {
                Vector2 uv = GetUV(meshCollider, contact.point);
                if (uv != Vector2.zero)
                {
                    myAudioSource.PlaySoundIndex(0, 0.3f);
                    Paint(uv);
                }
            }
        }
    }

    private Vector2 GetUV(MeshCollider meshCollider, Vector3 worldPoint)
    {
        Ray ray = new Ray(worldPoint + Vector3.up * 0.01f, Vector3.down);
        if (meshCollider.Raycast(ray, out RaycastHit hit, 0.1f))
        {
            return hit.textureCoord;
        }
        return Vector2.zero;
    }

    private void Paint(Vector2 uv)
    {
        RenderTexture.active = paintMask;

        GL.PushMatrix();
        GL.LoadPixelMatrix(0, paintMask.width, paintMask.height, 0);

        int size = (int)(paintMask.width * brushSize);
        int x = (int)(uv.x * paintMask.width);
        int y = (int)((1 - uv.y) * paintMask.height);

        Rect brushRect = new Rect(x - size / 2, y - size / 2, size, size);
        Graphics.DrawTexture(brushRect, brushMask);

        GL.PopMatrix();
        RenderTexture.active = null;
    }

    private void ResetPaintMask()
    {
        //si se quiere que el carro vuelva a estar sucio, aqui
        RenderTexture.active = paintMask;
        //GL.Clear(true, true, Color.black);
        GL.Clear(true, true, new Color(0, 0, 0, 0));
        RenderTexture.active = null;

        carMAT.SetTexture("_Mask", paintMask);
    }

    public float CheckPaintPercent()
    {
        Texture2D paintMaskTex = new(paintMask.width, paintMask.height, TextureFormat.RGBA32, false);

        RenderTexture.active = paintMask;
        paintMaskTex.ReadPixels(new Rect(0, 0, paintMask.width, paintMask.height), 0, 0);
        paintMaskTex.Apply();
        RenderTexture.active = null;

        int paintedPixels = 0;
        Color[] pixelsColors = paintMaskTex.GetPixels();
        foreach (Color pixelColor in pixelsColors)
        {
            if (pixelColor.a > 0.5f)
                paintedPixels++;
        }

        float paintPercent = ((float)paintedPixels / pixelsColors.Length) * 100f;

        Debug.Log(paintPercent);
        if (paintPercent >= completePercent && MissionManager.Instance.inMission && MissionManager.Instance.index == 2)
        {
            Debug.Log("listo");
            RenderTexture.active = paintMask;
            GL.Clear(true, true, new Color(1, 1, 1, 1));
            RenderTexture.active = null;

            MissionManager.Instance.FinishMission();
        }

        return paintPercent;
    }

    public void Interact()
    {
        beingDragged = true;
        rb.useGravity = false;
    }

    public void StopInteracting()
    {
        beingDragged = false;
        rb.useGravity = true;
    }

    public override void Use()
    {
        throw new System.NotImplementedException();
    }
}
