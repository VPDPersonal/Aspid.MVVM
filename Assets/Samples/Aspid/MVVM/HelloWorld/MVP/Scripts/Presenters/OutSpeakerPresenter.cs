using System;

namespace Aspid.MVVM.Samples.HelloWorld.MVP
{
    public sealed class OutSpeakerPresenter : IDisposable
    {
        private readonly Speaker _model;
        private readonly OutSpeakerView _view;

        public OutSpeakerPresenter(Speaker model, OutSpeakerView view)
        {
            _view = view;
            _model = model;
            _view.Text = _model.Text;

            Subscribe();
        }

        private void Subscribe() =>
            _model.TextChanged += OnTextChanged;

        private void Unsubscribe() =>
            _model.TextChanged -= OnTextChanged;
        
        private void OnTextChanged(string value) =>
            _view.Text = value;

        public void Dispose() =>
            Unsubscribe();
    }
}