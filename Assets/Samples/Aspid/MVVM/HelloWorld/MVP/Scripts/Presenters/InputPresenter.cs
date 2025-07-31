using System;

namespace Aspid.MVVM.Samples.HelloWorld.MVP
{
    public sealed class InputPresenter : IDisposable
    {
        private readonly Speaker _model;
        private readonly InputViewMVP _view;

        public InputPresenter(Speaker model, InputViewMVP view)
        {
            _view = view;
            _model = model;
            _view.Text = model.Text;
            
            Subscribe();
        }

        private void Subscribe() =>
            _view.Clicked += OnClicked;
        
        private void Unsubscribe() =>
            _view.Clicked -= OnClicked;

        private void OnClicked() =>
            _model.Say(_view.Text);

        public void Dispose() =>
            Unsubscribe();
    }
}