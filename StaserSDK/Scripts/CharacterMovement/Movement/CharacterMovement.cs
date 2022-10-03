using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace StaserSDK
{
    public abstract class CharacterMovement : Movement
    {
        private void OnEnable()
        {
            Handler.OnMove.AddListener(OnHandledMove);
        }

        private void OnDisable()
        {
            Handler.OnMove.RemoveListener(OnHandledMove);
        }

        private void OnHandledMove(Vector3 input)
        {
            Move(input);
        }
        
        protected abstract void Move(Vector3 input);

    }
}
