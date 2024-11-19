using Aspid.UI.MVVM;
using Aspid.UI.MVVM.ViewModels;
using Aspid.UI.MVVM.ViewModels.Generation;

namespace Aspid.UI.ExampleScripts.ViewModels
{
    [ViewModel]
    public partial class HybridViewModel
    {
        [Bind] private int _number;
        
        private string _text;
        private ViewModelEvent<string> _textChangedEvent;

        private string Text
        {
            get => _text;
            set => SetText(value);
        }
        
        private void SetText(string text)
        {
            if (ViewModelUtility.SetProperty(ref _text, text))
            {
                _textChangedEvent?.Invoke(_text);
            }
        }

        partial void AddBinderManual(IBinder binder, string propertyName, ref IRemoveBinderFromViewModel removeBinder)
        {
            removeBinder = propertyName switch
            {
                nameof(Text) => ViewModelUtility.AddBinder(binder, Text, ref _textChangedEvent, SetText),
                _ => removeBinder
            };
        }
    }
}