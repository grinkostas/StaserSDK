using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using StaserSDK.Upgrades;
using Zenject;

namespace StaserSDK.Upgrades
{
    [System.Serializable]
    public class UpgradeValue
    {
        [SerializeField] private Upgrade _upgrade;
        [SerializeField] private UpgradesController _upgradesController;

        public float Value => _upgradesController.GetValue(_upgrade);
        public int ValueInt => (int)_upgradesController.GetValue(_upgrade);

        public UpgradeModel Model => _upgradesController.GetModel(_upgrade);
    }
}
