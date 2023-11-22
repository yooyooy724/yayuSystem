using System;
using UnityEngine;

public abstract class BUTTON : MonoBehaviour
{
    public abstract bool interactable { get; set; }
    public abstract bool visible { get; set; }
    public abstract void AddListener_onClick(Action action);
    public abstract void AddListener_onEnter(Action action);
    public abstract void AddListener_onExit(Action action);
    public abstract void RemoveListener_onClick(Action action);
    public abstract void RemoveListener_onEnter(Action action);
    public abstract void RemoveListener_onExit(Action action);
    public abstract void RemoveAllListeners();
}