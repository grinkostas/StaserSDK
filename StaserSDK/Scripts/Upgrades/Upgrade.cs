using UnityEngine;

namespace StaserSDK.Upgrades
{
    [CreateAssetMenu]
    public class Upgrade : ScriptableObject
    {
        public string Name;
        public string Id;
        public UpgradeProperty Property;
        public int MaxLevel;

        public float GetValue(int level) => Property.Calculate(Mathf.Clamp(level, 0, MaxLevel));

    }
}

