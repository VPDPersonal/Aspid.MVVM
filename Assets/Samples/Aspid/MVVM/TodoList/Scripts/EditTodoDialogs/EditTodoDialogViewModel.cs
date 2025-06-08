using System;
using UnityEngine;

namespace Aspid.MVVM.TodoList.EditTodoDialogs
{
    [ViewModel]
    public sealed partial class EditTodoDialogViewModel
    {
        [TwoWayBind] private string _text;
        [OneTimeBind] private readonly IRelayCommand _cancelCommand;
        [OneTimeBind] private readonly IRelayCommand _renamedCommand;
        
        public EditTodoDialogViewModel(string text, Action<string> renamed, Action cancelled = null)
        {
            _text = text;
            _cancelCommand = new RelayCommand(cancelled ?? DefaultCancel);
            _renamedCommand = new RelayCommand(() => renamed.Invoke(Text), () => Text != text);
        }

        private static void DefaultCancel() =>
            Debug.Log("There is no implementation for cancellation");

        partial void OnTextChanged(string newValue) =>
            _renamedCommand?.NotifyCanExecuteChanged();
    }
}