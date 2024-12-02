using System;
using Aspid.MVVM.ViewModels;
using Aspid.MVVM.TodoList.Models;

namespace Aspid.MVVM.TodoList.ViewModels
{
    public interface ITodoItemViewModel : IViewModel
    {
        public event Action<ITodoItemViewModel> Edited;
        public event Action<ITodoItemViewModel> Deleted;
        
        public bool IsVisible { get; set; }
        
        public IReadOnlyTodo Todo { get; }
    }
}