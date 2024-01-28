using UnityEngine;
using UnityEngine.Events;
using yayu.UI;

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
    PANEL panel;
    private void Start()
    {
        panel = GetComponent<PANEL>();
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
        if (panel.isShow) panel.Hide();
        else panel.Show();
    }
}
