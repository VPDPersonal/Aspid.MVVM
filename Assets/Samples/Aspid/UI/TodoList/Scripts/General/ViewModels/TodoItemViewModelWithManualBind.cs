using System;
using Aspid.UI.MVVM;
using Aspid.UI.MVVM.ViewModels;
using Aspid.UI.TodoList.Models;
using Aspid.UI.TodoList.Factories;
using Aspid.UI.MVVM.ViewModels.Generation;

namespace Aspid.UI.TodoList.ViewModels
{

    [ViewModel]
    public partial class TodoItemViewModelWithManualBind : ITodoItemViewModel
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
        
        // TODO Aspid.UI Translate
        // Обычно за нас эти поля создает Source Generator, но ради примера мы сделаем ручную привязку двух полей.
        private ViewModelEvent<string> _textChangedEvent;
        private ViewModelEvent<bool> _isCompletedChangedEvent;
        
        private readonly Todo _todo;
        private readonly EditTodoPopUpViewFactory _editTodoPopUpViewFactory;

        public IReadOnlyTodo Todo => _todo;
        
        // TODO Aspid.UI Translate
        // Свойство для связывания
        private string Text
        {
            get => _todo.Text;
            set => SetText(value);
        }
        
        // TODO Aspid.UI Translate
        // Свойство для связывания
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

        // TODO Aspid.UI Translate
        // Установка значения и оповещение об изменение происходит в методе, а не прямо в свойстве, чтобы корректно
        // провести привязку данных.
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

        #region Manual Binds
        // TODO Aspid.UI Translate
        // Ручное связывание
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