using Aspid.UI.MVVM.Commands;
using Aspid.UI.MVVM.Generation;
using Aspid.UI.TodoList.Models;
using Aspid.UI.MVVM.ViewModels.Generation;

namespace Aspid.UI.TodoList.ViewModels
{
    // In the future, the CreateFrom attribute will be moved to the UnityFastTools package 
    // https://github.com/VPDPersonal/UnityFastTools
    // The CreateFrom attribute serves as a label for the Source Generator to create extension methods that 
    // create an EditTodoPopUpViewModel from EditTodoPopUp.
    // Creation examples:
    // 1.
    // EditTodoPopUp popUp;
    // EditTodoPopUpUpViewModel viewModel = popUp.ToEditTodoPopUpViewModel();
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