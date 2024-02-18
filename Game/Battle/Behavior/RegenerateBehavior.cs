using System;
using System.Collections.Generic;
using System.Linq;

namespace My.Battle
{
    public class RegenerateBehavior : IRegenerateBehavior
    {
        public RegenerateBehavior(Func<double> regenerateAmount)
        {
            this.regenerateAmount = regenerateAmount;
        }
        Func<double> regenerateAmount;
        public double Regenerate()
        {
            return regenerateAmount();
        }

        public bool UpdateCanRegenerate()
        {
            return true;
        }
    }

    // Composite Regenerate Behavior
    public class CompositeRegenerateBehavior : IRegenerateBehavior
    {
        private readonly IRegenerateBehavior[] behaviors;
        private IEnumerable<IRegenerateBehavior> regeneratableBehaviors;

        public CompositeRegenerateBehavior(params IRegenerateBehavior[] behaviors)
        {
            this.behaviors = behaviors;
            regeneratableBehaviors = Enumerable.Empty<IRegenerateBehavior>();
        }

        public double Regenerate()
        {
            return regeneratableBehaviors.Sum(behavior => behavior.Regenerate());
        }

        public bool UpdateCanRegenerate()
        {
            regeneratableBehaviors = behaviors.Where(behavior => behavior.UpdateCanRegenerate());
            return regeneratableBehaviors.Any();
        }
    }

    // Constant Regenerate Behavior
    public class ConstantRegenerateBehavior : IRegenerateBehavior
    {
        private readonly double regenerateRate;

        public ConstantRegenerateBehavior(double regenerateRate)
        {
            this.regenerateRate = regenerateRate;
        }

        public double Regenerate()
        {
            return regenerateRate;
        }

        public bool UpdateCanRegenerate()
        {
            return true;
        }
    }

    //// Interval Regenerate Behavior
    //public class IntervalRegenerateBehavior : IRegenerateBehavior
    //{
    //    private readonly IRegenerateBehavior decoratedBehavior;
    //    private readonly double interval;
    //    private double sec;

    //    public IntervalRegenerateBehavior(IRegenerateBehavior decoratedBehavior, double interval)
    //    {
    //        this.decoratedBehavior = decoratedBehavior;
    //        this.interval = interval;
    //        sec = 0;
    //    }

    //    public void UpdateBySec(double sec)
    //    {
    //        this.sec += sec;
    //    }

    //    public double Regenerate()
    //    {
    //        if (sec >= interval)
    //        {
    //            sec -= interval;
    //            return decoratedBehavior.Regenerate();
    //        }
    //        return 0;
    //    }

    //    public bool UpdateCanRegenerate()
    //    {
    //        return sec >= interval;
    //    }
    //}

    // Conditional Regenerate Behavior
    public class ConditionalRegenerateBehavior : IRegenerateBehavior
    {
        private readonly IRegenerateBehavior baseBehavior;
        private readonly Func<bool> condition;

        public ConditionalRegenerateBehavior(IRegenerateBehavior baseBehavior, Func<bool> condition)
        {
            this.baseBehavior = baseBehavior;
            this.condition = condition;
        }

        public double Regenerate()
        {
            if (condition())
            {
                return baseBehavior.Regenerate();
            }
            return 0;
        }

        public bool UpdateCanRegenerate()
        {
            return condition() && baseBehavior.UpdateCanRegenerate();
        }
    }
}