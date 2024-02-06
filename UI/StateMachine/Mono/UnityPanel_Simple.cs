using UnityEngine;
using yayu.UI;

namespace yayu.StateMachine
{
    public class UnityPanel_Simple : UIPanelMono
    {
        public override bool isOn
        {
            get => gameObject.activeSelf;
            set
            {
                if (isOn == value) return;
                if (value) Show();
                else Hide();
            }
        }

        public override void Show()
        {
            gameObject.SetActive(true);
        }

        public override void Hide()
        {
            gameObject.SetActive(false);
        }
    }


}