using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using StaserSDK.Utilities;
using UnityEngine.Events;

namespace StaserSDK.Interactable
{
    public abstract class Zone<TCharacter> : ZoneBase, IProgressible where TCharacter : InteractableCharacter
    {
        [SerializeField] private bool _multipleInteract = true;
        [SerializeField] private bool _interactWhenStopMove;
        [SerializeField] private float _interactDelay;
        [SerializeField] private List<InteractCondition> _conditions;

        private Coroutine _interacting;
        
        private TCharacter _interactCharacter;
        private bool _characterInside;

        private float _interactProgress = 0.0f;

        private float InteractProgress
        {
            get => _interactProgress;
            
            set
            {
                _interactProgress = Mathf.Clamp01(value);
                ProgressChanged?.Invoke(_interactProgress);
            }
        }
        
        private Coroutine _interactDelayCoroutine;
        private Coroutine _returnProgressCoroutine;

        public TCharacter Character => _interactCharacter;
        public bool IsCharacterInside => _characterInside;
        public UnityAction<float> ProgressChanged { get; set; }

        private void OnDisable()
        {
            if (_interacting != null)
            {
                StopCoroutine(_interacting);
            }

            _interactCharacter = null;
            _interactProgress = 0.0f;
            _characterInside = false;
        }
        
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out TCharacter character))
            {
                OnCharacterEnter(character);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out TCharacter character))
            {
                OnCharacterExit(character);
            }
        }

        private void OnCharacterEnter(TCharacter character)
        {
            if (_characterInside)
                return;

            _characterInside = true;
            OnEnter?.Invoke(character);
            _interactCharacter = character;
            if (InteractProgress >= 1.0f)
                InteractProgress = 0.0f;
            
            StopCoroutine(ReturnProgress());
            StartCoroutine(Interacting());
        }

        private IEnumerator Interacting()
        {
            while (CanInteract(_interactCharacter) == false)
            {
                if (_characterInside == false)
                    yield break;
                
                yield return null;
            }
            yield return InteractDelay();
            if (_characterInside)
            {
                OnInteract?.Invoke(_interactCharacter);
            }

            if (_multipleInteract)
                yield return Interacting();
        }

        

        private IEnumerator InteractDelay()
        {
            float wastedTime = InteractProgress * _interactDelay;
            Vector3 previousPosition = Vector3.zero;
            while (wastedTime <= _interactDelay)
            {
                yield return null;
                if (_characterInside == false || _interactCharacter == null)
                    yield break;
                Vector3 currentPosition = _interactCharacter.transform.position;
                if (_interactWhenStopMove && IsMove(previousPosition, currentPosition))
                {
                    previousPosition = currentPosition;
                    continue;
                }

                wastedTime += Time.deltaTime;
                InteractProgress = wastedTime / _interactDelay;
            }

            if (_multipleInteract)
                InteractProgress = 0.0f;
        }

        private bool IsMove(Vector3 previousPosition, Vector3 currentPosition)
        {
            float deltaX = Mathf.Abs(previousPosition.x - currentPosition.x);
            float deltaZ = Mathf.Abs(previousPosition.z - currentPosition.z);
            return deltaX > 0.01f || deltaZ > 0.01f;
        }

        private IEnumerator ReturnProgress()
        {
            float wastedTime = InteractProgress * _interactDelay;
            while (wastedTime > 0.0f && _characterInside == false)
            {
                wastedTime -= Time.deltaTime;
                InteractProgress = wastedTime / _interactDelay;
                yield return null;
            }
        }

        private bool CanInteract(TCharacter character) =>
            _characterInside && character.CanInteract && PassConditions(character);

        private bool PassConditions(TCharacter character)
        {
            if (_conditions.Count == 0) return true;
            return _conditions.All(condition => condition.CanInteract(character));
        }

        private void OnCharacterExit(TCharacter character)
        {
            if (character != _interactCharacter)
                return;
            StopCoroutine(Interacting());
            _characterInside = false;
            StartCoroutine(ReturnProgress());
            OnExit?.Invoke(character);
            _interactCharacter = null;
        }

    }
}