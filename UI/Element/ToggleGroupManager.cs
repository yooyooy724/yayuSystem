using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ToggleGroupManager : MonoBehaviour
{
    [SerializeField] Transform togglesParent;
    ToggleUnitForToggleGroup[] units;
    TOGGLE[] toggles;
    [HideInInspector] public int currentIndex;
    ToggleTextInfo[] toggleTextInfos;

    public void Init(ToggleTextInfo[] _toggleTextInfos)
    {
        this.toggleTextInfos = _toggleTextInfos;
        int len = toggleTextInfos.Length;
        //Debug.Log("len: " + len);
        units = togglesParent.GetComponentsInChildren<ToggleUnitForToggleGroup>();
        toggles = units.Select(_ => _.toggle).ToArray();
        if (units.Length < len)
        {
            Debug.LogWarning("unit ‘«‚è‚Ä‚È‚¢‚ª");
            len = units.Length;
        }
        for (int i = 0; i < len; i++)
        {
            var info = toggleTextInfos[i];
            units[i].InitToggle((_) =>
            {
                UpdateIndex();
            });
            units[i].InitTextField(info.text);
            //Debug.Log(info.text());
        }
        this.ObserveEveryValueChanged(_ => _.currentIndex).Subscribe(_ => toggleTextInfos[_].onSelected()).AddTo(this);
    }

    private void UpdateIndex()
    {
        for (int i = 0; i < toggles.Length; i++)
        {
            if(toggles[i].isOn == true)
            {
                currentIndex = i;
                break;
            }
        }
    }
}

public class ToggleTextInfo
{
    public ToggleTextInfo(Action onSelected, Func<string> text)
    {
        this.onSelected = onSelected;
        this.text = text;
    }
    public Action onSelected { get; private set; }
    public Func<string> text { get; private set; }
}
