namespace StaserSDK
{
    public class MovementSpeedMultiplayer
    {
        public object Sender;
        public float Value;

        public MovementSpeedMultiplayer(object sender, float value)
        {
            Sender = sender;
            Value = value;
        }
    }
}