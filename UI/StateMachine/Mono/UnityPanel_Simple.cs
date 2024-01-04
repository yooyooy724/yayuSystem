using UnityEngine;
using UnityEngine.UI;

namespace yayu.StateMachine
{
    public class UnityPanel_Simple : PANEL
    {
        public enum InitialAction
        {
            Show,
            Hide,
            None
        }
        public override bool isShow => _isShow;
        [SerializeField] bool _isShow;
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
            _isShow = true;
            gameObject.SetActive(_isShow);
        }

        public override void Hide()
        {
            _isShow = false;
            gameObject.SetActive(_isShow);
        }
    }


}