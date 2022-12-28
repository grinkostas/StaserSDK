using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using Haptic;
using StaserSDK.Interactable;
using UnityEngine.Events;
using Zenject;

namespace StaserSDK.Stack
{
    public abstract class StackTaker : MonoBehaviour
    {
        [SerializeField] private ZoneBase _zoneBase;
        [SerializeField] private Transform _takePoint;
        [SerializeField] private bool _destroyOnTake;
        [SerializeField, ShowIf(nameof(_destroyOnTake))] private float _destroyDelay;
        
        [Inject] private IHapticService _hapticService;
        
        public UnityAction<StackItem> OnTake;
        protected virtual Vector3 DestinationDelta => Vector3.zero;
        protected virtual float Progress => 0.0f;

        private void OnEnable()
        {
            _zoneBase.OnInteract += OnInteract;
        }
        
        private void OnDisable()
        {
            _zoneBase.OnInteract -= OnInteract;
        }

        public abstract ItemType GetTypeToTake(InteractableCharacter interactableCharacter);
        public abstract bool TakePredicate();
        
        private void OnInteract(InteractableCharacter interactableCharacter)
        {
            if(TakePredicate() == false)
                return;
            
            TakeItem(interactableCharacter, GetTypeToTake(interactableCharacter));
        }

        private void TakeItem(InteractableCharacter interactableCharacter, ItemType itemType)
        {
            if (interactableCharacter.TryToGetStack(itemType, out IStack stack) == false) 
                return;
            
            var canTake = stack.TryTake(itemType, out StackItem stackItem, _takePoint, DestinationDelta, Progress);
            if (canTake == false)
                return;
            
            _hapticService.Selection(); 
            OnTake?.Invoke(stackItem);
            OnTakeItem(stackItem);
            if(_destroyOnTake && stackItem != null)
                Destroy(stackItem.gameObject, _destroyDelay);
        }

        protected virtual void OnTakeItem(StackItem stackItem){}
        
    }
}