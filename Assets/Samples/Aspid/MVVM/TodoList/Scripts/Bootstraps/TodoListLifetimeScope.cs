using VContainer;
using UnityEngine;
using VContainer.Unity;

namespace Aspid.MVVM.Samples.TodoList
{
    public sealed class TodoListLifetimeScope : LifetimeScope
    {
        [Header("Edit Todo Dialog")]
        [SerializeField] private Transform _editTodoDialogViewContainer;
        [SerializeField] private EditTextDialogView _editTodoDialogViewPrefab;

        [Header("Todos")]
        [SerializeField] private string[] _todos;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<EditTextDialog>(Lifetime.Singleton)
                .WithParameter(_editTodoDialogViewPrefab)
                .WithParameter(_editTodoDialogViewContainer);
            
            builder.Register<TodoStorage>(Lifetime.Singleton);
            builder.Register<TodoStorageViewModel>(Lifetime.Singleton);
            
            builder.RegisterBuildCallback(container =>
            {
                var storage = container.Resolve<TodoStorage>();
                
                foreach (var todo in _todos)
                    storage.Add(todo);
            });
        }
    }
}