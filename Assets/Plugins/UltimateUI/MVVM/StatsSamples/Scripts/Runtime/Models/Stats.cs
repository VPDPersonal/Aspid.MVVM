using System;
using System.Collections;
using System.Collections.Generic;
using UltimateUI.MVVM.StatsSamples.Models.Data;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StatsSamples.Models
{
    
    public abstract class Stats<T> : IStats, IEnumerable<(string, int)>
        where T : DefaultStatsData
    {
        public event Action HpChanged;
        public event Action PowerChanged;
        public event Action DexterityChanged;
        
        private int _hp;
        private int _power;
        private int _dexterity;
        
        protected readonly T DefaultStats;
        
        public int Hp
        {
            get => _hp;
            set
            {
                _hp = value;
                HpChanged?.Invoke();
            }
        }
        
        public int Power
        {
            get => _power;
            set
            {
                _power = value;
                PowerChanged?.Invoke();
            }
        }
        
        public int Dexterity
        {
            get => _dexterity;
            set
            {
                _dexterity = value;
                DexterityChanged?.Invoke();
            }
        }
        
        protected Stats(T defaultStats)
        {
            DefaultStats = defaultStats;
            
            _hp = defaultStats.Hp;
            _power = defaultStats.Power;
            _dexterity = defaultStats.Dexterity;
        }
        
        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();
        
        public virtual IEnumerator<(string, int)> GetEnumerator()
        {
            yield return (nameof(Hp), Hp);
            yield return (nameof(Power), Power);
            yield return (nameof(Hp), Dexterity);
        }
        
        public virtual void Reset()
        {
            _hp = DefaultStats.Hp;
            _power = DefaultStats.Power;
            _dexterity = DefaultStats.Dexterity;
        }
    }
}