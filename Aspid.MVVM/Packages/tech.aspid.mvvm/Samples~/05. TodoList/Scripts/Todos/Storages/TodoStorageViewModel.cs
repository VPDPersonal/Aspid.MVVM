using System;
using Aspid.Collections.Observable.Synchronizer;

// ReSharper disable CheckNamespace
// ReSharper disable UnusedParameterInPartialMethod
namespace Aspid.MVVM.Samples.TodoList
{
    [ViewModel]
    public sealed partial class TodoStorageViewModel : IDisposable
    {
        [TwoWayBind] private string _searchInput = string.Empty;

        // The list of child ViewModels is kept in sync with the model collection by CreateSync.
        [OneTimeBind] private readonly IReadOnlyObservableListSync<TodoItemViewModel> _todoItemViewModels;

        private int _countAddedTodo;
        private readonly TodoStorage _todoStorage;
        private readonly EditTextDialog _editTextDialog;

        public TodoStorageViewModel(TodoStorage todoStorage, EditTextDialog editTodoDialog)
        {
            _todoStorage = todoStorage;
            _editTextDialog = editTodoDialog;
            _countAddedTodo = todoStorage.Todos.Count;
            _todoItemViewModels = todoStorage.Todos.CreateSync(CreateTodoViewModel);
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
            viewModel.IsVisible = string.IsNullOrWhiteSpace(SearchInput) || viewModel.Todo.Text.Contains(SearchInput);

        public void Dispose() =>
            _todoItemViewModels.Dispose();
    }
}