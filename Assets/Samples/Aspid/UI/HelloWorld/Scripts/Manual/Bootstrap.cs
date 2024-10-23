using System;
using UnityEngine;
using Aspid.UI.MVVM.ViewModels;
using Aspid.UI.HelloWorld.Views;
using Aspid.UI.HelloWorld.Models;
using Aspid.UI.HelloWorld.ViewModels;
using Aspid.UI.MVVM.Views.Extensions;
using Aspid.UI.MVVM.ViewModels.Extensions;

namespace Aspid.UI.HelloWorld.Manual
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
            // TODO Aspid.UI Translate
            // Можно воспользоваться методами расширения, чтобы деинициализировать View и освободить ViewModel.
            _speakerView.DeinitializeView()?.DisposeViewModel();
            
            // TODO Aspid.UI Translate
            // Ручной способ деинициализировать View и освободить ViewModel:
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