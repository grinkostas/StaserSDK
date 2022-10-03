﻿using UnityEngine;
using UnityEngine.Events;

namespace StaserSDK.Loading
{
    public abstract class LoadingOperation : MonoBehaviour
    {
        public UnityAction End;

        public abstract void Load();

        protected void Finish()
        {
            End?.Invoke();
        }

    }
}
