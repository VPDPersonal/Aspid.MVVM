using UnityEngine;
using Aspid.MVVM.Unity;
using UnityEngine.Events;

namespace Aspid.MVVM.StarterKit.Unity
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