using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PanelState
{
    Open,
    Close
}

public abstract class PANEL : MonoBehaviour
{
    public abstract bool isShow { get; }
    public abstract void Show();
    public abstract void Hide();
}
