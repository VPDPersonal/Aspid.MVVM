using System;

namespace Aspid.UI.MVVM.ViewModels
{
    public class MyNewViewModel
    {
        public event Action<string> TextChanged
        {
            add
            {
                _textChangedEvent ??= new ViewModelEvent<string>();
                _textChangedEvent.Changed += value;   
            }
            remove
            {
                if (_textChangedEvent is null) return;
                _textChangedEvent.Changed -= value;
            }
        }
        
        private string _text;
        private ViewModelEvent<string>? _textChangedEvent;
        
        public string Text
        {
            get => _text;
            set => SetText(value);
        }

        public void SetText(string text)
        {
            _text = text;
            _textChangedEvent?.Invoke(text);
        }
        
        public IRemoveBinderFromViewModel AddBinder(IBinder binder, string propertyName)
        {
            switch (propertyName)
            {
                case "Text":
                    _textChangedEvent ??= new ViewModelEvent<string>();
                    _textChangedEvent.SetValue ??= binder.IsReverseEnabled ? SetText : null;
                    return  _textChangedEvent.AddBinder(binder, _text);;
            }
            
            return null;
        }
    }

    public class MyBinder : IBinder
    {
        private IRemoveBinderFromViewModel? _removeBinder;

        public void Bind(MyNewViewModel viewModel, string id)
        {
             _removeBinder = viewModel.AddBinder(this, id);
        }

        public void Unbind()
        {
            if (_removeBinder is null) return;
            
            _removeBinder.RemoveBinder(this);
            _removeBinder = null;
        }

        public void Bind(IViewModel viewModel, string id) { }

        public void Unbind(IViewModel viewModel, string id) { }
    }
}