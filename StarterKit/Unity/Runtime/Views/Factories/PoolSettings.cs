// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    public readonly struct PoolSettings
    {
        public readonly int MaxCount;
        public readonly int InitialCount;

        public PoolSettings(int initialCount, int maxCount = int.MaxValue)
        {
            MaxCount = maxCount;
            InitialCount = initialCount;
        }
    }
}