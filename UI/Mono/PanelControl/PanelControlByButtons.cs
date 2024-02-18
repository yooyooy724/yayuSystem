using UnityEngine;
using My.UI;

public class PanelControlByButtons : MonoBehaviour
{
    [SerializeField] private UIButtonMono[] openButtons;
    [SerializeField] private UIButtonMono[] closeButtons;

    [SerializeField] private UIPanelMono panel;

    private void Start()
    {
        foreach (var button in openButtons)
        {
            button.AddListener_Click(panel.Show);
        }
        foreach (var button in closeButtons)
        {
            button.AddListener_Click(panel.Hide);
        }
    }
}
