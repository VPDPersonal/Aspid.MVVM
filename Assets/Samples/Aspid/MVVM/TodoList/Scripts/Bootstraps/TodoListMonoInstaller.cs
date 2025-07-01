using Zenject;
using UnityEngine;

namespace Aspid.MVVM.Samples.TodoList
{
    public sealed class TodoListMonoInstaller : MonoInstaller
    {
        [Header("Edit Todo Dialog")]
        [SerializeField] private Transform _editTodoDialogViewContainer;
        [SerializeField] private EditTextDialogView _editTodoDialogViewPrefab;

        [Header("Todos")]
        [SerializeField] private string[] _todos;

        public override void Start()
        {
            var storage = Container.Resolve<TodoStorage>();
                
            foreach (var todo in _todos)
                storage.Add(todo);
        }

        public override void InstallBindings()
        {
            Container.Bind<EditTextDialog>()
                .AsSingle()
                .WithArguments(_editTodoDialogViewPrefab, _editTodoDialogViewContainer);

            Container.Bind<TodoStorage>().AsSingle();
            Container.Bind<TodoStorageViewModel>().AsSingle();
        }
    }
}