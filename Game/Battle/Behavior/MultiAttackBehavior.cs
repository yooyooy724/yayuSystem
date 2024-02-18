using System;
using System.Collections.Generic;
using System.Linq;

namespace My.Battle
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
            //YDebugger.Log("Attackable Behaviors Count", attackableBehaviors.Count());
            return attackableBehaviors.Sum(behavior => behavior.Attack(attacker, targets));
        }

        public bool UpdateCanAttack()
        {
            attackableBehaviors = behaviors.Where(behavior => behavior.UpdateCanAttack());
            //if (attackableBehaviors.Count() > 0) YDebugger.Log("Attackable Behaviors Count !?!?!?!?!?!?", attackableBehaviors.Count());
            return attackableBehaviors.Any();
        }
    }

    // Constant Attack Power
    public class MultiAttackBehavior : IAttackBehavior
    {
        private readonly Func<double> attackPower;
        private readonly Func<int> maxTargetCount;
        private readonly Func<double> nextTargetDmgFactor;

        public MultiAttackBehavior(Func<double> attackPower, Func<int> maxTargetCount, Func<double> nextTargetDmgFactor)
        {
            this.attackPower = attackPower;
            this.maxTargetCount = maxTargetCount;
            this.nextTargetDmgFactor = nextTargetDmgFactor;
        }

        public double Attack(CharacterInformation attacker, IAttackTarget[] targets)
        {
            int enemyCount = Math.Min(maxTargetCount(), targets.Length);
            double atkPower = attackPower();    
            for (int i = 0; i < enemyCount; i++)
            {
                targets[i].OnAttacked(atkPower * Math.Pow(nextTargetDmgFactor(), i));
            }
            //YDebugger.Log("Multi Attack Behaviors", enemyCount, atkPower);
            return atkPower * enemyCount;
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
        int attackCount;

        public IntervalAttackBehavior(
            LoopTimer loopTimer,
            IAttackBehavior decoratedBehavior)
        {
            this.decoratedBehavior = decoratedBehavior;
            loopTimer.OnFilled.AddListener(_ => attackCount ++);
        }

        public double Attack(CharacterInformation attacker, IAttackTarget[] targets)
        {
            double totalAttack = 0;
            for (int i = 0; i < attackCount; i++)
            {
                totalAttack += decoratedBehavior.Attack(attacker, targets);
            }
            //YDebugger.Log("Interval Attack Behaviors", totalAttack);
            attackCount = 0;
            return totalAttack;
        }

        public bool UpdateCanAttack()
        {
            if (attackCount > 0)
            {
                //YDebugger.Log("Interval Attack !!!!!", attackCount);
                return true;
            }
            attackCount = 0;
            return false;
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