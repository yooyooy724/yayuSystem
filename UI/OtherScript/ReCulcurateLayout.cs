using UnityEngine;
using UniRx;
using UnityEngine.UI;

public class ReCulcurateLayout : MonoBehaviour
{
    public void ResizeLayoutAfterFixedFrame(int dueCount)
    {
        var rect = GetComponent<RectTransform>();
        Observable.TimerFrame(dueTimeFrameCount: dueCount).Subscribe(x =>
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(rect);
            //Debug.Log("rererererererererererere");
        }).AddTo(this);
    }
}
