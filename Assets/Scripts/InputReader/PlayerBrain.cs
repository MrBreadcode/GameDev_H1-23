using System.Collections.Generic;
using System.Linq;
using Player;

namespace InputReader
{
    public class PlayerBrain
    {
        private readonly PlayerEntity _playerEntity;
        private readonly List<IEntityInputSource> _inputSources;

        public PlayerBrain(PlayerEntity playerEntity, List<IEntityInputSource> inputSources)
        {
            _playerEntity = playerEntity;
            _inputSources = inputSources;
        }

        public void OnFixedUpdate()
        {
            _playerEntity.MoveHorizontally(GetHorizontalDirection());
            _playerEntity.MoveVertically(GetVerticalDirection());
            if(IsJump)
                _playerEntity.Jump();

            foreach (var inputSource in _inputSources)
                inputSource.ResetOneTimeActions();
        }

        private float GetHorizontalDirection()
        {
            foreach (var inputSource in _inputSources)
            {
                if(inputSource.HorizontalDirection == 0)
                    continue;
                return inputSource.HorizontalDirection;
            }

            return 0;
        }
        
        private float GetVerticalDirection()
        {
            foreach (var inputSource in _inputSources)
            {
                if(inputSource.VerticalDirection == 0)
                    continue;
                return inputSource.VerticalDirection;
            }

            return 0;
        }

        private bool IsJump => _inputSources.Any(source => source.Jump);
    }
}