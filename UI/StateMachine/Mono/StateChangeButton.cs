using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace yayu.StateMachine
{
    public class StateChangeButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private StateMachineKind targetStateMachine;
        [SerializeField] private MONO_STATE state;
        [SerializeField] private List<GameObject> objectsToEnable;
        [SerializeField] private List<GameObject> objectsToDisable;

        void OnEnable()
        {
            button.onClick.AddListener(ChangeState);
        }

        void OnDisable()
        {
            button.onClick.RemoveListener(ChangeState);
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

            UpdateGameObjects();
        }

        private void UpdateGameObjects()
        {
            foreach (var obj in objectsToEnable)
            {
                if (obj != null) obj.SetActive(true);
            }

            foreach (var obj in objectsToDisable)
            {
                if (obj != null) obj.SetActive(false);
            }
        }
    }
}
