using System;
using System.Collections.Generic;
using UltimateUI.MVVM.StatsSamples.Models.Races.Data;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StatsSamples.Models.Races
{
    public class DwarfStats : Stats<DwarfDefaultStatsData>
    {
        public event Action OreMiningSpeedChanged;
        
        private int _oreMiningSpeed;
        
        public int OreMiningSpeed
        {
            get => _oreMiningSpeed;
            set
            {
                _oreMiningSpeed = value;
                OreMiningSpeedChanged?.Invoke();
            }
        }
        
        public DwarfStats(DwarfDefaultStatsData defaultStatsData) : base(defaultStatsData)
        {
            _oreMiningSpeed = defaultStatsData.OreMiningSpeed;
        }
        
        public override IEnumerator<(string, int)> GetEnumerator()
        {
            yield return (nameof(Hp), Hp);
            yield return (nameof(Power), Power);
            yield return (nameof(Hp), Dexterity);
            yield return (nameof(OreMiningSpeed), OreMiningSpeed);
        }
        
        public override void Reset()
        {
            base.Reset();
            OreMiningSpeed = DefaultStats.OreMiningSpeed;
        }
    }
}