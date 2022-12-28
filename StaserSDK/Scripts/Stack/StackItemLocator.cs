using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes.Test;
using StaserSDK;
using StaserSDK.Stack;
using Zenject;

public class StackItemLocator : MonoBehaviour
{
    [SerializeField] private StackProvider _stackProvider;
    [SerializeField] private Vector2Int _size;
    [SerializeField] private ItemType _storedItemType;

    [Inject] private ResourceController _resourceController;
    
    public Vector3 GetCurrentDelta()
    {
        return GetDeltaByIndex(_stackProvider.Interface.ItemsCount);
    }

    public Vector3 GetNextDelta(int delta)
    {
        return GetDeltaByIndex(_stackProvider.Interface.ItemsCount + delta);
    }
    
    public void Rebuild()
    {
        int index = 0;
        foreach (var item in _stackProvider.Interface.Items)
        {
            item.transform.localPosition = GetDeltaByIndex(index);
            index++;
        }
    }

    private Vector3 GetDeltaByIndex(int count)
    {
        Vector3 size = _resourceController.GetPrefab(_storedItemType).StackSize.Size;
        Vector3Int index = Vector3Int.zero;
        int area = _size.x * _size.y;
        index.y = count / area;
        int xz = count - index.y * area;
        index.x = xz / _size.x;
        index.z = xz - _size.x * index.x;

        return Vector3.Scale(index, size);
    }
}
