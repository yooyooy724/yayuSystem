using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace yayu.StateMachine
{
    public class StateChangeButton : MonoBehaviour
    {
        [SerializeField] private BUTTON button;
        [SerializeField] private StateMachineKind targetStateMachine;
        [SerializeField] private MONO_STATE state;
        [SerializeField] private List<GameObject> objectsToEnable;
        [SerializeField] private List<GameObject> objectsToDisable;
        bool isInited = false;

        private void Start()
        {
            STATE_MACHINE.GetStateMachine(targetStateMachine).ObserveEveryValueChanged(_ => _.GetCurrentStatePath())
                .Where(_ => state != null)
                .Subscribe(_ => UpdateGameObjects(_.Equals(state.path)));
        }

        void OnEnable()
        {
            Init();
        }

        public void Init()
        {
            if (isInited) return;
            button.AddListener_onClick(ChangeState);
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
