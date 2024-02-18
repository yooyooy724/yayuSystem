using UnityEngine;
using System;

namespace My.Battle
{
    public class DirectDamageBehavior : IAttackedDamage
    {
        public double AttackedDamage(double attack, object attackFeature = null)
        {
            return attack;
        }
    }

    public class MinimalDamageBehavior : IAttackedDamage
    {
        readonly Func<double> minimalDamage;

        public MinimalDamageBehavior(Func<double> minimalDamage)
        {
            this.minimalDamage = minimalDamage;
        }

        public double AttackedDamage(double attack, object attackFeature = null)
        {
            return Math.Max(minimalDamage(), attack);
        }
    }

    public class EvasionDamageBehavior : IAttackedDamage
    {
        readonly IAttackedDamage attackedDamage;
        readonly Func<double> evasionRate;

        public EvasionDamageBehavior(
            Func<double> evasionRate,
            IAttackedDamage attackedDamage)
        {
            this.attackedDamage = attackedDamage;
            this.evasionRate = evasionRate;
        }

        public double AttackedDamage(double attack, object attackFeature = null)
        {
            //evasion
            if (UnityEngine.Random.value < evasionRate())
            {
                return 0;
            }

            return attackedDamage.AttackedDamage(attack, attackFeature);
        }
    }

    public class SubtractionDamageBehavior : IAttackedDamage
    {
        readonly IAttackedDamage attackedDamage;
        readonly Func<double> subtraction;

        public SubtractionDamageBehavior(
            Func<double> subtraction,
            IAttackedDamage attackedDamage)
        {
            this.attackedDamage = attackedDamage;
            this.subtraction = subtraction;
        }

        public double AttackedDamage(double attack, object attackFeature = null)
        {
            double atk = attack - subtraction();
            atk = Math.Max(atk, 0);
            return attackedDamage.AttackedDamage(atk, attackFeature);
        }
    }
}
