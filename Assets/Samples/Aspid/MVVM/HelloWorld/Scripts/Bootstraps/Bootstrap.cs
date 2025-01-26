using System;
using UnityEngine;
using Aspid.MVVM.HelloWorld.Views;
using Aspid.MVVM.HelloWorld.Models;
using Aspid.MVVM.HelloWorld.ViewModels;

namespace Aspid.MVVM.HelloWorld
{
    public sealed class Bootstrap : MonoBehaviour
    {
        [SerializeField] private ViewModelType _viewModelType;
        [SerializeField] private SpeakerView _speakerView;

        private Speaker _speaker;

        private void OnValidate()
        {
            if (!Application.isPlaying) return;
            if (!_speakerView || _speaker == null) return;
            
            DeinitializeView();
            InitializeView();
        }

        private void Awake()
        {
            _speaker = new Speaker();
            InitializeView();
        }

        private void OnDestroy() => DeinitializeView();

        private void InitializeView()
        {
            var viewModel = GetViewModel(_speaker);
            _speakerView.Initialize(viewModel);
        }
        
        private void DeinitializeView()
        {
            // You can use extension methods to deinitialize the View and release the ViewModel.
            _speakerView.DeinitializeView()?.DisposeViewModel();
            
            // Manual way to deinitialize View and release ViewModel:
            //
            // var viewModel = _speakerView.ViewModel;
            // _speakerView.Deinitialize();
            //
            // if (viewModel is IDisposable disposable) 
            // {
            //     disposable.Dispose();
            // }
        }

        private IViewModel GetViewModel(Speaker speaker) => _viewModelType switch
        {
            ViewModelType.Moment => new MomentSpeakerViewModel(speaker),
            ViewModelType.Command1 => new CommandSpeakerViewModel1(speaker),
            ViewModelType.Command2 => new CommandSpeakerViewModel2(speaker),
            _ => throw new ArgumentOutOfRangeException()
        };
        
        private enum ViewModelType
        {
            Moment,
            Command1,
            Command2,
        }
    }
}