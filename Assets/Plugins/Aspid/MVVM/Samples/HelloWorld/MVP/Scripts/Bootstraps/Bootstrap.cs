using System;
using UnityEngine;

namespace Aspid.MVVM.Samples.HelloWorld.MVP
{
    public sealed class Bootstrap : MonoBehaviour
    {
        [Header("Out View")]
        [SerializeField] private OutViewMVP _outView;

        [Header("Input View")]
        [SerializeField] private ViewInputType _viewInputType;
        [SerializeField] private Transform _inputContent;
        [SerializeField] private InputViewMVP _inputViewPrefab;
        [SerializeField] private MomentInputViewMVP _momentInputViewPrefab;

        private Speaker _speaker;
        private MonoBehaviour _instantiatedInputView;
        
        private OutPresenter _outPresenter;
        private InputPresenter _inputPresenter;
        private MomentInputPresenter _momentInputPresenter;

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
            _outPresenter = new OutPresenter(_speaker, _outView);

        private void BuildInputSpeakerView()
        {
            var view = Instantiate(_inputViewPrefab, _inputContent);
            _instantiatedInputView = view;
            
            _inputPresenter = new InputPresenter(_speaker, view);
        }

        private void BuildMomentInputSpeakerView()
        {
            var view = Instantiate(_momentInputViewPrefab, _inputContent);
            _instantiatedInputView = view;
            
            _momentInputPresenter = new MomentInputPresenter(_speaker, view);
        }

        private void Release()
        {
            _outPresenter?.Dispose();
            _inputPresenter?.Dispose();
            _momentInputPresenter?.Dispose();

            _outPresenter = null;
            _inputPresenter = null;
            _momentInputPresenter = null;
            
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