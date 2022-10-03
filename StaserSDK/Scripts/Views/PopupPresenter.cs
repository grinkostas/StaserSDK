using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace StaserSDK.Views
{
    public abstract class PopupPresenter : MonoBehaviour
    {
        [SerializeField] private Popup _popup;

        private void OnEnable()
        {
            _popup.OnShow += OnShow;
        }

        private void OnDisable()
        {
            _popup.OnShow -= OnShow;
        }

        protected abstract void OnShow();
    }

}
