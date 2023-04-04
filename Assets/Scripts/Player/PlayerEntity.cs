using Player;
using UnityEngine;

namespace Player
{

    [RequireComponent(typeof(Rigidbody2D))]

    public class PlayerEntity : MonoBehaviour
    {
        [SerializeField] private AnimationControll _animator ;

        [Header("HorizontalMovement")]
        [SerializeField] private float _horizontalSpeed;
        [SerializeField] private bool _faceRight;

        [Header("VerticalMovement")]
        [SerializeField] private float _verticalSpeed;
        [SerializeField] private float _minSize;
        [SerializeField] private float _maxSize;
        [SerializeField] private float _maxVerticalPosition;
        [SerializeField] private float _minVerticalPosition;

        [Header("Jump")]
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _gravityScale;

        private Rigidbody2D _rigidbody;

        private float _sizeModificator;
        private bool _isJumping;
        private float _startJumpVerticalPosition;

        private Vector2 _movement;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();

            float positionDifference = _maxVerticalPosition - _minVerticalPosition;
            float sizeDifference = _maxSize - _minSize;
            _sizeModificator = sizeDifference / positionDifference;
            UpdateSize();
        }
        
        private void Update()
        {
            if (_isJumping)
                UpdateJump();

            UpdateAnimation();
        }
        
        private void UpdateAnimation()
        {
            _animator.PlayAnimation(AnimationType.Idle, true);
            _animator.PlayAnimation(AnimationType.Go, _movement.magnitude > 0);
            _animator.PlayAnimation(AnimationType.Jump, _isJumping);
        }
        
        public void MoveHorizontally(float direction)
        {
            _movement.x = direction; 
            SetDirection(direction);
            Vector2 velocity = _rigidbody.velocity;
            velocity.x = direction * _horizontalSpeed;
            _rigidbody.velocity = velocity;
        }

        public void MoveVertically(float direction)
        {
            if (_isJumping)
                return;

            _movement.y = direction;
            Vector2 velocity = _rigidbody.velocity;
            velocity.y = direction * _verticalSpeed;
            _rigidbody.velocity = velocity;

            if (direction == 0)
                return;

            float verticalPosition = Mathf.Clamp(transform.position.y, _minVerticalPosition, _maxVerticalPosition);
            _rigidbody.position = new Vector2(_rigidbody.position.x, verticalPosition);

            UpdateSize();
        }
        
        public void Jump()
        {
            if (_isJumping)
                return;
            _isJumping = true;
            _rigidbody.AddForce(Vector2.up * _jumpForce);
            _rigidbody.gravityScale = _gravityScale;
            _startJumpVerticalPosition = transform.position.y;
        }

        private void UpdateSize()
        {
            float verticalDelta = _maxVerticalPosition - transform.position.y;
            float currentSizeModificator = _minSize + _sizeModificator * verticalDelta;
            transform.localScale = Vector2.one * currentSizeModificator;
        }

        private void SetDirection(float direction)
        {
            if ((_faceRight && direction < 0) || 
                (!_faceRight && direction > 0))
                Flip();

            float verticalDelta = _maxVerticalPosition - transform.position.y;
        }
        
        private void Flip()
        {
            transform.Rotate(0, 180, 0);
            _faceRight = !_faceRight;
        }
        
        private void UpdateJump()
        {
            if(_rigidbody.velocity.y < 0 && _rigidbody.position.y <= _startJumpVerticalPosition)
            {
                ResetJump();
                return;
            }
        }
       
        private void ResetJump()
        {
            _isJumping = false;
            _rigidbody.position = new Vector2(_rigidbody.position.x, _startJumpVerticalPosition);
            _rigidbody.gravityScale = 0;
        }

       
    }
}

