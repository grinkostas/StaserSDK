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
        [SerializeField] private float _interactDelay;
        [SerializeField] private List<InteractCondition> _conditions;

        private TCharacter _interactCharacter;
        private bool _characterInside;
        public bool IsCharacterInside => _characterInside;

        public UnityAction<float> ProgressChanged { get; set; }

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
            OnInteract?.Invoke(_interactCharacter);

            if (_multipleInteract)
                yield return Interacting();
        }

        private IEnumerator InteractDelay()
        {
            Debug.Log("Start Delay");
            float wastedTime = 0.0f;
            while (wastedTime <= _interactDelay)
            {
                yield return null;
                if (_characterInside == false)
                {
                    ProgressChanged?.Invoke(0.0f);
                    yield break;
                }

                wastedTime += Time.deltaTime;
                float progress = wastedTime / _interactDelay;
                ProgressChanged?.Invoke(progress);
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

            _characterInside = false;
            Debug.Log("Exit");
            OnExit?.Invoke(character);
            _interactCharacter = null;
        }

    }
}