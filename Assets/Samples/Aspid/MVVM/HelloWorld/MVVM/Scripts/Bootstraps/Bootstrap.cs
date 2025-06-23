using System;
using UnityEngine;

namespace Aspid.MVVM.Samples.HelloWorld.MVVM
{
    public sealed class Bootstrap : MonoBehaviour
    {
        [Header("Views")]
        [SerializeField] private OutSpeakerView _outSpeakerView;
        [SerializeField] private InputSpeakerView _inputSpeakerView;
        
        [Header("ViewModel")]
        [SerializeField] private InputViewModelType _inputViewModelType = InputViewModelType.Command;

        private Speaker _speaker;

        private void OnValidate()
        {
            if (!Application.isPlaying) return;
            if (!_outSpeakerView || !_inputSpeakerView || _speaker is null) return;
            
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
            
            _outSpeakerView.Initialize(viewModel);
            _inputSpeakerView.Initialize(viewModel);
        }
        
        private void DeinitializeViews()
        {
            // You can use extension methods to deinitialize the View and release the ViewModel.
            _outSpeakerView.DeinitializeView()?.DisposeViewModel();
            _inputSpeakerView.DeinitializeView()?.DisposeViewModel();
            
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