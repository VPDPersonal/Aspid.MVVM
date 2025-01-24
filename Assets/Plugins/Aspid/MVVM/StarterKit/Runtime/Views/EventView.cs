using UnityEngine;
using Aspid.MVVM.Mono;
using UnityEngine.Events;
using Aspid.MVVM.Generation;

namespace Aspid.MVVM.StarterKit.Views
{
    [View]
    public partial class EventView : MonoView
    {
        [Header("Events")]
        [SerializeField] private UnityEvent<IViewModel> _initialized;
        [SerializeField] private UnityEvent _deinitialized;

        partial void OnInitializedInternal(IViewModel viewModel) =>
            _initialized?.Invoke(viewModel);

        partial void OnDeinitializedInternal() =>
            _deinitialized?.Invoke();
    }
}