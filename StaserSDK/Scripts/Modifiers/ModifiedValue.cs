using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ModifiedValue<T>
{
    [SerializeField] private T StartValue;
    public T Value => GetValue();
    
    private List<ValueModifier<T>> _modifiers = new();

    private T GetValue()
    {
        T result = StartValue;
        
        foreach (var modifier in _modifiers)
            result = modifier.ApplyModifier(result);

        return result;
    }

    public void AddModifier(ValueModifier<T> modifier)
    {
        _modifiers.Add(modifier);
    }

    public void RemoveModifier(ValueModifier<T> modifier)
    {
        _modifiers.Remove(modifier);
    }
}
