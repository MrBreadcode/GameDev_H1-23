using UnityEngine;

namespace Player
{
    public abstract class AnimationControll : MonoBehaviour
    {
        private AnimationType _currentAnimationType;

        public void PlayAnimation(AnimationType animationType, bool active)
        {
            if (!active)
            {
                if (_currentAnimationType == AnimationType.Idle || _currentAnimationType != animationType)
                    return;
                _currentAnimationType = AnimationType.Idle;
                PlayAnimation(_currentAnimationType);
                return;

            }

            if (_currentAnimationType >= animationType)
                return;

            _currentAnimationType = animationType;
            PlayAnimation(_currentAnimationType);
        }

        protected abstract void PlayAnimation(AnimationType animationType);
    }
}

