using System;

namespace yayu.Battle
{
    /// <summary>
    /// Represents the state of a character, including health and behavioral state.
    /// </summary>
    public class CharacterState
    {
        public CharacterBehaviorState BehaviorState { get; set; }
        public double Hp { get; set; }

        public CharacterState(double initialHp)
        {
            Hp = initialHp;
            BehaviorState = CharacterBehaviorState.Resting; // Set initial state to resting
        }
    }

    public class CharacterStateControl
    {
        readonly CharacterState state;
        readonly CharacterEvents events;
        readonly Func<double> MaxHp;
        public CharacterStateControl(
            CharacterState state, 
            CharacterEvents events,
            Func<double> MaxHp)
        {
            this.state = state;
            this.events = events;
            this.MaxHp = MaxHp;
        }

        public void Damage(double damage)
        {
            state.Hp -= damage;
            state.Hp = Math.Max(state.Hp, 0);
            events.OnDamagedTrigger.Invoke(damage);
            UpdateOnHpChange();
        }

        public void Regenerate(double amount)
        {
            state.Hp += amount;
            state.Hp = Math.Min(state.Hp, MaxHp());
            events.OnRegenerateTrigger.Invoke(amount);
            UpdateOnHpChange();
        }

        public void UpdateBySec(double deltaTime)
        {
            if (state.Hp <= 0)
            {
                if (state.BehaviorState.Compare(CharacterBehaviorState.Dying))
                {
                    state.BehaviorState = CharacterBehaviorState.Dying;
                    events.OnDeadTrigger.Invoke();
                }
            }
        }

        void UpdateOnHpChange()
        {
            if (
                state.Hp <= 0 &&
                !state.BehaviorState.Compare(CharacterBehaviorState.Dying))
            {
                state.BehaviorState = CharacterBehaviorState.Dying;
                events.OnDeadTrigger.Invoke();
            }
            else if (
                state.BehaviorState.Compare(CharacterBehaviorState.Dying) &&
                state.Hp > 0)
            {
                state.BehaviorState = CharacterBehaviorState.Resting;
            }
        }
    }

    /// <summary>　
    /// このクラスのbooleanは主にCharacterControlクラスとの対応関係を想定します。
    /// </summary>
    public struct CharacterBehaviorState
    {
        private CharacterBehaviorState(bool canRest, bool canRegenerate, bool canAttack)
        {
            CanRest = canRest;
            CanRegenerate = canRegenerate;
            CanAttack = canAttack;
        }

        public bool CanRest { get; }
        public bool CanRegenerate { get; }
        public bool CanAttack { get; }

        // 以下にBehaviorStateを定義する。
        public static readonly CharacterBehaviorState Battling = new CharacterBehaviorState(false, true, true);
        public static readonly CharacterBehaviorState Resting = new CharacterBehaviorState(true, false, false);
        public static readonly CharacterBehaviorState Dying = new CharacterBehaviorState(false, false, false);
    }

    public static class CharacterBehaviorStateExtension
    {
        public static bool Compare(this CharacterBehaviorState a, CharacterBehaviorState b)
        {
            return a.CanRest == b.CanRest && a.CanRegenerate == b.CanRegenerate && a.CanAttack == b.CanAttack;
        }
    }
}