using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace My.UI.StateMachine
{
    public class State_Mono : MonoBehaviour, IState
    {
        [SerializeField] private string _path = "do not need to fill";
        [SerializeField] private List<State_Mono> _children = new();
        private UIPanelMono _panel;
        private UIPanelMono panel
        {
            get
            {
                if (_panel == null) _panel = GetComponent<UIPanelMono>();
                return _panel;
            }
        }
        [SerializeField] private string _id;
        public IEnumerable<IState> GetChildren() => _children.Where(_ => _ != null).AsEnumerable();
        public string id => _id;
        public string path { get => _path; set => _path = value; }
        public virtual void Enter() => panel.Show();
        public virtual void Exit() => panel.Hide();
    }
}


