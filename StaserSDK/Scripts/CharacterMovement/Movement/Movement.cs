using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;

namespace StaserSDK
{
    public abstract class Movement : MonoBehaviour
    {
        [SerializeField] private MovementHandler _handler;

        private List<MovementSpeedMultiplayer> _speedMultipliers = new List<MovementSpeedMultiplayer>();
        private const float Tolerance = 0.1f;

        private float SpeedMultipliersAggregated
        {
            get
            {
                if (_speedMultipliers.Count == 0) return 1.0f;
                return _speedMultipliers.Select(x => x.Value).Aggregate((sum, current) => sum * current);
            }
        }

        protected float Speed => TargetSpeed * Time.deltaTime;
        public float TargetSpeed => DefaultSpeed * SpeedMultipliersAggregated;
        
        protected abstract float DefaultSpeed { get; }

        public abstract bool IsMoving { get; }
        public abstract float ActualSpeed { get; }
        
        public MovementHandler Handler => _handler;

        
        public void ApplyMultiplayer(object sender, float multiplier)
        {
            var speedMultiplier = _speedMultipliers.Any(x => x.Sender == sender && Mathf.Abs(x.Value - multiplier) < Tolerance);
            if (speedMultiplier)
                return;
            _speedMultipliers.Add(new MovementSpeedMultiplayer(sender, multiplier));
        }

        public void RemoveMultiplayer(object sender, float multiplierValue)
        {
            var multiplier = _speedMultipliers.Find(x => x.Sender == sender && Mathf.Abs(x.Value - multiplierValue) < Tolerance);

            if (multiplier != null)
                _speedMultipliers.Remove(multiplier);
        }
    } 
}

