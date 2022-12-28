using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace StaserSDK.Stack
{
    public class TakeData
    {
        public StackItem Target { get; }
        public Transform DestinationPoint { get; }
        public Vector3 Delta { get; }
        public float Progress { get; }

        public TakeData(StackItem targetItem, Transform destinationPoint, Vector3 delta = new Vector3(), float progress = 0.0f)
        {
            Target = targetItem;
            DestinationPoint = destinationPoint;
            Delta = delta;
            Progress = progress;
        }
    }
}

