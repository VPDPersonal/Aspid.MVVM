// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// One-way converter that adds a fixed amount, shared by the composition-converter tests.
    /// </summary>
    internal sealed class AddConverter : IConverter<int, int>
    {
        private readonly int _amount;

        public AddConverter(int amount) => _amount = amount;

        public int Convert(int value) => value + _amount;
    }
}
