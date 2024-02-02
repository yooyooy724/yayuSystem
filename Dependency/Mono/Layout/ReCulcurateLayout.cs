using UnityEngine;
using R3;
using UnityEngine.UI;
using System;

public class ReCulcurateLayout : MonoBehaviour
{
    [SerializeField] int DueCount = 1;
    IDisposable disposable1, disposable2;

    public void ResizeLayoutAfterFixedFrame(int dueCount)
    {
        var rect = GetComponent<RectTransform>();
        disposable1 = Observable.TimerFrame(dueCount).Subscribe(x =>
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(rect);
            //Debug.Log("rererererererererererere");
        });
    }

    public void ResizeLayoutAfterFixedFrame()
    {
        var rect = GetComponent<RectTransform>();
        disposable2 = Observable.TimerFrame(DueCount).Subscribe(x =>
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(rect);
            //Debug.Log("rererererererererererere");
        });
    }

    private void OnDestroy()
    {
        disposable1?.Dispose();
        disposable2?.Dispose();
    }
}
