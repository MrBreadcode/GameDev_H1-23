using System.Collections.Generic;
using UnityEngine;

namespace StatSystem
{
    [CreateAssetMenu(fileName = "StatStorage", menuName = "Stat/StatStorage")]
    public class StatStorage : ScriptableObject
    {
        [field: SerializeField] public List<Stat> Stat { get; private set; }
    }
}