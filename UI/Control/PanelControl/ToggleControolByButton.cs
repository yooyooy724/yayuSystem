using System.Collections.Generic;
using UnityEngine;
using yayu.UI;
public class ToggleControlByButton : MonoBehaviour
{
    [SerializeField] TOGGLE toggle;
    [SerializeField] List<UIButtonMono> buttons;
    // Start is called before the first frame update
    void Start()
    {
        buttons.ForEach(_ => _.AddListener_Click(OnClick));
    }

    void OnClick()
    {
        toggle.isOn = !toggle.isOn;
    }
}
