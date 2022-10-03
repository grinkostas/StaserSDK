using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using StaserSDK.Utilities;
using Zenject;

namespace StaserSDK.Interactable
{
    public class InteractableCharacter : MonoBehaviour
    {
        [SerializeField] private List<Component> _linkedComponents;

        [Inject] private Timer _timer;
        private List<object> _interactDisablers = new List<object>();

        public bool CanInteract { get; private set; }

        private void Awake()
        {
            CanInteract = true;
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
