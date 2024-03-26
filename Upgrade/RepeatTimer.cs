using R3;
using System;
using System.Linq;
using UnityEngine;

namespace yayu
{
    [Serializable]
    public class RepeatTimerData
    {
        public long Count = 0;
        public double CurrentProgress = 0;
        public RepeatTimerState timerState = RepeatTimerState.notStarted;
        public RepeatTimerMode mode = RepeatTimerMode.stopOnFilled;
    }

    public enum RepeatTimerState
    {
        notStarted,
        running,
        filled,
        pausing
    }

    public enum RepeatTimerMode
    {
        stopOnFilled,
        nextAndStopOnFilled,
        repeatOnFilled
    }


    public class RepeatTimer
    {
        //ここから直接セーブデータにアクセスしない。リポジトリが値の変更を監視してセーブする。
        public RepeatTimerData data { set; get; }   
        private Func<long, double> requiredTime;
        /// <summary>
        /// 引数はStepCount
        /// </summary>
        private Action<long> onNextStep = (_) => { };

        public RepeatTimer(
            RepeatTimerData data,
            Func<long, double> requiredTime)
        {
            this.data = data;
            this.requiredTime = requiredTime;
        }

        public void UpdatePerTime(double time = 0.02f)
        {
            //Debug.Log("呼ばれてる？1");
            if (!(data.timerState == RepeatTimerState.running)) return;

            //Debug.Log("呼ばれてる？2");
            //Debug.Log($"ちゃんと足してる？ time, speed: {time}, {speed}, {currentProgress.Value}");
            data.CurrentProgress += time;
            if (IsFilled)
            {
                if (data.mode == RepeatTimerMode.repeatOnFilled)
                {
                    double div = 1;// data.CurrentProgress / requiredTime(data.Count);
                    int stepCount = (int)div;
                    data.Count += stepCount;
                    data.CurrentProgress = data.CurrentProgress - requiredTime(data.Count) * stepCount;
                    onNextStep(stepCount);
                }
                else if (data.mode == RepeatTimerMode.nextAndStopOnFilled)
                {
                    data.Count++;
                    data.CurrentProgress = 0;
                    onNextStep(1);
                    data.timerState = RepeatTimerState.notStarted;
                }
                else
                {
                    data.timerState = RepeatTimerState.filled;
                }
            }
        }
        // Create Instance
        public void AddOnNextStepCallback(Action<long> onCompleted)
        {
            this.onNextStep += onCompleted;
        }

        //これらは絶対Domainに書かない
        //これらの情報を提供するのはrootの責務
        public double currentSecond => data.CurrentProgress;
        public double totalSecond => requiredTime(data.Count);
        public bool IsFilled => data.CurrentProgress >= totalSecond;
        public bool IsAutomating => data.mode == RepeatTimerMode.repeatOnFilled;
        public bool IsRunning => data.timerState == RepeatTimerState.running;
        public double Count => data.Count;

        public void Start()
        {
            Debug.Log("Start");
            if(data.timerState == RepeatTimerState.filled)
            {
                data.Count++;
                data.CurrentProgress = 0;
                onNextStep(1);
            }
            data.timerState = RepeatTimerState.running;
        }

        public void Stop()
        {
            Debug.Log("Stop");
            if (IsRunning)
                data.timerState = RepeatTimerState.pausing;
        }

        public void OnResetTimer()
        {
            data.Count = 0;
            data.CurrentProgress = 0;
            data.timerState = RepeatTimerState.notStarted;
            //data.mode = defaultMode;
        }

        public void ChangeMode(RepeatTimerMode mode)
        {
            data.mode = mode;
        }
    }
}