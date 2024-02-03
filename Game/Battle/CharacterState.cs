using System;

namespace yayu.Battle
{
    /// <summary>
    /// Represents the state of a character, including health and behavioral state.
    /// </summary>
    public class CharacterState
    {
        CharacterBehaviorState _BehaviorState;
        public CharacterBehaviorState BehaviorState 
        { 
            get => _BehaviorState;
            set
            {
                _BehaviorState = value;
                //YDebugger.Log("::::::::::::::::::::::BehaviorState Update", _BehaviorState);
            }
        }

        public double _Hp;
        public double Hp 
        { 
            get => _Hp;
            set
            {
                _Hp = value;
                //YDebugger.Log("Hp Update", _Hp);
            }
        }

        public CharacterState(double initialHp, CharacterBehaviorState initialBehaviorState)
        {
            Hp = initialHp;
            BehaviorState = initialBehaviorState;
        }
    }

    public class CharacterStateControl
    {
        readonly CharacterState state;
        readonly CharacterEvents events;
        readonly Func<double> MaxHp;
        readonly CharacterBehaviorState stateAfterDead;

        public CharacterStateControl(
            CharacterState state, 
            CharacterEvents events,
            Func<double> MaxHp,
            CharacterBehaviorState stateAfterDead)
        {
            this.state = state;
            this.events = events;
            this.MaxHp = MaxHp;
            this.stateAfterDead = stateAfterDead;
        }

        public void Damage(double damage)
        {
            state.Hp -= damage;
            state.Hp = Math.Max(state.Hp, 0);
            events.OnDamagedTrigger.Invoke(damage);
            OnDamage();
        }

        public void Regenerate(double amount)
        {
            state.Hp += amount;
            state.Hp = Math.Min(state.Hp, MaxHp());
            events.OnRegenerateTrigger.Invoke(amount);
        }

        void OnDamage()
        {
            if (
                state.Hp <= 0 &&
                !state.BehaviorState.Compare(stateAfterDead))
            {
                state.BehaviorState = stateAfterDead;
                YDebugger.Log("CharacterStateControl", "OnDeadTrigger");
                events.OnDeadTrigger.Invoke();
            }
        }
    }

    /// <summary>　
    /// このクラスのbooleanは主にCharacterControlクラスとの対応関係を想定します。
    /// </summary>
    public struct CharacterBehaviorState
    {
        private CharacterBehaviorState(bool canRegenerate, bool canAttack)
        {
            //CanRest = canRest;
            CanRegenerate = canRegenerate;
            CanAttack = canAttack;
        }

        //public bool CanRest { get; }
        public bool CanRegenerate { get; }
        public bool CanAttack { get; }

        // 以下にBehaviorStateを定義する。
        public static readonly CharacterBehaviorState Battling = new CharacterBehaviorState(true, true);
        public static readonly CharacterBehaviorState Resting = new CharacterBehaviorState(true, false);
        public static readonly CharacterBehaviorState Dying = new CharacterBehaviorState(false, false);
    }

    public static class CharacterBehaviorStateExtension
    {
        public static bool Compare(this CharacterBehaviorState a, CharacterBehaviorState b)
        {
            return a.CanRegenerate == b.CanRegenerate && a.CanAttack == b.CanAttack;
        }
    }
}