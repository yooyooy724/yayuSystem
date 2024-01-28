using UnityEngine;
using yayu.UI;

public class PanelControlByButtons : MonoBehaviour
{
    [SerializeField] private UIButtonMono[] openButtons;
    [SerializeField] private UIButtonMono[] closeButtons;

    [SerializeField] private PANEL panel;

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
