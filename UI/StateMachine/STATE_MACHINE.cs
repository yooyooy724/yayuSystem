using System;
using UnityEngine;

namespace yayu.UI.StateMachine
{
    public enum StateMachineKind
    {
        // •K—v‚É‰ž‚¶‚Ä•ÒW‚·‚é
        Main, Game, 
    }
    public class STATE_MACHINE : MonoBehaviour
    {
        static StateMachine[] Instances { get; } = new StateMachine[Enum.GetValues(typeof(StateMachineKind)).Length];
        public static StateMachine Main => Instances[(int)StateMachineKind.Main];
        public static StateMachine GetStateMachine(StateMachineKind kind)
        {
            if(Instances[(int)kind] == null)
            {
                Instances[(int)kind] = new StateMachine();
            }
            return Instances[(int)kind];
        }
        private void OnDestroy()
        {
            foreach (var instance in Instances)
            {
                instance?.Dispose();
            }
        }
    }
}