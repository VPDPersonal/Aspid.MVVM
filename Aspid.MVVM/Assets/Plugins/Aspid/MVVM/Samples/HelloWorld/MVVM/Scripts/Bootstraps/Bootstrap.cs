using System;
using UnityEngine;

// ReSharper disable CheckNamespace
namespace Aspid.MVVM.Samples.HelloWorld.MVVM
{
    public sealed class Bootstrap : MonoBehaviour
    {
        [Header("Views")]
        [SerializeField] private OutView _outView; 
        [SerializeField] private InputView _inputView;
        
        [Header("ViewModel")]
        [SerializeField] private InputViewModelType _inputViewModelType = InputViewModelType.Command;

        private Speaker _speaker;

        private void OnValidate()
        {
            if (!Application.isPlaying) return;
            if (!_outView || !_inputView || _speaker is null) return;
            
            DeinitializeViews();
            InitializeViews();
        }

        private void Awake()
        {
            _speaker = new Speaker();
            InitializeViews();
        }

        private void OnDestroy() => 
            DeinitializeViews();

        private void InitializeViews()
        {
            var viewModel = GetViewModel();
            
            _outView.Initialize(viewModel);
            _inputView.Initialize(viewModel);
        }
        
        private void DeinitializeViews()
        {
            // You can use extension methods to deinitialize the View and release the ViewModel.
            _outView.DeinitializeView()?.DisposeViewModel();
            _inputView.DeinitializeView()?.DisposeViewModel();
            
            // Manual way to deinitialize View and release ViewModel:
            // var viewModel = _outSpeakerView.ViewModel;
            // _outSpeakerView.Deinitialize();
            //
            // if (viewModel is IDisposable disposable) 
            //     disposable.Dispose();
        }

        private IViewModel GetViewModel() => _inputViewModelType switch
        {
            InputViewModelType.Command => new SpeakerViewModel(_speaker),
            InputViewModelType.Moment => new MomentSpeakerViewModel(_speaker),
            _ => throw new ArgumentOutOfRangeException()
        };
        
        private enum InputViewModelType
        {
            Moment,
            Command,
        }
    }
}