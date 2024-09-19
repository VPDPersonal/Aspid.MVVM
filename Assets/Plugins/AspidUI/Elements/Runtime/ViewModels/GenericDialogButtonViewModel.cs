using System;
using AspidUI.MVVM.Commands;
using System.Collections.Generic;
using AspidUI.MVVM.ViewModels.Generation;
using Plugins.AspidUI.Elements.Runtime.Models;

namespace Plugins.AspidUI.Elements.Runtime.ViewModels
{
    [ViewModel]
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