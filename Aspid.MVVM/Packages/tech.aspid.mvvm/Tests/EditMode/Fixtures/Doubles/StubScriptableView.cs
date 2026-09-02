using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Minimal <see cref="IView"/> ScriptableObject, for tests that only need a scriptable <see cref="IView"/> instance.
    /// </summary>
    internal sealed class StubScriptableView : ScriptableObject, IView
    {
        public IViewModel ViewModel { get; private set; }

        public void Initialize(IViewModel viewModel) => ViewModel = viewModel;

        public void Deinitialize() => ViewModel = null;
    }
}
