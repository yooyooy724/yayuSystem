using System;
using System.Collections.Generic;
using System.Linq;

namespace yayu.Battle
{
    public class CompositeAttackBehavior : IAttackBehavior
    {
        private readonly IAttackBehavior[] behaviors;
        private IEnumerable<IAttackBehavior> attackableBehaviors;

        public CompositeAttackBehavior(params IAttackBehavior[] behaviors)
        {
            this.behaviors = behaviors;
            this.attackableBehaviors = Enumerable.Empty<IAttackBehavior>();
        }

        public double Attack(CharacterInformation attacker, IAttackTarget[] targets)
        {
            return attackableBehaviors.Sum(behavior => behavior.Attack(attacker, targets));
        }

        public bool UpdateCanAttack()
        {
            attackableBehaviors = behaviors.Where(behavior => behavior.UpdateCanAttack());
            return attackableBehaviors.Any();
        }
    }

    // Constant Attack Power
    public class AttackBehavior : IAttackBehavior
    {
        private readonly Func<double> attackPower;
        private readonly Func<int> maxTargetCount;

        public AttackBehavior(Func<double> attackPower, Func<int> maxTargetCount)
        {
            this.attackPower = attackPower;
            this.maxTargetCount = maxTargetCount;
        }

        public double Attack(CharacterInformation attacker, IAttackTarget[] targets)
        {
            int enemyCount = Math.Min(maxTargetCount(), targets.Length);
            for (int i = 0; i < enemyCount; i++)
            {
                targets[i].OnAttacked(attackPower());
            }
            return attackPower() * enemyCount;
        }

        public bool UpdateCanAttack()
        {
            // Always true for constant attack behavior
            return true;
        }
    }


    public class IntervalAttackBehavior : IAttackBehavior
    {
        private readonly IAttackBehavior decoratedBehavior;
        private readonly Func<double> interval;

        double sec;

        public IntervalAttackBehavior(
            Func<double> interval,
            IAttackBehavior decoratedBehavior)
        {
            this.decoratedBehavior = decoratedBehavior;
            this.interval = interval;
            sec = 0;
        }

        public void UpdateBySec(double sec)
        {
            this.sec += sec;
        }

        public double Attack(CharacterInformation attacker, IAttackTarget[] targets)
        {
            long count = (long)(sec / interval());
            sec -= count * interval();

            double totalAttack = 0;
            for (int i = 0; i < count; i++)
            {
                totalAttack += decoratedBehavior.Attack(attacker, targets);
            }
            return totalAttack;
        }

        public bool UpdateCanAttack()
        {
            return sec >= interval();
        }
    }

    public class ConditionalAttackBehavior : IAttackBehavior
    {
        private readonly IAttackBehavior baseBehavior;
        private readonly Func<bool> condition;
        private readonly double additiveBonus;
        private readonly double multiplicativeBonus;

        public ConditionalAttackBehavior(IAttackBehavior baseBehavior, Func<bool> condition, double additiveBonus = 0, double multiplicativeBonus = 1)
        {
            this.baseBehavior = baseBehavior;
            this.condition = condition;
            this.additiveBonus = additiveBonus;
            this.multiplicativeBonus = multiplicativeBonus;
        }

        public double Attack(CharacterInformation attacker, IAttackTarget[] targets)
        {
            double baseAttack = baseBehavior.Attack(attacker, targets);

            if (condition())
            {
                // Apply the bonuses if the condition is true
                baseAttack = (baseAttack + additiveBonus) * multiplicativeBonus;
            }

            return baseAttack;
        }

        public bool UpdateCanAttack()
        {
            // Delegate the decision to the base behavior
            return baseBehavior.UpdateCanAttack();
        }
    }

}