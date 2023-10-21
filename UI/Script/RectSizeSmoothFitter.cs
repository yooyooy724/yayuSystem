using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UniRx;

public class RectSizeSmoothFitter : UIBehaviour
{
    RectTransform _rect;
    RectTransform rect 
    { 
        get {
            if (_rect == null) _rect = GetComponent<RectTransform>();
            return _rect;
        }
    }

    [SerializeField] bool adjustWidth = true, adjustHeight = false;
    [SerializeField] float easingRateWidth = 0.01f, easingRateHeight = 0.01f;
    [SerializeField] [Tooltip("•Ï‰»—Ê‚ª [Sleeping Threshold] ‚ð’´‚¦‚½‚Æ‚«‚É’l‚ðXV")] float sleepingThreshold = 0.01f;
    Easing easingX = new Easing(), easingY = new Easing();
    float next_w, next_h, w, h;

    protected override void Start()
    {
        Observable.IntervalFrame(1).Take(5).Subscribe(_ =>
        {
            easingX.SetValue(adjustWidth ?
                LayoutUtility.GetPreferredWidth(rect) :
                rect.sizeDelta.x);
            easingY.SetValue(adjustHeight ?
                LayoutUtility.GetPreferredHeight(rect) :
                rect.sizeDelta.y);
        }).AddTo(this);

        Observable.EveryUpdate().Subscribe(_ => UpdateSize()).AddTo(this);
    }

    void UpdateSize()
    {
        if (!adjustWidth && !adjustHeight) return;
        next_w = adjustWidth ?
            easingX.GetValue(LayoutUtility.GetPreferredWidth(rect), easingRateWidth) :
            rect.sizeDelta.x;
        next_h = adjustHeight ?
            easingY.GetValue(LayoutUtility.GetPreferredHeight(rect), easingRateHeight) :
            rect.sizeDelta.y;

        float diff = Mathf.Abs(next_w - w) + Mathf.Abs(next_h - h); //ŽG‚¢‚¯‚Ç‚Ü‚Æ‚ß‚Ä
        if (diff > sleepingThreshold)
        {
            w = next_w;
            h = next_h;
            rect.sizeDelta = new Vector2(w, h);
        }
    }

    class Easing : MonoBehaviour
    {
        float currentValue;
        public void SetValue(float value) 
        { 
            this.currentValue = value;
            //Debug.Log(value);
        }
        public float GetValue(float goalValue, float easingRate)
        {
            var d = goalValue - currentValue;
            currentValue += d * easingRate;
            return currentValue;
        }
    }
}
