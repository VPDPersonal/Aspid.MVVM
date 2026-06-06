using System;
using Aspid.Collections.Observable.Synchronizer;

// ReSharper disable CheckNamespace
// ReSharper disable UnusedParameterInPartialMethod
namespace Aspid.MVVM.Samples.TodoList
{
    [ViewModel]
    public sealed partial class TodoStorageViewModel : IDisposable
    {
        private string _searchInput;

        [TwoWayBind] 
        public string SearchInput
        {
            get => _searchInput;
            private set
            {
                var newValue = string.IsNullOrWhiteSpace(value)
                    ? string.Empty
                    : value;
                
                SetSearchInputField(ref _searchInput, newValue);
            }
        }

        [OneTimeBind] 
        private IReadOnlyObservableListSync<TodoItemViewModel> TodoItemViewModels { get; }
        
        private int _countAddedTodo;
        private readonly TodoStorage _todoStorage;
        private readonly EditTextDialog _editTextDialog;

        public TodoStorageViewModel(TodoStorage todoStorage, EditTextDialog editTodoDialog)
        {
            _todoStorage = todoStorage;
            _editTextDialog = editTodoDialog;
            _countAddedTodo = todoStorage.Todos.Count;
            TodoItemViewModels = todoStorage.Todos.CreateSync(CreateTodoViewModel);
        }
        
        private TodoItemViewModel CreateTodoViewModel(Todo todo)
        {
            var viewModel = new TodoItemViewModel(todo, OnTodoItemEditedCommand, OnTodoItemDeletedCommand);
            SetTodoItemVisible(viewModel);
            
            return viewModel;
        }

        [RelayCommand]
        private void AddTodo()
        {
            _countAddedTodo++;
            _todoStorage.Add($"New Todo {_countAddedTodo}");
        }

        [RelayCommand]
        private void OnTodoItemEdited(TodoItemViewModel viewModel)
        {
            _editTextDialog.Open(viewModel.Todo.Text, text =>
            {
                viewModel.Text = text;
                SetTodoItemVisible(viewModel);
            });
        }
        
        [RelayCommand]
        private void OnTodoItemDeleted(TodoItemViewModel viewModel) =>
            _todoStorage.Remove(viewModel.Todo);

        partial void OnSearchInputChanged(string newValue)
        {
            foreach (var viewModel in TodoItemViewModels)
                SetTodoItemVisible(viewModel);
        }
        
        private void SetTodoItemVisible(TodoItemViewModel viewModel) =>
            viewModel.IsVisible = string.IsNullOrEmpty(SearchInput) || viewModel.Todo.Text.Contains(SearchInput);

        public void Dispose() => 
            TodoItemViewModels?.Dispose();
    }
}