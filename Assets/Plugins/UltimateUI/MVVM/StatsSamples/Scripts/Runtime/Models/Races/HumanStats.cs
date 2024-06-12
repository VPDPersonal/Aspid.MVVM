using System;
using System.Collections.Generic;
using UltimateUI.MVVM.StatsSamples.Models.Races.Data;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StatsSamples.Models.Races
{
    public class HumanStats : Stats<HumanDefaultStatsData>
    {
        public event Action GreedChanged;
        
        private int _greed;
        
        public int Greed
        {
            get => _greed;
            set
            {
                _greed = value;
                GreedChanged?.Invoke();
            }
        }
        
        public HumanStats(HumanDefaultStatsData defaultStatsData) : base(defaultStatsData)
        {
            _greed = defaultStatsData.Greed;
        }
        
        public override IEnumerator<(string, int)> GetEnumerator()
        {
            yield return (nameof(Hp), Hp);
            yield return (nameof(Power), Power);
            yield return (nameof(Hp), Dexterity);
            yield return (nameof(Greed), Greed);
        }
        
        public override void Reset()
        {
            base.Reset();
            Greed = DefaultStats.Greed;
        }
    }
}