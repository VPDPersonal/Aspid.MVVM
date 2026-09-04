using UnityEngine;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Samples.DynamicViewModel
{
    // No [ViewModel] class and no [View] class: properties are declared at runtime by id,
    // and the scene uses a plain MonoView whose binders are listed in the Inspector.
    public sealed class StopwatchBootstrap : MonoBehaviour
    {
        [SerializeField] private MonoView _view;

        private bool _isRunning;
        private float _seconds;
        private StarterKit.DynamicViewModel _viewModel;
        private IDynamicProperty<string> _elapsed;
        private IDynamicProperty<int> _laps;

        private void Awake()
        {
            _viewModel = new StarterKit.DynamicViewModel();

            _viewModel.Add("Title", "Stopwatch", BindMode.OneTime);
            _elapsed = _viewModel.Add("Elapsed", Format(0f));
            _laps = _viewModel.Add("Laps", 0);

            // Commands are values too; a OneTime property is enough for a read-only member.
            _viewModel.Add<IRelayCommand>("StartStopCommand", new RelayCommand(() => _isRunning = !_isRunning), BindMode.OneTime);
            _viewModel.Add<IRelayCommand>("LapCommand", new RelayCommand(() => _laps.Value++), BindMode.OneTime);
            _viewModel.Add<IRelayCommand>("ResetCommand", new RelayCommand(ResetStopwatch), BindMode.OneTime);

            _view.Initialize(_viewModel);
        }

        private void Update()
        {
            if (!_isRunning) return;

            _seconds += Time.deltaTime;
            _elapsed.Value = Format(_seconds);
        }

        private void OnDestroy() =>
            _view.Deinitialize();

        private void ResetStopwatch()
        {
            _isRunning = false;
            _seconds = 0f;
            _elapsed.Value = Format(0f);
            _laps.Value = 0;
        }

        private static string Format(float seconds) =>
            $"{(int)seconds / 60:0}:{seconds % 60:00.0}";
    }
}
