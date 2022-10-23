using System;
using UnityEngine;

namespace StaserSDK
{
    public class CharacterControllerMovement : CharacterMovement
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private GameObject _rotationModel;
        [SerializeField] private float _speed;
        
        protected override float DefaultSpeed => _speed;
        public override float ActualSpeed => _characterController.velocity.magnitude;
        public override bool IsMoving => _characterController.velocity.magnitude > 0.15f;

        protected override void Move(Vector3 input)
        {
            if (_characterController.isGrounded == false)
                input.y -= 1;
            
            Rotate(input);
            float magnitude = input.magnitude;
            float magnitudeCoefficient = magnitude > 1.0f ? magnitude : 1.0f;
            _characterController.Move(input * Speed / magnitudeCoefficient);
        }
    
        private void Rotate(Vector3 move)
        {
            Quaternion rotation = Quaternion.LookRotation(move, Vector3.up);
            var angles = Vector3.Scale(rotation.eulerAngles, Vector3.up);
            _rotationModel.transform.rotation = Quaternion.Euler(angles);
        }
    }
}

