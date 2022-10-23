using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

namespace StaserSDK.Utilities
{
    public class Rotator : MonoBehaviour
    {
        [SerializeField] private float _rotateAngle;
        [SerializeField] private float _duration;
        [SerializeField] private Vector3 _rotatesAxis;

        private void OnEnable()
        {
            
            transform.DORotate(transform.rotation.eulerAngles + _rotatesAxis*_rotateAngle, _duration);
        }
    }
}
