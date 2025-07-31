using System;

namespace Aspid.MVVM.Samples.HelloWorld.MVP
{
    public sealed class MomentInputPresenter : IDisposable
    {
        private readonly Speaker _model;
        private readonly MomentInputViewMVP _view;
        
        public MomentInputPresenter(Speaker model, MomentInputViewMVP view)
        {
            _view = view;
            _model = model;
            _view.Text = model.Text;

            Subscribe();
        }

        private void Subscribe() =>
            _view.TextChanged += OnTextChanged;

        private void Unsubscribe() =>
            _view.TextChanged -= OnTextChanged;
        
        private void OnTextChanged(string value) =>
            _model.Say(value);

        public void Dispose() =>
            Unsubscribe();
    }
}