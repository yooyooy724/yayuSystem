﻿using System;
using UnityEngine;
using UnityEngine.Events;

public abstract class TOGGLE : MonoBehaviour
{
    public abstract bool isOn { get; set; }
    public abstract void AddListener_OnValueChanged(Action<bool> action);
    public abstract void RemoveListener_OnValueChanged(Action<bool> action);
    public abstract void RemoveAllListeners();
}