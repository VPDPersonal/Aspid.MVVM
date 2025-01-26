using System;
using Aspid.MVVM.Generation;
using Aspid.MVVM.TodoList.Models;
using Aspid.MVVM.TodoList.Factories;

namespace Aspid.MVVM.TodoList.ViewModels
{

    [ViewModel]
    public partial class TodoItemViewModelWithManualBind : ITodoItemViewModel
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
        
        // Usually Source Generator creates these fields for us, but for the sake of this example we will bind two fields manually.
        private ViewModelEvent<string> _textChangedEvent;
        private ViewModelEvent<bool> _isCompletedChangedEvent;
        
        private readonly Todo _todo;
        private readonly EditTodoPopUpViewFactory _editTodoPopUpViewFactory;

        public IReadOnlyTodo Todo => _todo;
        
        // Property to bind
        private string Text
        {
            get => _todo.Text;
            set => SetText(value);
        }
        
        // Property to bind
        private bool IsCompleted
        {
            get => _todo.IsCompleted;
            set => SetCompleted(value);
        }

        public TodoItemViewModelWithManualBind(Todo todo, EditTodoPopUpViewFactory editTodoPopUpViewFactory)
        {
            _todo = todo;
            _editTodoPopUpViewFactory = editTodoPopUpViewFactory;
        }

        // Setting the value and notification of the change is done in the method, not directly in the property, in order
        // to correctly bind the data.
        private void SetText(string value)
        {
            var text = _todo.Text;
            
            if (ViewModelUtility.SetProperty(ref text, value))
            {
                _todo.Text = text;;
                _textChangedEvent?.Invoke(text);
            }
        }

        private void SetCompleted(bool value)
        {
            var isCompleted = _todo.IsCompleted;
            
            if (ViewModelUtility.SetProperty(ref isCompleted, value))
            {
                _todo.IsCompleted = isCompleted;;
                _isCompletedChangedEvent?.Invoke(value);
            }
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

        #region Manual Binds
        // Manual binding
        partial void AddBinderManual(IBinder binder, string propertyName, ref IRemoveBinderFromViewModel removeBinder)
        {
            removeBinder = propertyName switch
            {
                nameof(Text) => ViewModelUtility.AddBinder(binder, Text, ref _textChangedEvent, SetText),
                nameof(IsCompleted) => ViewModelUtility.AddBinder(binder, IsCompleted, ref _isCompletedChangedEvent, SetCompleted),
                _ => removeBinder
            };
        }
        #endregion
    }
}