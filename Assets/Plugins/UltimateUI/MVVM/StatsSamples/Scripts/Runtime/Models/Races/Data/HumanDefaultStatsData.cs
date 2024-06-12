using UnityEngine;
using UltimateUI.MVVM.StatsSamples.Models.Data;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StatsSamples.Models.Races.Data
{
    [CreateAssetMenu(fileName = "New Human Default Stats", menuName = "UltimateUI/Samples/Heroes/Heroes/Stats/Human Default Stats", order = 0)]
    public class HumanDefaultStatsData : DefaultStatsData
    {
        [field: Min(0)]
        [field: SerializeField]
        public int Greed { get; private set; }
    }
}