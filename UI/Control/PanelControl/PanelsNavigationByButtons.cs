using UnityEngine;

public class PanelsNavigationByButtons : MonoBehaviour
{
    [SerializeField] private BUTTON nextButton;
    [SerializeField] private BUTTON previousButton;
    [SerializeField] private PANEL[] panels;
    private int currentIndex;

    private void Start()
    {
        currentIndex = 0; // 初期インデックスを0に設定

        nextButton.AddListener_onClick(() =>
        {
            NavigateToPanel((currentIndex + 1) % panels.Length);
        });

        previousButton.AddListener_onClick(() =>
        {
            NavigateToPanel((currentIndex - 1 + panels.Length) % panels.Length);
        });

        NavigateToPanel(currentIndex); // 初期パネルを表示
    }

    private void NavigateToPanel(int index)
    {
        currentIndex = index;
        for (int i = 0; i < panels.Length; i++)
        {
            if (i == currentIndex)
            {
                panels[i].Show();
            }
            else
            {
                panels[i].Hide();
            }
        }
    }
}
