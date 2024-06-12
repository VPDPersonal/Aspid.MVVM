using UnityEngine;
using UltimateUI.MVVM.StatsSamples.Models.Data;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StatsSamples.Models.Races.Data
{
    [CreateAssetMenu(fileName = "New Wizard Default Stats", menuName = "UltimateUI/Samples/Heroes/Heroes/Stats/Wizard Default Stats", order = 0)]
    public class WizardDefaultStatsData : DefaultStatsData
    {
        [field: Min(0)]
        [field: SerializeField]
        public int Mana { get; private set; }
    }
}