using Aspid.MVVM.Commands;
using Aspid.MVVM.Generation;
using Aspid.UI.Elements.Models;
using Aspid.MVVM.ViewModels.Generation;

namespace Aspid.UI.Elements.ViewModels
{
    [ViewModel]
    [CreateFrom(typeof(GenericDialogButton))]
    public partial class GenericDialogButtonViewModel
    {
        [ReadOnlyBind] private readonly string _text;
        [ReadOnlyBind] private readonly IRelayCommand _clickCommand;

        private GenericDialogButton _button;

        public GenericDialogButtonViewModel(GenericDialogButton button)
        {
            _text = button.Text;
            _clickCommand = new RelayCommand(button.CallBack);
        }
    }
}