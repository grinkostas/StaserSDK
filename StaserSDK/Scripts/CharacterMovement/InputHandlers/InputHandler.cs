using UnityEngine;
using UnityEngine.Events;

namespace StaserSDK
{
    public abstract class InputHandler : MovementHandler
    {
        [SerializeField, Range(0, 1)] 
        private float _sensitivity;

        [SerializeField] private bool _maximizeMoveVector;
        
        private bool _enabledHandle = true;
        private bool _isMoving = false;

        

        private void Update()
        {
            HandleMove();
        }

        private void HandleMove()
        {
            if(_enabledHandle == false)
                return;
            var input = GetInput();
            if (Mathf.Abs(input.x) + Mathf.Abs(input.y) < _sensitivity)
            {
                if(_isMoving)
                    OnStopMove?.Invoke();
                _isMoving = false;
                return;
            }

            if(_isMoving == false)
                OnStartMove?.Invoke();
            _isMoving = true;

            if (_maximizeMoveVector)
                input = Maximaze(input);
            
            OnMove?.Invoke(input);
        }

        private Vector3 Maximaze(Vector3 input)
        {
            Vector3 maximizedVector = Vector3.zero;
            maximizedVector.x = CalculateAxisInput(input.x, input.z);
            maximizedVector.z = CalculateAxisInput(input.z, input.x);
            return maximizedVector;
        }
        
        
        private float CalculateAxisInput(float axisToCalculate, float secondAxis)
        {
            if (Mathf.Abs(axisToCalculate) <= _sensitivity)
            {
                return 0.0f;
            }
        
            if (Mathf.Abs(axisToCalculate) >= Mathf.Abs(secondAxis))
                return axisToCalculate < 0 ? -1.0f : 1.0f;

            return axisToCalculate / Mathf.Abs(secondAxis);
        }



        public void EnableHandle() => _enabledHandle = true;
        public void DisableHandle() => _enabledHandle = false;
        
    }  
}


