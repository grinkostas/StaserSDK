using UnityEngine;
using System.Collections;
using System;


namespace StaserSDK.Utilities
{
    public class TimerDelay
    {
        public float Duration { get; }
        public float WaitedTime { get; private set; } = 0.0f;
        public float TimeLeft => Duration - WaitedTime;
        public TimerStatus Status { get; private set; }
        private TimeScale Scale;
        private Coroutine _coroutine;
        private Timer _timer;
        private bool _paused = false;

        private Action _additionalAction;
        
        public TimerDelay(Action action, Timer timer, float duration, TimeScale scale = TimeScale.Unscaled)
        {
            Status = TimerStatus.Active;
            _timer = timer;
            Duration = duration;
            _coroutine = timer.StartCoroutine(Delay(action));
        }

        public void Kill()
        {
            if(Status == TimerStatus.Ended)
                return;
            
            Status = TimerStatus.Ended;
            _timer.StopCoroutine(_coroutine); 
        }

        public void Pause()
        {
            _paused = true;
        }

        public void Resume()
        {
            _paused = false;
        }

        public TimerDelay AddAction(Action action)
        {
            _additionalAction += action;
            return this;
        }

        public IEnumerator Delay(Action action)
        {
            while (WaitedTime < Duration)
            {
                if (_paused == false)
                {
                    if(Scale == TimeScale.Unscaled)
                        WaitedTime += Time.unscaledDeltaTime;
                    else
                        WaitedTime += Time.deltaTime;
                }
                
                yield return null;
            }
            
            Status = TimerStatus.Ended;
            action();
            _additionalAction?.Invoke();
        }

    }
}
