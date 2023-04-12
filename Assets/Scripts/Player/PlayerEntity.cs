using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StatSystem;
using Core.Tools;
using Core.Enums;
using Core.Animation;
using Core.Movement.Data;
using Core.Movement.Controller;
using InputReader;

namespace Player
{

    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerEntity : MonoBehaviour
    {
        [SerializeField] private AnimatorController _animator;
    
        [SerializeField] private DirectionalMovementData _directionMovementData;
        [SerializeField] private JumpData _jumpData;
        [SerializeField] private DirectionalCameraPair _cameras;
        [SerializeField] private GameUIInputView _uiInput;

        private Rigidbody2D _rigidbody;
        private DirectionalMover _directionalMover;
        private Jumper _jumper;
        private PlayerSystem _a;

        public void Start()
        {
            _a = new PlayerSystem(this,new List<IEntityInputSource>{_uiInput, new ExternalDevicesInputReader()});
        }

        public void Initialize()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _directionalMover = new DirectionalMover(_rigidbody, _directionMovementData);
            _jumper = new Jumper(_rigidbody, _jumpData, _directionMovementData.MaxSize);
        }
        
        private void Update()
        {
            if(_jumper.IsJumping)
                _jumper.UpdateJump();
    
            UpdateAnimation();
            UpdateCamera();
        }
    
    
        private void UpdateAnimation()
        {
            _animator.PlayAnimation(AnimationType.Idle, true);
            _animator.PlayAnimation(AnimationType.Go, _directionalMover.IsMoving);
            _animator.PlayAnimation(AnimationType.Jump, _jumper.IsJumping);
        }
    
        public void MoveHorizontally(float direction) => _directionalMover.MoveHorizontally(direction);
        public void MoveVertically(float direction) 
        {
            if(_jumper.IsJumping) 
                return;
    
            _directionalMover.MoveVertically(direction);
        }
    
        public void Jump() => _jumper.Jump();
        
            private void UpdateCamera()
        {
            foreach (var cameraPair in _cameras.DirectionalCameras)
                cameraPair.Value.enabled = cameraPair.Key == _directionalMover.Direction;
        }
     }
}

