using System;
using System.Collections.Generic;
using UltimateUI.MVVM.StatsSamples.Models.Races.Data;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StatsSamples.Models.Races
{
    public class WizardStats : Stats<WizardDefaultStatsData>
    {
        public event Action ManaChanged;
        
        private int _mana;
        
        public int Mana
        {
            get => _mana;
            set
            {
                _mana = value;
                ManaChanged?.Invoke();
            }
        }
        
        public WizardStats(WizardDefaultStatsData defaultStats) : base(defaultStats)
        {
            Mana = defaultStats.Mana;
        }
        
        public override IEnumerator<(string, int)> GetEnumerator()
        {
            yield return (nameof(Hp), Hp);
            yield return (nameof(Power), Power);
            yield return (nameof(Hp), Dexterity);
            yield return (nameof(Mana), Mana);
        }
        
        public override void Reset()
        {
            base.Reset();
            Mana = DefaultStats.Mana;
        }
    }
}