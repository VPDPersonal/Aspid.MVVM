using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StatsSamples.Models.Data
{
    public abstract class DefaultStatsData : ScriptableObject
    {
        [field: Min(0)]
        [field: SerializeField]
        public int Hp { get; private set; }
        
        [field: Range(0, 100)]
        [field: SerializeField]
        public int Power { get; private set; }
        
        [field: Range(0, 100)]
        [field: SerializeField]
        public int Dexterity { get; private set; }
    }
    
}