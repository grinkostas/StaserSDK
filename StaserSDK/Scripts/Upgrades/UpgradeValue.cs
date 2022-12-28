using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using StaserSDK.Upgrades;
using Zenject;

namespace StaserSDK.Upgrades
{
    public class UpgradeValue : MonoBehaviour
    {
        [SerializeField] private Upgrade _upgrade;

        [Inject] private UpgradesController _upgradesController;

        public float Value => _upgradesController.GetValue(_upgrade);
        public int ValueInt => (int)_upgradesController.GetValue(_upgrade);

        public UpgradeModel Model => _upgradesController.GetModel(_upgrade);

        public float GetValue(UpgradesController controller)
        {
            return controller.GetValue(_upgrade);
        }
    }
}
