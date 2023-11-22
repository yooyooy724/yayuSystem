using System;

namespace yayu.StateMachine
{
    public enum StateMachineKind
    {
        // 必要に応じて編集する
        Main, Sub
    }
    public static class STATE_MACHINE
    {
        static StateMachine[] Instances { get; } = new StateMachine[Enum.GetValues(typeof(StateMachineKind)).Length];
        public static StateMachine Main => Instances[(int)StateMachineKind.Main];
        public static StateMachine GetStateMachine(StateMachineKind kind) => Instances[(int)kind];
    }
}