using UnityEngine;
using Aspid.UI.TodoList.Views;
using Aspid.UI.TodoList.Models;
using Aspid.UI.TodoList.Factories;
using Aspid.UI.TodoList.ViewModels;
using Aspid.UI.MVVM.Views.Extensions;
using Aspid.UI.MVVM.ViewModels.Extensions;

namespace Aspid.UI.TodoList.Manual
{
    public sealed class Bootstrap : MonoBehaviour
    {
        [SerializeField] private TodoItemViewModelFactory.Settings _todoItemViewModelFactorySettings;
        
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
            var todoItemViewModelFactory = new TodoItemViewModelFactory(editTodoPopUpViewFactory, _todoItemViewModelFactorySettings);

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