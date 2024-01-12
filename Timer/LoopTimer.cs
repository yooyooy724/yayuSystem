using System;
using yayu.Event;

namespace yayu
{
    public interface ILoopTimer
    {
        void UpdateBySec(double sec);

        double Rate();
        double Interval { get; }
        int LoopCount { get; }
        double timeInInterval { get; }
    }

    public class LoopTimer : ILoopTimer
    {
        private double time;
        private int loopCount;
        private Func<double> interval;

        private CustomEvent<int> onFilled = new CustomEvent<int>();
        /// <summary>
        /// Event triggered when time reach interval.
        /// <para>Argument: int - loop count</para>
        /// </summary>
        public IEventSubscription<int> OnFilled => onFilled;

        public LoopTimer(Func<double> interval)
        {
            this.interval = interval;
            time = 0;
            loopCount = 0;
        }

        public void UpdateBySec(double sec)
        {
            time += sec;
            var itv = Interval;
            while (time >= itv)
            {
                time -= itv;
                loopCount++;
                onFilled.Invoke(loopCount);
            }
        }

        public void Reset()
        {
            time = 0;
            loopCount = 0;
        }

        public double Rate()
        {
            return time / Interval;
        }
        public double Interval => interval();
        public int LoopCount
        {
            get { return loopCount; }
        }
        public double timeInInterval
        {
            get { return time; }
        }
    }
}