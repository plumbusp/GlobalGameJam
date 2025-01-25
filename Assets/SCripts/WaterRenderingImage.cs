using UnityEngine;
using UnityEngine.UI;
public class WaterRenderingImage : MonoBehaviour
{
    public RenderTexture rntx;
    private void Start()
    {
        float aspectRatio = Camera.main.aspect;
        Rect thisRect = GetComponent<RectTransform>().rect;
        thisRect.size = new Vector2(thisRect.size.x * aspectRatio, thisRect.size.y * aspectRatio);
        Debug.Log(Camera.main.aspect + " thisRect.size" + thisRect.size);
    }
}
