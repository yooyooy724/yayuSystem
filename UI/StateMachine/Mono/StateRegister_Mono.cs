using UnityEngine;

namespace yayu.UI.StateMachine
{
    public class StateRegister_Mono : MonoBehaviour
    {
        [SerializeField] bool asInitialState = false;
        [SerializeField] StateMachineKind targetStateMachine;
        private void Awake()
        {
            var state = GetComponent<State_Mono>();
            if (state != null)
            {
                if (asInitialState)
                    STATE_MACHINE.GetStateMachine(targetStateMachine).RegisterRootStateAsInitial(state);
                else
                    STATE_MACHINE.GetStateMachine(targetStateMachine).RegisterRootState(state);
            }
        }
    }
}