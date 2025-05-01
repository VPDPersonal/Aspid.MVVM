using System;
using Aspid.MVVM.TodoList.Models;
using Aspid.MVVM.TodoList.Factories;

namespace Aspid.MVVM.TodoList.ViewModels
{
    [ViewModel]
    public partial class TodoItemViewModel : ITodoItemViewModel
    {
        public event Action<ITodoItemViewModel> Edited;
        public event Action<ITodoItemViewModel> Deleted;
        
        // The Bind attribute creates a fully private field by default
        // private bool IsVisible
        // {
        //     get => ...
        //     set => ...
        // }
        // To change the access level of the property, you can specify the Access attribute, which serves as a label for the
        // Source Generator and creates instead of a private field - a field with settings from the Access attribute
        [Access(Access.Public)]
        [Bind] private bool _isVisible;
        
        [Bind] private string _text;
        [Bind] private bool _isCompleted;
        
        private readonly Todo _todo;
        private readonly EditTodoPopUpViewFactory _editTodoPopUpViewFactory;

        public IReadOnlyTodo Todo => _todo;
        
        public TodoItemViewModel(Todo todo, EditTodoPopUpViewFactory editTodoPopUpViewFactory)
        {
            _todo = todo;
            _text = todo.Text;
            _isCompleted = todo.IsCompleted;
            _editTodoPopUpViewFactory = editTodoPopUpViewFactory;
        }

        [RelayCommand]
        private void Edit()
        {
            var editPopUp = new EditTodoPopUp(_todo.Text);
            var view = _editTodoPopUpViewFactory.Create(editPopUp);

            editPopUp.Renamed += Renamed;
            editPopUp.Canceled += ReleasePopUp;
            return;

            void Renamed(string newText)
            {
                ReleasePopUp();
                
                // In order for the binding to work correctly, it is necessary to use a property, not a field.
                // Since the analyzer does not know about the existence of this property and field, it will not prompt about the error.
                Text = newText;
                Edited?.Invoke(this);
            }

            void ReleasePopUp()
            {
                editPopUp.Renamed -= Renamed;
                editPopUp.Canceled -= ReleasePopUp;
                
                EditTodoPopUpViewFactory.Release(view);
            }
        }

        [RelayCommand]
        private void Delete() => Deleted?.Invoke(this);

        partial void OnTextChanged(string newValue) =>
            _todo.Text = newValue;

        partial void OnIsCompletedChanged(bool newValue) =>
            _todo.IsCompleted = newValue;
    }
}