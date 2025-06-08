using System;
using Aspid.MVVM.TodoList.EditTodoDialogs;
using Aspid.Collections.Observable.Synchronizer;

namespace Aspid.MVVM.TodoList.Todos.Storages
{
    [ViewModel]
    public sealed partial class TodoStorageViewModel : IDisposable
    {
        [TwoWayBind] private string _searchInput;
        [OneTimeBind] private readonly IReadOnlyObservableListSync<TodoItemViewModel> _todoItemViewModels;
        
        private readonly TodoStorage _todoStorage;
        private readonly EditTodoDialog _editTodoDialog;

        public TodoStorageViewModel(TodoStorage todoStorage, EditTodoDialog editTodoDialog)
        {
            _todoStorage = todoStorage;
            _editTodoDialog = editTodoDialog;
            _todoItemViewModels = todoStorage.Todos.CreateSync(CreateTodoViewModel);
        }

        [RelayCommand]
        private void AddTodo() => 
            _todoStorage.AddTodo($"New Todo {_todoStorage.CountAddedTodo + 1}");

        private void SetTodoItemVisible(TodoItemViewModel viewModel) =>
            viewModel.IsVisible = string.IsNullOrEmpty(SearchInput) || viewModel.Todo.Text.Contains((string)SearchInput);
        
        private TodoItemViewModel CreateTodoViewModel(Todo todo)
        {
            var viewModel = new TodoItemViewModel(
                todo,
                new RelayCommand<TodoItemViewModel>(OnTodoItemEdited),
                new RelayCommand<TodoItemViewModel>(OnTodoItemDeleted));

            SetTodoItemVisible(viewModel);
            return viewModel;
        }

        private void OnTodoItemEdited(TodoItemViewModel viewModel)
        {
            _editTodoDialog.Open(viewModel.Todo.Text, text =>
            {
                viewModel.Todo.Text = text;
                SetTodoItemVisible(viewModel);
            });
        }
        
        private void OnTodoItemDeleted(TodoItemViewModel viewModel) =>
            _todoStorage.RemoveTodo(viewModel.Todo.Id);

        partial void OnSearchInputChanged(string newValue)
        {
            foreach (var viewModel in TodoItemViewModels)
                SetTodoItemVisible(viewModel);
        }

        public void Dispose() => 
            TodoItemViewModels?.Dispose();
    }
}