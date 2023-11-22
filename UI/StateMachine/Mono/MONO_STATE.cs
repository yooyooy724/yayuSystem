using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace yayu.StateMachine
{
    public abstract class MONO_STATE : MonoBehaviour, IState
    {
        [SerializeField] private string _path;
        [SerializeField] private List<MONO_STATE> _children = new();
        public IEnumerable<IState> GetChildren() => _children.Where(_ => _ != null).AsEnumerable();
        public abstract string id { get; }
        public string path { get => _path; set => _path = value; }
        public abstract void Enter();
        public abstract void Exit();
    }
}
