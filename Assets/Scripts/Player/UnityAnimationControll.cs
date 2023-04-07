using System;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Animator))]

    public class UnityAnimationControll : AnimationControll
    {
        private Animator _animator;

        private void Start() => _animator = GetComponent<Animator>();

        protected override void PlayAnimation(AnimationType animationType)
        {
            Debug.Log(animationType);
            _animator.SetInteger(nameof(AnimationType),(int) animationType);
        }
    }
}
