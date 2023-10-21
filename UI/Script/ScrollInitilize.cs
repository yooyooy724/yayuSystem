using UnityEngine;
using UnityEngine.UI;

public class ScrollInitilize : MonoBehaviour
{
    [SerializeField] bool isInstantShow;
    float initialRate = 1;
    float errorSize = 0.00001f;
    int threshold = 10;

    ScrollRect scroll;
    bool isInited;
    bool isStarted = false;
    int count;
    void Start()
    {
        isInited = false;
        isStarted = true;
        scroll = gameObject.GetComponent<ScrollRect>();
    }
    void Update()
    {
        if (!isInstantShow || isInited) return;
        float pos = scroll.verticalNormalizedPosition;
        if (initialRate - errorSize <= pos && pos <= initialRate + errorSize) 
        {
            count++;
            scroll.verticalNormalizedPosition = initialRate;
            if (count > threshold)
            {
                Debug.Log("init scroll");
                isInited = true;
            }
        }
        scroll.verticalNormalizedPosition = initialRate;
    }

    public void InitScroll()
    {
        if (!isStarted || isInited) return;
        Debug.Log("init scroll");
        scroll.verticalNormalizedPosition = initialRate;
        isInited = true;
    }
}
