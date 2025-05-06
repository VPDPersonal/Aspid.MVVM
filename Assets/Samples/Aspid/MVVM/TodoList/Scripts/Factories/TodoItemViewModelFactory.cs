using Aspid.MVVM.TodoList.Models;
using Aspid.MVVM.TodoList.ViewModels;

namespace Aspid.MVVM.TodoList.Factories
{
    public class TodoItemViewModelFactory
    {
        private readonly EditTodoPopUpViewFactory _editTodoPopUpViewFactory;

        public TodoItemViewModelFactory(EditTodoPopUpViewFactory editTodoPopUpViewFactory)
        {
            _editTodoPopUpViewFactory = editTodoPopUpViewFactory;
        }

        public ITodoItemViewModel Create(Todo todo) => new TodoItemViewModel(todo, _editTodoPopUpViewFactory);
    }
}