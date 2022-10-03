using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace StaserSDK.Utilities
{
    public class Timer : MonoBehaviour
    {
        public void ExecuteWithDelay(Action action, float delay)
        {
            StartCoroutine(Delay(action, delay));
        }
        public void ExecuteWithDelay<T>(Action<T> action, T t, float delay)
        {
            StartCoroutine(Delay(action, t, delay));
        }

        private IEnumerator Delay(Action action, float delay)
        {
            yield return new WaitForSeconds(delay);
            action();
        }
    
        private IEnumerator Delay<T>(Action<T> action, T t, float delay)
        {
            yield return new WaitForSeconds(delay);
            action(t);
        }
    }
}

