using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Two-way converter that adds a fixed amount, shared by the composition-converter tests.
    /// </summary>
    internal sealed class TwoWayAddConverter : ITwoWayConverter<int, int>
    {
        private readonly int _amount;

        public TwoWayAddConverter(int amount) => _amount = amount;

        public int Convert(int value) => value + _amount;

        public int ConvertBack(int value) => value - _amount;
    }
}
