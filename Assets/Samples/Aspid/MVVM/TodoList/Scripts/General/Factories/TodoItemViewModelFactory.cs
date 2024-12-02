using System;
using UnityEngine;
using Aspid.MVVM.TodoList.Models;
using Aspid.MVVM.TodoList.ViewModels;

namespace Aspid.MVVM.TodoList.Factories
{
    public class TodoItemViewModelFactory
    {
        private readonly Settings _settings;
        private readonly EditTodoPopUpViewFactory _editTodoPopUpViewFactory;

        public TodoItemViewModelFactory(EditTodoPopUpViewFactory editTodoPopUpViewFactory, Settings settings)
        {
            _settings = settings;
            _editTodoPopUpViewFactory = editTodoPopUpViewFactory;
        }

        public ITodoItemViewModel Create(Todo todo) => _settings.Type switch
        {
            ViewModelType.Auto => new TodoItemViewModel(todo, _editTodoPopUpViewFactory),
            ViewModelType.Manual => new TodoItemViewModelWithManualBind(todo, _editTodoPopUpViewFactory),
            _ => throw new ArgumentOutOfRangeException()
        };
        
        [Serializable]
        public class Settings
        {
            [field: SerializeField]
            public ViewModelType Type { get; private set; }
        }
        
        public enum ViewModelType
        {
            Auto,
            Manual
        }
    }
}