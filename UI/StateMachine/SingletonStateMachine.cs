using System;

namespace yayu.StateMachine
{
    public enum StateMachineKind
    {
        // �K�v�ɉ����ĕҏW����
        Main, Sub
    }
    public static class STATE_MACHINE
    {
        static StateMachine[] Instances { get; } = new StateMachine[Enum.GetValues(typeof(StateMachineKind)).Length];
        public static StateMachine Main => Instances[(int)StateMachineKind.Main];
        public static StateMachine GetStateMachine(StateMachineKind kind) => Instances[(int)kind];
    }
}