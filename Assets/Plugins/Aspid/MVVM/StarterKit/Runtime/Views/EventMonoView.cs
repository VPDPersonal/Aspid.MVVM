using UnityEngine;
using Aspid.MVVM.Mono;
using UnityEngine.Events;
using Aspid.MVVM.Generation;

namespace Aspid.MVVM.StarterKit.Views
{
    [View]
    [AddComponentMenu("Aspid/MVVM/Views/Event View")]
    public partial class EventMonoView : MonoView
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