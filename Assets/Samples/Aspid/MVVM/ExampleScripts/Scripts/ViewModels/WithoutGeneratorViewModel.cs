namespace Aspid.MVVM.ExampleScripts.ViewModels
{
    public class WithoutGeneratorViewModel : IViewModel
    {
        private string _text;
        private TwoWayViewModelEvent<string> _textChangedEvent;

        private string Text
        {
            get => _text;
            set => SetText(value);
        }
        
        public BindResult AddBinder(IBinder binder, string propertyName)
        {
            return propertyName switch
            {
                nameof(Text) => ViewModelUtility.AddBinder(binder, Text, ref _textChangedEvent, SetText),
                _ => default
            };
        }

        private void SetText(string text)
        {
            if (ViewModelUtility.SetProperty(ref _text, text))
            {
                _textChangedEvent?.Invoke(_text);
            }
        }
    }
}