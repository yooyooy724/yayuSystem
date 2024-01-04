using UnityEngine;
using UniRx;
using UnityEngine.UI;

public class ReCulcurateLayout : MonoBehaviour
{
    [SerializeField] int DueCount = 1;

    public void ResizeLayoutAfterFixedFrame(int dueCount)
    {
        var rect = GetComponent<RectTransform>();
        Observable.TimerFrame(dueTimeFrameCount: dueCount).Subscribe(x =>
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(rect);
            //Debug.Log("rererererererererererere");
        }).AddTo(this);
    }

    public void ResizeLayoutAfterFixedFrame()
    {
        var rect = GetComponent<RectTransform>();
        Observable.TimerFrame(dueTimeFrameCount: DueCount).Subscribe(x =>
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(rect);
            //Debug.Log("rererererererererererere");
        }).AddTo(this);
    }
}
