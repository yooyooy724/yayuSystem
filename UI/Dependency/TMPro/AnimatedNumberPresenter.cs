//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System;
//using IdleLibrary;
//using TMPro;

//public class AnimatedNumberPresenter : MonoBehaviour
//{
//    // yana edit -----------------------------------------------
//    Func<double, string> doubleToString;
//    public void SetNumberComverter(Func<double, string> doubleToString)
//    {
//        this.doubleToString = doubleToString;
//    }
//    string DoubleToString(double number)
//    {
//        if (doubleToString == null)
//            return number.ToString();
//        else
//            return doubleToString(number);
//    }
//    // ---------------------------------------------------------

//    double value = -9999;
//    TextMeshProUGUI textComp;

//    //normal settings
//    public double initialValue = 0;


//    [Header("------------- UI Counter normal animation -------------")]
//    [Space]
//    public bool takeFromTextComp = true;
//    public float normalFontSize = 50;
//    public Color normalColor = Color.white;
//    [Range(5f, 50f)] public float normalSpeed = 10f;
//    [Header("-- When increasing...")]
//    public Color normalColorUp = Color.green;
//    public float normalSizeFactorUp = 130f;
//    [Header("-- When decreasing...")]
//    public Color normalColorDown = Color.red;
//    public float normalSizeFactorDown = 300f;


//    public enum ZoneType { none, belowValue, aboveValue }
//    [Header("------------- UI Counter special animation -------------")]
//    [Space]
//    public ZoneType zoneType = ZoneType.none;
//    public float zoneThreshold = 10;
//    public float zoneFontSize = 50;
//    public Color zoneColor = Color.white;
//    public float zoneFontSizeFactor = 130f; //30% bigger
//    public Color zoneColorUp = Color.green;
//    public Color zoneColorDown = Color.red;
//    [Range(5f, 50f)] public float zoneSpeed = 10f;
//    bool inSpecialZone = false;


//    [Header("---------------- Floating Text Animation ----------------")]
//    public Color ftColorUp = Color.white;
//    public Color ftColorDown = Color.red;

//    [Space]
//    public bool moveTextUp = true;
//    public float distancePerSecond = 1f;

//    [Space]
//    public bool fadeColor = true;
//    [Range(90f, 100f)] public float durationFactor = 96f;

//    [Space]
//    public bool decreaseSize = false;
//    public float ftFontSize = 12;
//    [Range(90f, 120f)] public float decreaseFactor = 98f;

//    Transform floatingTextsContainer;


//    public enum whenCamShakeOptions { Never, EveryChange, EveryChangeUp, EveryChangeDown, Below, Above }
//    [Serializable]
//    public class configCamShake
//    {
//        public whenCamShakeOptions whenApplyShake = whenCamShakeOptions.Never;
//        public float BelowAboveWhat;
//        public float magnitude = 1f;
//    }
//    [Header("---------------- Camera shake ----------------")]
//    public List<configCamShake> configsCamShake = new List<configCamShake>();




//    void Start()
//    {
//        textComp = GetComponent<TextMeshProUGUI>();
//        SetValue(initialValue); //force the text to match the initial value, without animation

//        if (takeFromTextComp && textComp != null)
//        {
//            //here I read the "normal" settings from the text component
//            normalFontSize = textComp.fontSize;
//            normalColor = textComp.color;
//        }

//    }

//    void FixedUpdate()
//    {
//        if (textComp == null) return;
//        if (inSpecialZone)
//        {
//            textComp.fontSize = Mathf.Lerp(textComp.fontSize, zoneFontSize, zoneSpeed / 100);
//            textComp.color = Color.Lerp(textComp.color, zoneColor, zoneSpeed / 100);
//        }
//        else
//        {
//            textComp.fontSize = Mathf.Lerp(textComp.fontSize, normalFontSize, normalSpeed / 100);
//            textComp.color = Color.Lerp(textComp.color, normalColor, normalSpeed / 100);

//        }
//    }


//    public void SetValue(double newValue, Transform origin = null, string floatingText = "", bool withUIAnimation = true)
//    {
//        if (textComp == null) return;
//        if (value == newValue) return;
//        if (value == -9999) withUIAnimation = false;

//        CheckIfInZone(newValue);


