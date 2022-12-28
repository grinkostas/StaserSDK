using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace StaserSDK.Utilities
{
    public class Timer : MonoBehaviour
    {
        public TimerDelay ExecuteWithDelay(Action action, float delay, TimeScale timeScale = TimeScale.Unscaled)
        {
            return new TimerDelay(action, this, delay, timeScale);
        }

        public TimerDelay ExecuteWithDelay<T>(Action<T> action, T t, float delay,
            TimeScale timeScale = TimeScale.Unscaled)
        {
            return new TimerDelay(() => action(t), this, delay, timeScale);
        }
    }
}

