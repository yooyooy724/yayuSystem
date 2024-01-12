using System;
using System.Linq;

namespace yayu.Battle
{
    public interface IAttackBehavior
    {
        public double Attack(CharacterInformation attacker, IAttackTarget[] targets);
        public bool UpdateCanAttack();
    }

    public interface IRegenerateBehavior
    {
        public double Regenerate();
        public bool UpdateCanRegenerate();
    }

    public interface IAttackTarget
    {
        public void OnAttacked(double attack, object attackFeature = null);
    }

    public interface IAttackedDamage
    {
        public double AttackedDamage(double attack, object attackFeature = null);
    }

    public class NormalAttackedDamage
    {
        public double AttackedDamage(double attack, object attackFeature = null) => attack;
    }

    public class CharacterControl : IAttackTarget
    {
        CharacterInformation info { get; }
        CharacterState state { get; }
        CharacterEvents events { get; }
        IAttackBehavior attackBehavior { get; }
        IAttackedDamage attackedDamage { get; }
        IRegenerateBehavior regenerateBehavior { get; }
        CharacterStateControl characterStateControl { get; }

        public ICharacter[] enemies;

        public CharacterControl(
                CharacterInformation info,
                    CharacterState state,
                    CharacterEvents events,
                    IAttackBehavior attackBehaviors,
                    IAttackedDamage attackedDamage,
                    IRegenerateBehavior regenerateBehavior,
                    CharacterBehaviorState behaviorStateAfterDead)
        {
            this.info = info;
            this.state = state;
            this.events = events;
            this.attackBehavior = attackBehaviors;
            this.attackedDamage = attackedDamage;
            this.regenerateBehavior = regenerateBehavior;
            characterStateControl = new CharacterStateControl(state, events, info.MaxHp, behaviorStateAfterDead);

            if(this.attackedDamage == null) this.attackedDamage = new DirectDamageBehavior();
        }

        public void SetEnemies(ICharacter[] enemies)
        {
            this.enemies = enemies;
        }

        public void UpdateBySec(double sec)
        {
            AttackProcess();
            RegenerateProcess();
        }

        private void AttackProcess()
        {
            //YDebugger.Log("attackBehavior == null", attackBehavior == null);
            //YDebugger.Log("state.BehaviorState.CanAttack", state.BehaviorState.CanAttack);
            if (attackBehavior == null) return;
            if (!state.BehaviorState.CanAttack) return;
            var targets = enemies.Select(enemy => enemy.ctr as IAttackTarget).ToArray();
            if (attackBehavior.UpdateCanAttack())
            {
                double attackPower = attackBehavior.Attack(info, targets);
                events.OnAttackTrigger.Invoke(attackPower);
                YDebugger.Log("attackPower", attackPower);
            }
        }

        private void RegenerateProcess()
        {
            if(regenerateBehavior == null) return;
            if (!state.BehaviorState.CanRegenerate) return;
            if (regenerateBehavior.UpdateCanRegenerate())
            {
                double regenerateAmount = regenerateBehavior.Regenerate();
                characterStateControl.Regenerate(regenerateAmount);
            }
        }

        public void OnAttacked(double attack, object attackFeature = null)
        {
            events.OnAttackedTrigger.Invoke(attack, attackFeature);
            double damage = attackedDamage.AttackedDamage(attack, attackFeature);
            characterStateControl.Damage(damage);
        }
        // Implementations for RegenerateProcess and RestProcess
    }
}