using System;
using Aspid.MVVM.TodoList.Models;
using Aspid.Collections.Observable;
using Aspid.MVVM.TodoList.Factories;

namespace Aspid.MVVM.TodoList.ViewModels
{
    [ViewModel]
    public partial class TodoStorageViewModel : IDisposable
    {
        [Bind] private string _searchInput;
        
        // ObservableList is a special collection that allows you to keep track of changes to the collection
        [Bind] private readonly ObservableList<ITodoItemViewModel> _todoItemViewModels;
        
        private readonly TodoStorage _todoStorage;
        private readonly TodoItemViewModelFactory _todoItemViewModelFactory;

        public TodoStorageViewModel(TodoStorage todoStorage, TodoItemViewModelFactory todoItemViewModelFactory)
        {
            _todoStorage = todoStorage;
            _todoItemViewModelFactory = todoItemViewModelFactory;
            _todoItemViewModels = new ObservableList<ITodoItemViewModel>();

            foreach (var todo in _todoStorage.Todos)
                OnTodoAdded(todo);
            
            _todoStorage.TodoAdded += OnTodoAdded;
            _todoStorage.TodoRemoved += OnTodoRemoved;
        }

        [RelayCommand]
        private void AddTodo() => 
            _todoStorage.AddTodo($"New Todo {_todoStorage.CountAddedTodo + 1}");

        private void SetTodoItemVisible(ITodoItemViewModel viewModel) =>
            viewModel.IsVisible = string.IsNullOrEmpty(SearchInput) || viewModel.Todo.Text.Contains(SearchInput);
        
        private void OnTodoAdded(Todo todo)
        {
            var viewModel = _todoItemViewModelFactory.Create(todo);
            viewModel.Edited += OnTodoItemEdited;
            viewModel.Deleted += OnTodoItemDeleted;

            SetTodoItemVisible(viewModel);
            TodoItemViewModels.Add(viewModel);
        }
        
        private void OnTodoRemoved(Todo todo)
        {
            for (var i = 0; i < TodoItemViewModels.Count; i++)
            {
                var viewModel = TodoItemViewModels[i];
                if (todo.Id != viewModel.Todo.Id) continue;
                
                viewModel.Edited -= OnTodoItemEdited;
                viewModel.Deleted -= OnTodoItemDeleted;
                TodoItemViewModels.RemoveAt(i);
                break;
            }
        }

        private void OnTodoItemDeleted(ITodoItemViewModel todo) =>
            _todoStorage.RemoveTodo(todo.Todo.Id);

        private void OnTodoItemEdited(ITodoItemViewModel todo) =>
            SetTodoItemVisible(todo);

        partial void OnSearchInputChanged(string newValue) =>
            TodoItemViewModels.ForEach(SetTodoItemVisible);

        public void Dispose()
        {
            if (_todoStorage != null)
            {
                _todoStorage.TodoAdded -= OnTodoAdded;
                _todoStorage.TodoRemoved -= OnTodoRemoved;
            }
        }
    }
}