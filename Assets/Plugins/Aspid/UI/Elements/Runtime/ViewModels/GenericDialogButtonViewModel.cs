using Aspid.MVVM;
using Aspid.MVVM.Generation;
using Aspid.UI.Elements.Models;

namespace Aspid.UI.Elements.ViewModels
{
    [ViewModel]
    public partial class GenericDialogButtonViewModel
    {
        [ReadOnlyBind] private readonly string _text;
        [ReadOnlyBind] private readonly IRelayCommand _clickCommand;

        private GenericDialogButton _button;

        [CreateFrom(typeof(GenericDialogButton))]
        public GenericDialogButtonViewModel(GenericDialogButton button)
        {
            _text = button.Text;
            _clickCommand = new RelayCommand(button.CallBack);
        }
    }
}