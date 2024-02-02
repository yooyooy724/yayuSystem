using UnityEngine;
using yayu.UI;

namespace yayu.StateMachine
{
    public class UnityPanel_Simple : UIPanelMono
    {
        public enum InitialAction
        {
            Show,
            Hide,
            None
        }
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
        [SerializeField] InitialAction initialAction = InitialAction.None;

        private void Awake()
        {
            switch (initialAction)
            {
                case InitialAction.Show:
                    Show();
                    break;
                case InitialAction.Hide:
                    Hide();
                    break;
                case InitialAction.None:
                    break;
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