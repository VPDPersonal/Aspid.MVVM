namespace Aspid.MVVM.StarterKit.Unity
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