using UnityEngine; 
using Aspid.MVVM.Samples.TodoList.Todos.Storages;
using Aspid.MVVM.Samples.TodoList.EditTodoDialogs;

namespace Aspid.MVVM.Samples.TodoList.Bootstraps
{
    public sealed class Bootstrap : MonoBehaviour
    {
        [Header("Edit Text Dialog")]
        [SerializeField] private Transform _editTodoDialogViewContainer;
        [SerializeField] private EditTextDialogView _editTextDialogViewPrefab;

        [Header("Todos")]
        [SerializeField] private TodoStorageView _todoStorageView;
        [SerializeField] private string[] _todos;

        private TodoStorage _todoStorage;

        private void Awake()
        {
            var editTodoDialog = new EditTextDialog(_editTextDialogViewPrefab, _editTodoDialogViewContainer);

            _todoStorage = new TodoStorage();
            foreach (var todo in _todos)
                _todoStorage.Add(todo);
            
            var todoStorageViewModel = new TodoStorageViewModel(_todoStorage, editTodoDialog);
            _todoStorageView.Initialize(todoStorageViewModel);
        }

        private void OnDestroy() => 
            _todoStorageView.DeinitializeView()?.DisposeViewModel();
    }
}