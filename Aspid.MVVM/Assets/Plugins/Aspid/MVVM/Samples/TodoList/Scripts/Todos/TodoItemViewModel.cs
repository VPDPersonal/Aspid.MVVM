// ReSharper disable CheckNamespace
namespace Aspid.MVVM.Samples.TodoList
{
    [ViewModel]
    public sealed partial class TodoItemViewModel
    {
        [Access(Access.Public)]
        [OneWayBind] private bool _isVisible;
                
        [OneTimeBind] private readonly IRelayCommand _editCommand;
        [OneTimeBind] private readonly IRelayCommand _deleteCommand;

        [TwoWayBind]
        public string Text
        {
            get => Todo.Text;
            set
            {
                if (Todo.Text == value) return;
                
                Todo.Text = value;
                OnTextPropertyChanged();
            }
        }
        
        [TwoWayBind]
        public bool IsCompleted
        {
            get => Todo.IsCompleted;
            set
            {
                if (Todo.IsCompleted == value) return;
                
                Todo.IsCompleted = value;
                OnIsCompletedPropertyChanged();
            }
        }
        
        public readonly Todo Todo;
        
        public TodoItemViewModel(
            Todo todo, 
            IRelayCommand<TodoItemViewModel> editCommand = null,
            IRelayCommand<TodoItemViewModel> deleteCommand = null)
        {
            Todo = todo;
            
            _editCommand = editCommand.CreateCommandWithoutParametersOrEmpty(this);
            _deleteCommand =  deleteCommand.CreateCommandWithoutParametersOrEmpty(this);
        }

        partial void OnIsCompletedChanged(bool newValue) =>
            Todo.IsCompleted = newValue;
    }
}