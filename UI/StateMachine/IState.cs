using System.Collections.Generic;

namespace yayu.StateMachine
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