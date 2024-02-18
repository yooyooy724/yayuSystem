using System.Collections.Generic;
using R3;
using UnityEngine;

namespace My.UI.StateMachine
{
    public class StateAndPanelSyncControl : MonoBehaviour
    {
        [SerializeField] private StateMachineKind targetStateMachine;

        [Header("Path Setting [Fill Eather]")]
        [SerializeField] private State_Mono _state;
        private IState state;
        [SerializeField] private string state_path;

        [Header("Others")]
        [SerializeField] private List<UIPanelMono> panelsToEnable;
        [SerializeField] private List<GameObject> objectsToEnable;
        [SerializeField] private List<UIPanelMono> panelsToDisable;
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
            STATE_MACHINE.GetStateMachine(targetStateMachine).OnChangeState.AddListener(_ => UpdateGameObjects(_.path.Equals(state.path)));
        }

        private void UpdateGameObjects(bool isState)
        {
            if (isState)
            {
                foreach (var panel in panelsToEnable) panel.Show();
                foreach (var panel in panelsToDisable) panel.Hide();
            }
            else
            {
                foreach (var panel in panelsToEnable) panel.Hide();
                foreach (var panel in panelsToDisable) panel.Show();
            }

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
