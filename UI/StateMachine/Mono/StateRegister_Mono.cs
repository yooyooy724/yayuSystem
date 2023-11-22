using UnityEngine;

namespace yayu.StateMachine
{
    public class StateRegister_Mono : MonoBehaviour
    {
        [SerializeField] StateMachineKind targetStateMachine;
        private void Awake()
        {
            var state = GetComponent<MONO_STATE>();
            if (state != null)
            {
                STATE_MACHINE.GetStateMachine(targetStateMachine).RegisterRootState(state);
            }
        }
    }
}