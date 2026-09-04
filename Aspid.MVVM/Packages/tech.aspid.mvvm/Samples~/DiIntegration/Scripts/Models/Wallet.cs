using System;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Samples.DiIntegration
{
    public sealed class Wallet
    {
        public event Action<int> CoinsChanged;

        public int Coins { get; private set; }

        public void Add(int amount)
        {
            Coins += amount;
            CoinsChanged?.Invoke(Coins);
        }
    }
}
