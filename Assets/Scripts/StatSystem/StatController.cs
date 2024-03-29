﻿using System;
using System.Collections.Generic;
using System.Linq;
using Core.Services.Updater;
using StatSystem.Enum;
using UnityEngine;

namespace StatSystem
{
    public class StatController : IDisposable, IStatValueGiver
    {
        private readonly List<Stat> _currentStat;
        private readonly List<StatModificator> _activeModificators;

        public StatController(List<Stat> currentStats)
        {
            _currentStat = currentStats;
            _activeModificators = new List<StatModificator>();
            ProjectUpdater.Instance.UpdateCalled += OnUpdate;
        }

        public float GetStatValue(StatType statType) => _currentStat.Find(stat => stat.Type == statType).Value;

        public void ProcessModificator(StatModificator modificator)
        {
            var statToChange = _currentStat.Find(stat => stat.Type == modificator.Stat.Type);
            
            if(statToChange == null)
                return;

            var addedValue = modificator.Type == StatModificatorType.Additive
                ? statToChange + modificator.Stat
                : statToChange * modificator.Stat;
            
            statToChange.SetStatValue(statToChange + addedValue);
            if(modificator.Duration<0)
                return;

            if (_activeModificators.Contains(modificator))
            {
                _activeModificators.Remove(modificator);
            }
            else
            {
                var addedStat = new Stat(modificator.Stat.Type, -addedValue);
                var tempModificator =
                    new StatModificator(addedStat, StatModificatorType.Additive, modificator.Duration, Time.time);
                _activeModificators.Add(tempModificator);
            }
        }
        
        public void Dispose() => ProjectUpdater.Instance.UpdateCalled -= OnUpdate;

        private void OnUpdate()
        {
            if(_activeModificators.Count == 0)
                return;

            var expireModificator =
                _activeModificators.Where(modificator => modificator.StartTime + modificator.Duration >= Time.time);

            foreach (var modificator in expireModificator)
                ProcessModificator(modificator);
        }
    }
}