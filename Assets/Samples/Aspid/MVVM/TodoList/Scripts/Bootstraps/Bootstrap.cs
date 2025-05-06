using UnityEngine;
using Aspid.MVVM.TodoList.Views;
using Aspid.MVVM.TodoList.Models;
using Aspid.MVVM.TodoList.Factories;
using Aspid.MVVM.TodoList.ViewModels;

namespace Aspid.MVVM.TodoList
{
    public sealed class Bootstrap : MonoBehaviour
    {
        [Header("Edit Todo PopUp View Factory")]
        [SerializeField] private Transform _editTodoPopUpViewContainer;
        [SerializeField] private EditTodoPopUpView _editTodoPopUpViewPrefab;

        [Header("Todo Storage")]
        [SerializeField] private TodoStorageView _todoStorageView;
        
        [Header("Todos")]
        [SerializeField] private string[] _todos;

        private TodoStorage _todoStorage;

        private void Awake()
        {
            var editTodoPopUpViewFactory = new EditTodoPopUpViewFactory(_editTodoPopUpViewPrefab, _editTodoPopUpViewContainer);
            var todoItemViewModelFactory = new TodoItemViewModelFactory(editTodoPopUpViewFactory);

            _todoStorage = new TodoStorage();
            foreach (var todo in _todos)
                _todoStorage.AddTodo(todo);
            
            var todoStorageViewModel = new TodoStorageViewModel(_todoStorage, todoItemViewModelFactory);
            _todoStorageView.Initialize(todoStorageViewModel);
        }

        private void OnDestroy()
        {
            _todoStorageView.DeinitializeView()?.DisposeViewModel();
        }
    }
}