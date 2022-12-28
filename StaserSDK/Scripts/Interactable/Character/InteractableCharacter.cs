using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using StaserSDK.Stack;
using StaserSDK.Utilities;
using Zenject;

namespace StaserSDK.Interactable
{
    public class InteractableCharacter : MonoBehaviour
    {
        [SerializeField] private List<StackProvider> _stackProviders;
        [SerializeField] private List<Component> _linkedComponents;

        [Inject] private Timer _timer;
        private List<object> _interactDisablers = new List<object>();

        public bool CanInteract { get; private set; }

        private void Awake()
        {
            CanInteract = true;
        }

        public bool TryToGetStack(StackPlace stackPlace, out IStack stack)
        {
            stack = _stackProviders.Find(x => x.Interface.StackPlace == stackPlace).Interface;
            return stack != null;
        }

        public bool TryToGetStack(ItemType itemType, out IStack stack)
        {
            stack = _stackProviders.Find(stackProvider => stackProvider.Interface.StoredType == itemType || stackProvider.Interface.StoredType == ItemType.Any)?.Interface;
            return stack != null;
        }

        public bool TryToGetComponent<TComponent>(out TComponent component) where TComponent : Component
        {
            var foundComponent = _linkedComponents.Find(component => typeof(TComponent) == component.GetType());
            component = (TComponent)foundComponent;
            return foundComponent != null;
        }

        public void DisableInteractForSeconds(object sender, float duration)
        {
            DisableInteract(sender);
            _timer.ExecuteWithDelay(() => EnableInteract(sender), duration);
        }

        public void DisableInteract(object disabler)
        {
            _interactDisablers.Add(disabler);
            CanInteract = false;
        }

        public void EnableInteract(object disabler)
        {
            _interactDisablers.Remove(disabler);
            if (_interactDisablers.Count == 0)
                CanInteract = true;
        }

    }
}
