#if ASPID_MVVM_VCONTAINER_INTEGRATION
using VContainer;
using UnityEngine;
using VContainer.Unity;
using Aspid.MVVM.TodoList.Views;
using Aspid.MVVM.TodoList.Models;
using Aspid.MVVM.TodoList.Factories;
using Aspid.MVVM.TodoList.ViewModels;

namespace Aspid.MVVM.TodoList.VContainer
{
    public sealed class TodoListLifetimeScope : LifetimeScope
    {
        [SerializeField] private TodoItemViewModelFactory.Settings _todoItemViewModelFactorySettings;
        
        [Header("Edit Todo PopUp View Factory")]
        [SerializeField] private Transform _editTodoPopUpViewContainer;
        [SerializeField] private EditTodoPopUpView _editTodoPopUpViewPrefab;

        [Header("Todos")]
        [SerializeField] private string[] _todos;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<TodoItemViewModelFactory>(Lifetime.Singleton)
                .WithParameter(_todoItemViewModelFactorySettings);
            
            builder.Register<EditTodoPopUpViewFactory>(Lifetime.Singleton)
                .WithParameter(_editTodoPopUpViewPrefab)
                .WithParameter(_editTodoPopUpViewContainer);
            
            builder.Register<TodoStorage>(Lifetime.Singleton);
            builder.Register<TodoStorageViewModel>(Lifetime.Singleton);
            
            builder.RegisterBuildCallback(container =>
            {
                var storage = container.Resolve<TodoStorage>();
                
                foreach (var todo in _todos)
                    storage.AddTodo(todo);
            });
        }
    }
}
#endif
