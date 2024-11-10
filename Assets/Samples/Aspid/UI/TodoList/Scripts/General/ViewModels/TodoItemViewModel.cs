using System;
using Aspid.UI.TodoList.Models;
using Aspid.UI.TodoList.Factories;
using Aspid.UI.MVVM.ViewModels.Generation;

namespace Aspid.UI.TodoList.ViewModels
{
    [ViewModel]
    public partial class TodoItemViewModel : ITodoItemViewModel
    {
        public event Action<ITodoItemViewModel> Edited;
        public event Action<ITodoItemViewModel> Deleted;

        // TODO Aspid.UI Translate
        // Атрибут Bind по умолчанию создает полностью приватное поле
        // private bool IsVisible
        // {
        //     get => ...
        //     set => ...
        // }
        // Чтобы изменить уровень доступа свойства, можно указать атрибут Access, который служит меткой для
        // Source Generator и создает вместо приватного поля - поле с настройкам из атрибута Access
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
                
                // TODO Aspid.UI Translate
                // Для корректной работы привязки, необходимо использовать свойство, а не поле.
                // Так как анализатор не знает о существование этого свойства и поля он не подскажет об ошибки.
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