using UnityEngine;
using Zenject;

namespace StaserSDK.Upgrades
{
    public class UpgradesInstaller : MonoInstaller
    {
        [SerializeField] private UpgradesController _upgradesController;
        public override void InstallBindings()
        {
            Container.Bind<UpgradesController>().FromInstance(_upgradesController).NonLazy();
        }
    }
}
