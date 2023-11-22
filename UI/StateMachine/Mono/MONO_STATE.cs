using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace yayu.StateMachine
{
    public abstract class MONO_STATE : MonoBehaviour, IState
    {
        [SerializeField] private List<MONO_STATE> _children = new();
        public IEnumerable<IState> GetChildren() => _children.Where(_ => _ != null).AsEnumerable();
        public string id { get; protected set; }
        public string path { get; set; }
        public abstract void Enter();
        public abstract void Exit();
    }
}
