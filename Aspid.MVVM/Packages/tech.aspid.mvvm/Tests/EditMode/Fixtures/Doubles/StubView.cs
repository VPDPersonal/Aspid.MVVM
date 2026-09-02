using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Tests
{
    /// <summary>
    /// Minimal <see cref="IView"/> so a test exercises a binder's own logic rather than MonoView's
    /// initialization requirements.
    /// </summary>
    internal sealed class StubView : MonoBehaviour, IView
    {
        public IViewModel ViewModel { get; private set; }

        public void Initialize(IViewModel viewModel) => ViewModel = viewModel;

        public void Deinitialize() => ViewModel = null;
    }
}
