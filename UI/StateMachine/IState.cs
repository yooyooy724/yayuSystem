using System.Collections.Generic;
using UnityEngine.UIElements;

namespace yayu.UI.StateMachine
{
    public interface IState
    {
        string id { get; }
        string path { get; set; }
        void Enter();
        void Exit();
        IEnumerable<IState> GetChildren();
    }
}