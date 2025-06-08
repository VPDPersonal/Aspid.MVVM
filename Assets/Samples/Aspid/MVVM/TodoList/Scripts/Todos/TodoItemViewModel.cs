using System;
using UnityEngine;

namespace Aspid.MVVM.TodoList.Todos
{
    [ViewModel]
    public sealed partial class TodoItemViewModel : IDisposable
    {
        [Access(Access.Public)]
        [OneWayBind] private bool _isVisible;
        
        [OneWayBind] private string _text;
        [TwoWayBind] private bool _isCompleted;
        
        public readonly Todo Todo;
        private readonly IRelayCommand<TodoItemViewModel> _editCommand;
        private readonly IRelayCommand<TodoItemViewModel> _deleteCommand;
        
        public TodoItemViewModel(
            Todo todo, 
            IRelayCommand<TodoItemViewModel> editCommand,
            IRelayCommand<TodoItemViewModel> deleteCommand)
        {
            Todo = todo;
            _text = todo.Text;
            _isCompleted = todo.IsCompleted;
            
            _editCommand = editCommand;
            _deleteCommand = deleteCommand;

            Subscribe();
        }

        private void Subscribe()
        {
            Todo.TextChanged += SetText;
            Todo.IsCompletedChanged += SetIsCompleted;
        }

        private void Unsubscribe()
        {
            Todo.TextChanged -= SetText;
            Todo.IsCompletedChanged -= SetIsCompleted;
        }

        [RelayCommand]
        private void Edit()
        {
            if (_editCommand is not null)
                _editCommand.Execute(this);
            else Debug.Log("There is no implementation for edit");
        }

        [RelayCommand]
        private void Delete()
        {
            if (_deleteCommand is not null)
                _deleteCommand.Execute(this);
            else Debug.Log("There is no implementation for edit");
        }

        partial void OnIsCompletedChanged(bool newValue) =>
            Todo.IsCompleted = newValue;

        public void Dispose() =>
            Unsubscribe();
    }
}