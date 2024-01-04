using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace yayu.StateMachine
{
    public class StateChangeButton : MonoBehaviour
    {
        private BUTTON _button;
        private BUTTON button
        {
            get
            {
                if (_button == null) _button = GetComponent<BUTTON>();
                return _button;
            }
        }
        [SerializeField] private StateMachineKind targetStateMachine;
        
        [Header("Path Setting [Fill Eather]")]
        [SerializeField] private State_Mono _state;
        private IState state;
        [SerializeField] private string state_path;

        [Header("Others")]
        [SerializeField] private List<GameObject> objectsToEnable;
        [SerializeField] private List<GameObject> objectsToDisable;
        bool isInited = false;

        private void Start()
        {
            if (_state == null) state = STATE_MACHINE.GetStateMachine(targetStateMachine).GetState(state_path);
            else state = _state;

            if (state == null)
            {
                Debug.LogWarning($"State not found for the button '{gameObject.name}'");
                return;
            }
            STATE_MACHINE.GetStateMachine(targetStateMachine).ObserveEveryValueChanged(_ => _.GetCurrentStatePath())
                .Subscribe(_ => UpdateGameObjects(_.Equals(state.path)));
        }

        void OnEnable()
        {
            Init();
        }

        public void Init()
        {
            if (isInited) return;
            button.AddListener_onClick(() => ChangeState());
            isInited = true;
        }

        private void ChangeState()
        {
            if (state == null)
            {
                Debug.LogWarning($"State not set for the button '{gameObject.name}'");
                return;
            }

            var stateMachine = STATE_MACHINE.GetStateMachine(targetStateMachine);
            if (stateMachine == null)
            {
                Debug.LogWarning($"StateMachine '{targetStateMachine}' not found");
                return;
            }

            stateMachine.ChangeState(state.path);
        }

        private void UpdateGameObjects(bool isState)
        {
            foreach (var obj in objectsToEnable)
            {
                if (obj != null) obj.SetActive(isState);
            }

            foreach (var obj in objectsToDisable)
            {
                if (obj != null) obj.SetActive(!isState);
            }
        }
    }
}
