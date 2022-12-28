using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;

namespace StaserSDK
{
    public class NavMeshMovementAnimator : NavMeshMovementAnimatorBase
    {
        [SerializeField] private Animator _animator;
        protected override Animator Animator => _animator;
    }
}
