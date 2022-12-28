
public class FloatModifier: ValueModifier<float>
{
    private NumericAction _numericAction;
    private float _valueModifier;
    
    public FloatModifier(float valueModifier, NumericAction action)
    {
        _valueModifier = valueModifier;
        _numericAction = action;
    }
    
    public override float ApplyModifier(float value)
    {
        return _numericAction.Calculate(value, _valueModifier);
    }
}
