using UnityEngine;

namespace StaserSDK
{
    public class KeyboardInputHandler : InputHandler
    {
        protected override Vector3 GetInput()
        {
            return new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        }
    }
}
