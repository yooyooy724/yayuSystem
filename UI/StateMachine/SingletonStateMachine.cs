using System;

namespace yayu.StateMachine
{
    public enum StateMachineKind
    {
        // •K—v‚É‰‚¶‚Ä•ÒW‚·‚é
        Main, Sub
    }
    public static class STATE_MACHINE
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
    }
}