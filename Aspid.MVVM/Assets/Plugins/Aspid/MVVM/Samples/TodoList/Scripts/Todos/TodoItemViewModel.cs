using System;

// ReSharper disable CheckNamespace
namespace Aspid.MVVM.Samples.TodoList
{
    [ViewModel]
    public sealed partial class TodoItemViewModel : IDisposable
    {
        [Access(Access.Public)]
        [OneWayBind] private bool _isVisible;
        
        [TwoWayBind] private string _text;
        [TwoWayBind] private bool _isCompleted;
        
        [OneTimeBind] private readonly IRelayCommand _editCommand;
        [OneTimeBind] private readonly IRelayCommand _deleteCommand;
        
        public readonly Todo Todo;
        
        public TodoItemViewModel(
            Todo todo, 
            IRelayCommand<TodoItemViewModel> editCommand = null,
            IRelayCommand<TodoItemViewModel> deleteCommand = null)
        {
            Todo = todo;
            _text = todo.Text;
            _isCompleted = todo.IsCompleted;
            
            _editCommand = editCommand.CreateCommandWithoutParametersOrEmpty(this);
            _deleteCommand =  deleteCommand.CreateCommandWithoutParametersOrEmpty(this);
            
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

        partial void OnIsCompletedChanged(bool newValue) =>
            Todo.IsCompleted = newValue;

        public void Dispose() =>
            Unsubscribe();
    }
}