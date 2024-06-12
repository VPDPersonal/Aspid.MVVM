using System;
using System.Collections.Generic;
using UltimateUI.MVVM.StatsSamples.Models.Races.Data;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StatsSamples.Models.Races
{
    public class OrcStats : Stats<OrcDefaultStatsData>
    {
        public event Action FuryChanged;
        
        private int _fury;
        
        public int Fury
        {
            get => _fury;
            set
            {
                _fury = value;
                FuryChanged?.Invoke();
            }
        }
        
        public OrcStats(OrcDefaultStatsData defaultStatsData) : base(defaultStatsData)
        {
            _fury = defaultStatsData.Fury;
        }
        
        public override IEnumerator<(string, int)> GetEnumerator()
        {
            yield return (nameof(Hp), Hp);
            yield return (nameof(Power), Power);
            yield return (nameof(Hp), Dexterity);
            yield return (nameof(Fury), Fury);
        }
        
        public override void Reset()
        {
            base.Reset();
            Fury = DefaultStats.Fury;
        }
    }
}