using System;
using System.Collections.Generic;
using UltimateUI.MVVM.StatsSamples.Models.Races.Data;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StatsSamples.Models.Races
{
    public class HobbitStats : Stats<HobbitDefaultStatsData>
    {
        public event Action CalmChanged;
        
        private int _calm;
        
        public int Calm
        {
            get => _calm;
            set
            {
                _calm = value;
                CalmChanged?.Invoke();
            }
        }
        
        public HobbitStats(HobbitDefaultStatsData defaultStatsData) : base(defaultStatsData)
        {
            _calm = defaultStatsData.Calm;
        }
        
        public override IEnumerator<(string, int)> GetEnumerator()
        {
            yield return (nameof(Hp), Hp);
            yield return (nameof(Power), Power);
            yield return (nameof(Hp), Dexterity);
            yield return (nameof(Calm), Calm);
        }
        
        public override void Reset()
        {
            base.Reset();
            Calm = DefaultStats.Calm;
        }
    }
}