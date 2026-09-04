using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Samples.Counter
{
    // Manual wiring. The "Counter (ViewInitializer)" scene does the same with a component and no code.
    public sealed class Bootstrap : MonoBehaviour
    {
        [SerializeField] private CounterView _counterView;

        private void Awake() =>
            _counterView.Initialize(new CounterViewModel());

        private void OnDestroy() =>
            _counterView.DeinitializeView()?.DisposeViewModel();
    }
}
