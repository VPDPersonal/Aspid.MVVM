using System;
using UnityEngine;

namespace Aspid.MVVM.Samples.HelloWorld.MVP
{
    public sealed class Bootstrap : MonoBehaviour
    {
        [Header("Out View")]
        [SerializeField] private OutSpeakerView _outSpeakerView;

        [Header("Input View")]
        [SerializeField] private ViewInputType _viewInputType;
        [SerializeField] private Transform _inputContent;
        [SerializeField] private InputSpeakerView _inputSpeakerViewPrefab;
        [SerializeField] private MomentInputSpeakerView _momentInputSpeakerViewPrefab;

        private Speaker _speaker;
        private MonoBehaviour _instantiatedInputView;
        
        private OutSpeakerPresenter _outSpeakerPresenter;
        private InputSpeakerPresenter _inputSpeakerPresenter;
        private MomentInputSpeakerPresenter _momentInputSpeakerPresenter;

        private void OnValidate()
        {
            if (_speaker is null) return;
            
            Release();
            Build();
        }

        private void Awake()
        {
            _speaker = new Speaker();
            Build();
        }

        private void Build()
        {
            BuildOutSpeakerView();
            
            switch (_viewInputType)
            {
                case ViewInputType.Command: BuildInputSpeakerView(); break;
                case ViewInputType.Moment: BuildMomentInputSpeakerView(); break;
                default: throw new ArgumentOutOfRangeException();
            }
        }

        private void BuildOutSpeakerView() =>
            _outSpeakerPresenter = new OutSpeakerPresenter(_speaker, _outSpeakerView);

        private void BuildInputSpeakerView()
        {
            var view = Instantiate(_inputSpeakerViewPrefab, _inputContent);
            _instantiatedInputView = view;
            
            _inputSpeakerPresenter = new InputSpeakerPresenter(_speaker, view);
        }

        private void BuildMomentInputSpeakerView()
        {
            var view = Instantiate(_momentInputSpeakerViewPrefab, _inputContent);
            _instantiatedInputView = view;
            
            _momentInputSpeakerPresenter = new MomentInputSpeakerPresenter(_speaker, view);
        }

        private void Release()
        {
            _outSpeakerPresenter?.Dispose();
            _inputSpeakerPresenter?.Dispose();
            _momentInputSpeakerPresenter?.Dispose();

            _outSpeakerPresenter = null;
            _inputSpeakerPresenter = null;
            _momentInputSpeakerPresenter = null;
            
            if (_instantiatedInputView)
                Destroy(_instantiatedInputView.gameObject);
        }
        
        private enum ViewInputType
        {
            Moment,
            Command,
        }
    }
}