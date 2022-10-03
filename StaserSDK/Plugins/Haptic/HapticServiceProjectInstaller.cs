using System;
using JetBrains.Annotations;
using NepixCore.Game.API;
using NepixCore.Game.Configs.NC;
using UnityEngine;
using Zenject;

#pragma warning disable 0649
namespace NepixCoreModules.NepixCore.Module.Haptic
{
    [CreateAssetMenu(fileName = "HapticServiceProjectInstaller", menuName = CorePaths.NepixCore + "/Haptic Project Installer")]
    public class HapticServiceProjectInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IHapticService>().To<HapticService>().AsSingle().NonLazy();
        }

        #if UNITY_ANDROID
        /// <summary>
        /// A dummy method to force Unity to add vibrate permission.
        /// </summary>
        [UsedImplicitly]
        private void DummyVibrate()
        {
            Handheld.Vibrate();
            throw new Exception("Never call this method!");
        }
        #endif
    }
}