using UnityEngine;
using NaughtyAttributes;

namespace StaserSDK
{
    public class MovementAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private CharacterMovement _characterMovement;
        
        [SerializeField, AnimatorParam(nameof(_animator))]
        private string _speedRatioParameter;

        private void OnEnable()
        {
            _characterMovement.Handler.OnMove.AddListener(OnMove);
            _characterMovement.Handler.OnStopMove.AddListener(OnStopMove);
        }
        
        private void OnDisable()
        {
            _characterMovement.Handler.OnMove.RemoveListener(OnMove);
            _characterMovement.Handler.OnStopMove.RemoveListener(OnStopMove);
        }

        private void OnMove(Vector3 input)
        {
            SetRatio(input.magnitude);
        }

        private void OnStopMove() => SetRatio(0);
        

        private void SetRatio(float ratio)
        {
            _animator.SetFloat(_speedRatioParameter, Mathf.Clamp(ratio, 0, 1));
        }
    }
}

