using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

namespace StaserSDK
{
    public abstract class MovementHandler : MonoBehaviour
    {
        public UnityEvent OnStartMove;
        public UnityEvent OnStopMove;
        public UnityEvent<Vector3> OnMove;
        protected abstract Vector3 GetInput();
    }
}