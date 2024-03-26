using R3;
using System;
using System.Linq;
using UnityEngine;

namespace yayu
{
    public class RepeatableTimerData
    {
        public long Count;
        public double currentSecond;
        public bool IsStarting;
        public bool IsAutoRepeat = true;
    }

    public class RepeatableTimer
    {
        //�������璼�ڃZ�[�u�f�[�^�ɃA�N�Z�X���Ȃ��B���|�W�g�����l�̕ύX���Ď����ăZ�[�u����B
        private RepeatableTimerData data;
        private Func<double> requiredTime;
        /// <summary>
        /// ������StepCount
        /// </summary>
        private Action<long> onNextStep = (_) => { };

        public RepeatableTimer(RepeatableTimerData data, Func<double> requiredTime) 
        {
            this.requiredTime = requiredTime;
            this.data = data;
        }

        public void UpdatePerTime(double time = 0.02f)
        {
            if (!data.IsStarting) return;

            data.currentSecond += time;
            if (IsFilled)
            {
                if (data.IsAutoRepeat)
                {
                    double div = data.currentSecond / requiredTime();
                    int stepCount = (int)div;
                    data.Count += stepCount;
                    data.currentSecond = data.currentSecond - requiredTime() * stepCount;
                    onNextStep(stepCount);
                }
                else
                {
                    data.currentSecond = requiredTime();
                    data.IsStarting = false;
                }
            }
        }
        // Create Instance
        public void AddOnNextStepCallback(Action<long> onCompleted)
        {
            this.onNextStep += onCompleted;
        }

        //�����͐��Domain�ɏ����Ȃ�
        //�����̏���񋟂���̂�root�̐Ӗ�
        public double currentSecond => data.currentSecond;
        public double totalSecond => requiredTime();
        public bool IsFilled => data.currentSecond >= requiredTime();
        public bool IsStarting => data.IsStarting;
        public bool IsAutomating => data.IsAutoRepeat;
        public long Count => data.Count;

        public void Start()
        {
            if (IsFilled)
            {
                data.Count++;
                data.currentSecond = 0;
                onNextStep(1);
            }
            data.IsStarting = true;
        }

        public void Stop()
        {
            data.IsStarting = false;
        }

        public void OnReset()
        {
            data.Count = 0;
        }

        public void ChangeAutoRepeatState(bool isOn)
        {
            data.IsAutoRepeat = isOn;
        }
    }

}