using Doozy.Runtime.UIManager.Containers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UIView))]
public class DoozyPanel : PANEL
{
    private UIContainer _uiContainer;
    private UIContainer uiContainer
    {
        get
        {
            if (_uiContainer == null)
            {
                _uiContainer = GetComponent<UIContainer>();
            }
            return _uiContainer;
        }
    }

    public override bool isShow { get => uiContainer.isVisible || uiContainer.isShowing; }
    public override void Hide() => uiContainer.Hide();
    public override void Show() => uiContainer.Show();
}
