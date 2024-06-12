using UnityEngine;
using UltimateUI.MVVM.StatsSamples.Models.Data;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StatsSamples.Models.Races.Data
{
    [CreateAssetMenu(fileName = "New Dwarf Default Stats", menuName = "UltimateUI/Samples/Heroes/Heroes/Stats/Dward Default Stats", order = 0)]
    public class DwarfDefaultStatsData : DefaultStatsData
    {
        [field: Min(0)]
        [field: SerializeField]
        public int OreMiningSpeed { get; private set; }
    }
}