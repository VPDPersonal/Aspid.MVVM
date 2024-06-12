using UnityEngine;
using UltimateUI.MVVM.StatsSamples.Models.Data;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StatsSamples.Models.Races.Data
{
    [CreateAssetMenu(fileName = "New Hobbit Default Stats", menuName = "UltimateUI/Samples/Heroes/Heroes/Stats/Hobbit Default Stats", order = 0)]
    public class HobbitDefaultStatsData : DefaultStatsData
    {
        [field: Min(0)]
        [field: SerializeField]
        public int Calm { get; private set; }
    }
}