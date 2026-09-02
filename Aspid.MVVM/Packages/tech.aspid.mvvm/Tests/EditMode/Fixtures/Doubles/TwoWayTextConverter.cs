using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Two-way converter between an int and its decimal text, shared by the composition-converter
    /// tests.
    /// </summary>
    internal sealed class TwoWayTextConverter : ITwoWayConverter<int, string>
    {
        public string Convert(int value) => value.ToString();

        public int ConvertBack(string value) => int.Parse(value);
    }
}
