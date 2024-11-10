using System;
using Aspid.UI.MVVM.ViewModels;
using Aspid.UI.TodoList.Models;

namespace Aspid.UI.TodoList.ViewModels
{
    public interface ITodoItemViewModel : IViewModel
    {
        public event Action<ITodoItemViewModel> Edited;
        public event Action<ITodoItemViewModel> Deleted;
        
        public bool IsVisible { get; set; }
        
        public IReadOnlyTodo Todo { get; }
    }
}