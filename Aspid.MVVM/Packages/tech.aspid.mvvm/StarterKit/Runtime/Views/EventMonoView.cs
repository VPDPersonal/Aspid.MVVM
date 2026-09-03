using UnityEngine;
using UnityEngine.Events;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="MonoView"/> that raises <see cref="UnityEvent"/>s when it is initialized and deinitialized.
    /// </summary>
    [View]
    [ShowDesignViewModel]
    [AddComponentMenu("Aspid/MVVM/Views/Event View")]
    public partial class EventMonoView : MonoView
    {
        [Tooltip("Raised after the view is initialized with a ViewModel.")]
        [SerializeField] private UnityEvent<IViewModel> _initialized;

        [Tooltip("Raised after the view is deinitialized.")]
        [SerializeField] private UnityEvent _deinitialized;

        partial void OnInitializedInternal(IViewModel viewModel) =>
            _initialized?.Invoke(viewModel);

        partial void OnDeinitializedInternal() =>
            _deinitialized?.Invoke();
    }
}
