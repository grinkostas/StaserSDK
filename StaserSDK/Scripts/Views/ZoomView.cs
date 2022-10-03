using DG.Tweening;
using StaserSDK.Utilities;
using UnityEngine;
using Zenject;

namespace StaserSDK.Views
{
    public class ZoomView : View
    {
        [SerializeField] private float _duration;
        [Inject] private Timer _timer;
        public override void Show()
        {
            Model.transform.localScale = Vector3.zero;
            base.Show();
            Model.transform.DOScale(Vector3.one, _duration);
        }

        public override void Hide()
        {
            Model.transform.DOScale(Vector3.zero, _duration);
            _timer.ExecuteWithDelay(base.Hide, _duration);
        }
    }
}

