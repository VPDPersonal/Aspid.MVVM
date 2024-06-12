using UnityEngine;
using UltimateUI.MVVM.StatsSamples.Models.Data;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StatsSamples.Models.Races.Data
{
    [CreateAssetMenu(fileName = "New Orc Default Stats", menuName = "UltimateUI/Samples/Heroes/Heroes/Stats/Orc Default Stats", order = 0)]
    public class OrcDefaultStatsData : DefaultStatsData
    {
        [field: Min(0)]
        [field: SerializeField]
        public int Fury { get; private set; }
    }
}