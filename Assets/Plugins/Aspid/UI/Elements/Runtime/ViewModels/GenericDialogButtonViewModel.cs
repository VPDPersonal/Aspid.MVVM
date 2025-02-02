using Aspid.MVVM;
using Aspid.MVVM.Generation;
using Aspid.UI.Elements.Models;

namespace Aspid.UI.Elements.ViewModels
{
    [ViewModel]
    public partial class GenericDialogButtonViewModel
    {
        [Bind] private readonly string _text;
        [Bind] private readonly IRelayCommand _clickCommand;

        private GenericDialogButton _button;

        [CreateFrom(typeof(GenericDialogButton))]
        public GenericDialogButtonViewModel(GenericDialogButton button)
        {
            _text = button.Text;
            _clickCommand = new RelayCommand(button.CallBack);
        }
    }
}