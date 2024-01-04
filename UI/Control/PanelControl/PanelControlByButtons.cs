using UnityEngine;

public class PanelControlByButtons : MonoBehaviour
{
    [SerializeField] private BUTTON[] openButtons;
    [SerializeField] private BUTTON[] closeButtons;

    [SerializeField] private PANEL panel;

    private void Start()
    {
        foreach (var button in openButtons)
        {
            button.AddListener_onClick(panel.Show);
        }
        foreach (var button in closeButtons)
        {
            button.AddListener_onClick(panel.Hide);
        }
    }
}
