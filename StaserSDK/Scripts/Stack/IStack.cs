using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;


namespace StaserSDK.Stack
{
    public interface IStack
    {
        public ItemType StoredType { get; }
        public int ItemsCount { get; }
        public int MaxSize { get; }
        public StackPlace StackPlace { get; }
        public IEnumerable<StackItem> Items { get; }        
        
        public UnityAction<AddData> AddedItem { get; set; } 
        public UnityAction<TakeData> TookItem { get; set; }
        public UnityAction<int> CountChanged { get; set; }       

        public bool TryAdd(StackItem stackItem);

        public bool TryTakeLast(out StackItem stackItem, Transform destination,
            Vector3 additionalDelta = new Vector3());
        public bool TryTake(ItemType itemType, out StackItem stackItem, Transform destination, Vector3 additionalDelta = new Vector3(), float progress = 0.0f);

        public bool TrySpend(ItemType type, int amount);

    }
}