//        // 1. ANIMATION OF UI COUNTER:
//        if (withUIAnimation)
//        {
//            AnimateUI(newValue);
//        }


//        // 2. FLOATING TEXT:
//        if (origin != null)
//        {
//            if (floatingText == "")
//            {
//                floatingText = ((newValue > value) ? "+" : "-") + Math.Abs(newValue - value).ToString();
//            }

//            if (floatingTextsContainer == null)
//                floatingTextsContainer = new GameObject("floating texts").transform;

//            GameObject instance = new GameObject("floating text " + origin.name);
//            instance.transform.position = origin.position;
//            instance.transform.rotation = Camera.main.transform.rotation; //for 3D or 2.5D
//            TextMeshPro text = instance.gameObject.AddComponent<TextMeshPro>();
//            text.text = floatingText;
//            text.fontSize = ftFontSize;
//            text.alignment = TextAlignmentOptions.MidlineGeoAligned;
//            text.color = (newValue < value) ? ftColorDown : ftColorUp;
//            text.font = textComp.font;

//            animCounter_floatAndDisappear script = instance.AddComponent<animCounter_floatAndDisappear>();
//            script.distancePerSecond = moveTextUp ? distancePerSecond : 0;
//            script.duration = fadeColor ? durationFactor : 100;
//            script.sizeDecrease = decreaseSize ? decreaseFactor : 100;
//            instance.transform.SetParent(floatingTextsContainer);
//        }


//        //3. CAMERA SHAKE
//        bool shakeNow = false;
//        float biggerMagnitude = 0;
//        foreach (configCamShake c in configsCamShake)
//        {
//            bool shakeInThisConfig = false;
//            switch (c.whenApplyShake)
//            {
//                case whenCamShakeOptions.EveryChange:
//                    shakeInThisConfig = true;
//                    break;

//                case whenCamShakeOptions.EveryChangeUp:
//                    shakeInThisConfig = (newValue > value);
//                    break;

//                case whenCamShakeOptions.EveryChangeDown:
//                    shakeInThisConfig = (newValue < value);
//                    break;

//                case whenCamShakeOptions.Below:
//                    shakeInThisConfig = (newValue < c.BelowAboveWhat);
//                    break;

//                case whenCamShakeOptions.Above:
//                    shakeInThisConfig = (newValue > c.BelowAboveWhat);
//                    break;
//            }
//            if (shakeInThisConfig)
//            {
//                shakeNow = true;
//                biggerMagnitude = Mathf.Max(biggerMagnitude, c.magnitude);
//            }
//        }

//        if (shakeNow) counter_CamShake.Shake?.Invoke(biggerMagnitude, true);


//        //4. REFRESH VALUES
//        value = newValue;
//        textComp.text = DoubleToString(value);
//    }

//    public double GetValue()
//    {
//        return value;
//    }

//    public void AddValue(int difference, Transform origin = null, string floatingText = "", bool withUIAnimation = true)
//    {
//        SetValue(value + difference, origin, floatingText, withUIAnimation);
//    }

//    public void forceAnimation(float simulatedValue)
//    {
//        AnimateUI(simulatedValue);
//    }

//    void CheckIfInZone(double newValue)
//    {
//        if (zoneType == ZoneType.none) inSpecialZone = false;
//        if (zoneType == ZoneType.aboveValue) inSpecialZone = (newValue > zoneThreshold);
//        if (zoneType == ZoneType.belowValue) inSpecialZone = (newValue < zoneThreshold);
//    }

//    void AnimateUI(double newValue)
//    {
//        if (textComp == null) return;
//        if (newValue == value) return;

//        CheckIfInZone(newValue);

//        if (inSpecialZone)
//        {
//            textComp.fontSize = Mathf.FloorToInt(zoneFontSize * (zoneFontSizeFactor / 100));
//            textComp.color = (newValue < value) ? zoneColorDown : zoneColorUp;
//        }
//        else
//        {
//            textComp.fontSize = Mathf.FloorToInt(normalFontSize *
//                (((newValue < value) ? normalSizeFactorDown : normalSizeFactorUp) / 100)
//            );
//            textComp.color = (newValue < value) ? normalColorDown : normalColorUp;
//        }
//    }
//}
