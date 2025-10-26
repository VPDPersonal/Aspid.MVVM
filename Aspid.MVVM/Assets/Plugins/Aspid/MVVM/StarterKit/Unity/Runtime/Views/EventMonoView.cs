using UnityEngine;
using UnityEngine.Events;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [View]
    [AddComponentMenu("Aspid/MVVM/Views/Event View")]
    public partial class EventMonoView : MonoView
    {
        [SerializeField] private UnityEvent<IViewModel> _initialized;
        [SerializeField] private UnityEvent _deinitialized;

        partial void OnInitializedInternal(IViewModel viewModel) =>
            _initialized?.Invoke(viewModel);

        partial void OnDeinitializedInternal() =>
            _deinitialized?.Invoke();
    }
}