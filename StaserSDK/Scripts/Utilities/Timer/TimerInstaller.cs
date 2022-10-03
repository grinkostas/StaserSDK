using UnityEngine;
using StaserSDK;
using Zenject;

namespace StaserSDK.Utilities
{
    public class TimerInstaller : MonoInstaller
    {
        [SerializeField] private Timer _timer;
        public override void InstallBindings()
        {
            Container.Bind<Timer>().FromInstance(_timer);
        }
    } 
}

