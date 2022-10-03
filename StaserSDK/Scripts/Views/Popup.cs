using UnityEngine;
using NepixCore.Game.API;
using StaserSDK.Utilities;
using UnityEngine.Events;
using Zenject;

namespace StaserSDK.Views
{
    public sealed class Popup : MonoBehaviour
    {
        [SerializeField] private GameObject _popupModel;
        [SerializeField] private bool _pauseOnShow = true;

        [Inject] private PauseManager _pauseManager;
        [Inject] private IHapticService _hapticService;

        public UnityAction OnShow;
        
        public void Show()
        {
            OnShow?.Invoke();
            _popupModel.SetActive(true);
            _hapticService.Selection();
            if(_pauseOnShow)
                _pauseManager.Pause(this);
        }

        public void Hide()
        {
            _popupModel.SetActive(false);
            if(_pauseOnShow)
                _pauseManager.Resume(this);
        }
    }
}

