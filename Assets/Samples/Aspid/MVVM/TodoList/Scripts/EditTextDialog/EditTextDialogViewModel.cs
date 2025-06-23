using System;

namespace Aspid.MVVM.TodoList.EditTodoDialogs
{
    [ViewModel]
    public sealed partial class EditTextDialogViewModel
    {
        [TwoWayBind] private string _text;
        [OneTimeBind] private readonly IRelayCommand _cancelCommand;
        [OneTimeBind] private readonly IRelayCommand _renamedCommand;
        
        public EditTextDialogViewModel(string text, Action<string> renamed, Action cancelled = null)
        {
            _text = text;
            _cancelCommand = cancelled.CreateCommandOrEmpty();
            
            _renamedCommand = new RelayCommand(
                execute: () => renamed.Invoke(Text), 
                canExecute: () => Text != text);
        }
        
        partial void OnTextChanged(string newValue) =>
            _renamedCommand?.NotifyCanExecuteChanged();
    }
}