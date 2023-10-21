using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDebugMode : MonoBehaviour
{
    [SerializeField] TOGGLE toggle;
    public static bool isDebugMode = false;
    void Start()
    {
        toggle.AddListener_OnValueChanged(_ => isDebugMode = _);
    }
}
