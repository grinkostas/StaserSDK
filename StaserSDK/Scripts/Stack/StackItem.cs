using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using StaserSDK.Stack;


namespace StaserSDK.Stack
{
    public abstract class StackItem : MonoBehaviour
    {
        [SerializeField] private int _amount;
        [SerializeField] private ItemType _itemType;
        [SerializeField] private StackSize _stackSize;
        [SerializeField] private Transform _wrapper;
        public StackSize StackSize => _stackSize;
        public ItemType Type => _itemType;
        public Transform Wrapper => _wrapper;
        
        public int Amount => _amount;

        public abstract void Claim();
    }

}

