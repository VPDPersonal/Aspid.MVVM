// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Empty concrete subclass of the abstract <see cref="ScriptableView"/>, for tests that need a live instance
    /// of the real base type without any bindable members.
    /// </summary>
    internal sealed class EmptyScriptableView : ScriptableView { }
}
