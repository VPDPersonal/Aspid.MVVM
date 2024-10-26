using Aspid.UI.TodoList.Models;
using Aspid.UI.TodoList.ViewModels;

namespace Aspid.UI.TodoList.Factories
{
    public class TodoItemViewModelFactory
    {
        private readonly EditTodoPopUpViewFactory _editTodoPopUpViewFactory;

        public TodoItemViewModelFactory(EditTodoPopUpViewFactory editTodoPopUpViewFactory)
        {
            _editTodoPopUpViewFactory = editTodoPopUpViewFactory;
        }
        
        public TodoItemViewModel Create(Todo todo) => new(todo, _editTodoPopUpViewFactory);
    }
}