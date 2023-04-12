using System;
using System.Collections.Generic;
using System.Linq;
using InputReader;
using StatSystem;
using UnityEngine;

namespace Player
{
    public class PlayerSystem: IDisposable
    {
        private readonly StatController _statController;
        private readonly PlayerEntity _playerEntity;
        private readonly PlayerBrain _playerBrain;
        private readonly List<IDisposable> _disposables;

        public PlayerSystem(PlayerEntity playerEntity, List<IEntityInputSource> inputSources)
        {
            
            Debug.Log("hkuk");
            _disposables = new List<IDisposable>();
            
            
            
            _playerEntity = playerEntity;
            _playerEntity.Initialize();
            
            _playerBrain = new PlayerBrain(_playerEntity, inputSources);
            _disposables.Add(_playerBrain);
        }

        public void Dispose()
        {
            foreach (var disposable in _disposables)
                disposable.Dispose();
        }
    }
}