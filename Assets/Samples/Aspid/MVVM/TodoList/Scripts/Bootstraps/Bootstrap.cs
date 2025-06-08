using UnityEngine;
using Aspid.MVVM.TodoList.Todos.Storages;
using Aspid.MVVM.TodoList.EditTodoDialogs;

namespace Aspid.MVVM.TodoList.Bootstraps
{
    public sealed class Bootstrap : MonoBehaviour
    {
        [Header("Edit Todo Dialog")]
        [SerializeField] private Transform _editTodoDialogViewContainer;
        [SerializeField] private EditTodoDialogView _editTodoDialogViewPrefab;

        [Header("Todos")]
        [SerializeField] private TodoStorageView _todoStorageView;
        [SerializeField] private string[] _todos;

        private TodoStorage _todoStorage;

        private void Awake()
        {
            var editTodoDialog = new EditTodoDialog(_editTodoDialogViewPrefab, _editTodoDialogViewContainer);

            _todoStorage = new TodoStorage();
            foreach (var todo in _todos)
                _todoStorage.AddTodo(todo);
            
            var todoStorageViewModel = new TodoStorageViewModel(_todoStorage, editTodoDialog);
            _todoStorageView.Initialize(todoStorageViewModel);
        }

        private void OnDestroy() => 
            _todoStorageView.DeinitializeView()?.DisposeViewModel();
    }
}