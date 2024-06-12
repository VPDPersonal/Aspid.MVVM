using System;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.Samples.Economic
{
    public class Wallet
    {
        public event Action CoinChanged;
        
        public event Action DiamondChanged;
        
        public int Coins { get; private set; }
        
        public int Diamonds { get; private set; }
        
    }
}