using System;

// ReSharper disable CheckNamespace
// ReSharper disable UnusedParameterInPartialMethod
namespace Aspid.MVVM.Samples.TodoList
{
    [ViewModel]
    public sealed partial class EditTextDialogViewModel
    {
        [TwoWayBind] private string _text;
        [OneTimeBind] private readonly IRelayCommand _cancelCommand;
        [OneTimeBind] private readonly IRelayCommand _renamedCommand;
        
        public EditTextDialogViewModel(string text, Action<string> renamed, Action cancelled)
        {
            _text = text;
            _cancelCommand = new RelayCommand(cancelled);
            
            _renamedCommand = new RelayCommand(
                execute: () => renamed.Invoke(Text), 
                canExecute: () => Text != text);
        }
        
        // Re-evaluate CanExecute so the Rename button is enabled only when the text actually changed.
        partial void OnTextChanged(string newValue) =>
            _renamedCommand.NotifyCanExecuteChanged();
    }
}