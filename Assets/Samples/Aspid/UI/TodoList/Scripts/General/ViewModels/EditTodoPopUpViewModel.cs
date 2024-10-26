using Aspid.UI.MVVM.Commands;
using Aspid.UI.MVVM.Generation;
using Aspid.UI.TodoList.Models;
using Aspid.UI.MVVM.ViewModels.Generation;

namespace Aspid.UI.TodoList.ViewModels
{
    // TODO Aspid.UI Translate
    // В будущем аттрибут CreateFrom будет перемещен в пакет UnityFastTools
    // Аттрибут CreateFrom служит меткой для Source Generator, что необходимо создать методы расширения, которые 
    // создают EditTodoPopUpViewModel из EditTodoPopUp.
    // Примеры создания:
    // 1.
    // EditTodoPopUp popUp;
    // EditTodoPopUpViewModel viewModel = popUp.ToEditTodoPopUpViewModel();
    // 2.
    // EditTodoPopUp[] popUps;
    // EditTodoPopUpViewModel[] viewModels = popUps.ToEditTodoPopUpViewModel();
    [ViewModel]
    [CreateFrom(typeof(EditTodoPopUp))]
    public partial class EditTodoPopUpViewModel
    {
        [Bind] private string _text;
        [ReadOnlyBind] private readonly IRelayCommand _cancelCommand;
        [ReadOnlyBind] private readonly IRelayCommand _renamedCommand;

        private readonly EditTodoPopUp _editTodoPopUp;
        
        public EditTodoPopUpViewModel(EditTodoPopUp editTodoPopUp)
        {
            _editTodoPopUp = editTodoPopUp;
            _text = editTodoPopUp.SourceText;
            _cancelCommand = new RelayCommand(editTodoPopUp.Cancel);
            _renamedCommand = new RelayCommand(editTodoPopUp.Rename);
        }

        partial void OnTextChanged(string newValue)
        {
            _editTodoPopUp.CurrentText = newValue;
        }
    }
}