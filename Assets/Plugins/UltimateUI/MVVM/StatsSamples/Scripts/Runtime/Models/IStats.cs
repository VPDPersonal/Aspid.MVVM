using System;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StatsSamples.Models
{
    public interface IStats
    {
        public event Action HpChanged;
        public event Action PowerChanged;
        public event Action DexterityChanged;
        
        public int Hp { get; set; }
        
        public int Power { get; set; }
        
        public int Dexterity { get; set; }
        
        public void Reset();
    }
}