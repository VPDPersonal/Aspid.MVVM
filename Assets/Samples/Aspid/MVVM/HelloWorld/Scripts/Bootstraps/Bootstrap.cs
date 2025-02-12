using System;
using UnityEngine;
using Aspid.MVVM.HelloWorld.Views;
using Aspid.MVVM.HelloWorld.Models;
using Aspid.MVVM.HelloWorld.ViewModels;

namespace Aspid.MVVM.HelloWorld
{
    public sealed class Bootstrap : MonoBehaviour
    {
        [Header("Views")]
        [SerializeField] private SpeakerView _speakerView;
        [SerializeField] private InputSpeakerView _inputSpeakerView;
        
        [Header("ViewModel")]
        [SerializeField] private InputViewModelType _inputViewModelType;

        private Speaker _speaker;

        private void OnValidate()
        {
            if (!Application.isPlaying) return;
            if (!_speakerView || _speaker is null) return;
            
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
            var inputViewModel = GetInputViewModel();
            _inputSpeakerView.Initialize(inputViewModel);

            var viewModel = GetViewModel();
            _speakerView.Initialize(viewModel);
        }
        
        private void DeinitializeViews()
        {
            // Вы можете использовать методы расширения для деинициализации View и освобождения ViewModel.
            _speakerView.DeinitializeView()?.DisposeViewModel();
            _inputSpeakerView.DeinitializeView()?.DisposeViewModel();
            
            // Ручной способ деинициализации View и освобождения ViewModel:
            // var viewModel = _speakerView.ViewModel;
            // _speakerView.Deinitialize();
            //
            // if (viewModel is IDisposable disposable) 
            //     disposable.Dispose();
        }
        
        private IViewModel GetViewModel() =>
            new SpeakerViewModel(_speaker);

        private IViewModel GetInputViewModel() => _inputViewModelType switch
        {
            InputViewModelType.Command => new InputSpeakerViewModel(_speaker),
            InputViewModelType.Moment => new MomentInputSpeakerViewModel(_speaker),
            _ => throw new ArgumentOutOfRangeException()
        };
        
        private enum InputViewModelType
        {
            Moment,
            Command,
        }
    }
}