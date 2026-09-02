// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Counts how many items a walked sequence pulled and whether its enumerator was disposed,
    /// shared by the collection-converter tests.
    /// </summary>
    internal sealed class SequencePullProbe
    {
        public int Pulls;
        public bool Disposed;
    }
}
