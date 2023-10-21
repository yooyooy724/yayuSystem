using System.Collections.Generic;
using UnityEngine;

public class ToggleControolByButton : MonoBehaviour
{
    [SerializeField] TOGGLE toggle;
    [SerializeField] List<BUTTON> buttons;
    // Start is called before the first frame update
    void Start()
    {
        buttons.ForEach(_ => _.AddListener_onClick(OnClick));
    }

    void OnClick()
    {
        toggle.isOn = !toggle.isOn;
    }
}
