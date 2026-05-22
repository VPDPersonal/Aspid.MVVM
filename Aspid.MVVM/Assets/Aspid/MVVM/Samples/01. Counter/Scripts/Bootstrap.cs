using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Samples.Counter
{
    public sealed class Bootstrap : MonoBehaviour
    {
        [SerializeField] private MonoView _counterView;

        private void Awake()
        {
            var viewModel = new CounterViewModel();
            _counterView.Initialize(viewModel);
        }

        private void OnDestroy() =>
            _counterView.DeinitializeView()?.DisposeViewModel();
    }
}