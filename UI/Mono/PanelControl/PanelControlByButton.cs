using UnityEngine;
using UnityEngine.Events;
using My.UI;

public class PanelControlByButton : MonoBehaviour
{
    enum PanelControlType
    {
        close,
        open,
        openAndClose
    }
    [SerializeField] PanelControlType containerControlType;
    [SerializeField] UIButtonMono button;
    UIPanelMono panel;
    private void Start()
    {
        panel = GetComponent<UIPanelMono>();
        UnityAction action = containerControlType switch
        {
            PanelControlType.close => panel.Hide,
            PanelControlType.open => panel.Show,
            PanelControlType.openAndClose => SwitchContainer,
            _ => () => { }
        };
        button.AddListener_Click(() => action());
    }

    private void SwitchContainer()
    {
        if (panel.isOn) panel.Hide();
        else panel.Show();
    }
}
