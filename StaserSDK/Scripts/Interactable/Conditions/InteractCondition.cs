using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace StaserSDK.Interactable
{
    public abstract class InteractCondition : MonoBehaviour
    {
        public abstract bool CanInteract(InteractableCharacter character);
    }
}