using UnityEngine;
using UnityEngine.UI;

public class ChildCubeRectFitter : MonoBehaviour
{
    private RectTransform rectTransform;
    private RectTransform parentRectTransform;
    private float lastParentSize;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        parentRectTransform = transform.parent.GetComponent<RectTransform>();
        lastParentSize = Mathf.Min(parentRectTransform.rect.width, parentRectTransform.rect.height);
    }

    void Update()
    {
        float parentSize = Mathf.Min(parentRectTransform.rect.width, parentRectTransform.rect.height);
        if (parentSize != lastParentSize)
        {
            float scaleFactor = parentSize / lastParentSize;
            rectTransform.sizeDelta *= scaleFactor;
            lastParentSize = parentSize;
        }
    }
}
