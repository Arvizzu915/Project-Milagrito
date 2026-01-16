using UnityEngine;

public class CleaningSponge : MonoBehaviour
{
    public Material carMAT;
    public RenderTexture paintMask;
    public Texture brushMask;
    public float brushSize = 0.1f;
    public float completePercent = 80f;
    public string carTag;

    private void Start()
    {
        ResetPaintMask();
        InvokeRepeating("CheckPaintPercent", 1f, 1f);
    }

    private void OnCollisionStay(Collision collision)
    {
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

    private void CheckPaintPercent()
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
        if (paintPercent >= completePercent) 
        {
            Debug.Log("listo");
            RenderTexture.active = paintMask;
            GL.Clear(true, true, new Color(1, 1, 1, 1));
            RenderTexture.active = null;
        }
    }
}
