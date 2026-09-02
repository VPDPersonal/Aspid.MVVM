// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// A ViewModel that resolves no bindable members, for tests that only need an <see cref="IViewModel"/> instance.
    /// </summary>
    internal sealed class StubViewModel : IViewModel
    {
        public FindBindableMemberResult FindBindableMember(in FindBindableMemberParameters parameters) => default;
    }
}
