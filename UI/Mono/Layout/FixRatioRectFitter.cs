using UnityEngine;

public class FixRatioRectFitter : MonoBehaviour
{
    private RectTransform rectTransform;
    private RectTransform parentRectTransform;
    private Vector2 lastParentSize;
    public bool fitHeight = true;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        parentRectTransform = rectTransform.parent.GetComponent<RectTransform>();
        lastParentSize = parentRectTransform.rect.size;
        AdjustSizeKeepingAspectRatio();
    }

    void Update()
    {
        if (lastParentSize != parentRectTransform.rect.size)
        {
            AdjustSizeKeepingAspectRatio();
            lastParentSize = parentRectTransform.rect.size;
        }
    }

    void AdjustSizeKeepingAspectRatio()
    {
        float aspectRatio = rectTransform.sizeDelta.x / rectTransform.sizeDelta.y;
        float newHeight = 0;
        float newWidth = 0;

        if (fitHeight)
        {
            newHeight = parentRectTransform.rect.height;
            newWidth = newHeight * aspectRatio;
        }
        else
        {
            newWidth = parentRectTransform.rect.width;
            newHeight = newWidth * aspectRatio;
        }

        rectTransform.sizeDelta = new Vector2(newWidth, newHeight);
    }
}