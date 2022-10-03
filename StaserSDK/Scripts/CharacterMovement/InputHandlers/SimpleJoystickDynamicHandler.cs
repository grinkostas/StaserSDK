using UnityEngine;

namespace StaserSDK
{
    public class SimpleJoystickDynamicHandler : InputHandler
    {
        [SerializeField] private DynamicJoystick _dynamicJoystick;

        protected override Vector3 GetInput()
        {
            return new Vector3(_dynamicJoystick.Horizontal, 0, _dynamicJoystick.Vertical);
        }
    }
}

