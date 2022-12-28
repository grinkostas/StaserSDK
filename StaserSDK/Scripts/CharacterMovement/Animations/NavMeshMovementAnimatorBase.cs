using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;

namespace StaserSDK
{
    public abstract class NavMeshMovementAnimatorBase : MonoBehaviour
    {
        [SerializeField] protected NavMeshAgentHandler _moveHandler;
        [SerializeField] protected Animator BaseAnimator;

        [SerializeField, ShowIf(nameof(HasAnimator)), AnimatorParam(nameof(BaseAnimator))]
        private string _walkingParamRatio;

        [SerializeField] private float _minMoveRatio;
        [SerializeField] private float _maxSpeed;

        protected bool HasAnimator => BaseAnimator != null;
        
        protected abstract Animator Animator { get; }

        private void OnEnable()
        {
            _moveHandler.OnStartMove.AddListener(OnStartMove);
            _moveHandler.OnMove.AddListener(OnMove);
        }

        private void OnDisable()
        {
            _moveHandler.OnStartMove.RemoveListener(OnStartMove);
            _moveHandler.OnMove.RemoveListener(OnMove);
        }

        protected virtual void OnMove(Vector3 input)
        {
            ChangeSpeedRatio();
        }

        protected virtual void OnStartMove()
        {
            ChangeSpeedRatio();
        }
        
        public void ChangeSpeedRatio()
        {
            if (_moveHandler.Agent.speed <= 0.05f)
            {
                StopMove();
                return;
            }
            float moveRatio = Mathf.Clamp(_moveHandler.Agent.speed / _maxSpeed, _minMoveRatio, 1);
            
            Animator.SetFloat(_walkingParamRatio, moveRatio);
        }

        public void StopMove()
        {
            Animator.SetFloat(_walkingParamRatio, 0);
        }
    }
}
