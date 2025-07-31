using System;

namespace Aspid.MVVM.Samples.HelloWorld.MVP
{
    public sealed class OutPresenter : IDisposable
    {
        private readonly Speaker _model;
        private readonly OutViewMVP _view;

        public OutPresenter(Speaker model, OutViewMVP view)
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