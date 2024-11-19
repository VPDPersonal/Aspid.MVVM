using Aspid.UI.MVVM.Commands;
using Aspid.UI.MVVM.ViewModels.Generation;

namespace Samples.Aspid.UI.ExampleScripts.ViewModels
{
    [ViewModel]
    public partial class Command1ViewModel
    {
        [Bind] private string _text;
        
        [ReadOnlyBind] private readonly IRelayCommand _command1;
        [ReadOnlyBind] private readonly IRelayCommand<int> _command2;
        [ReadOnlyBind] private readonly IRelayCommand<int, int> _command3;
        [ReadOnlyBind] private readonly IRelayCommand<int, int, int> _command4;
        [ReadOnlyBind] private readonly IRelayCommand<int, int, int, int> _command5;

        public Command1ViewModel()
        {
            _command1 = new RelayCommand(Do1, CanDo1);
            _command2 = new RelayCommand<int>(Do2, CanDo2);
            _command3 = new RelayCommand<int, int>(Do3, CanDo3);
            _command4 = new RelayCommand<int, int, int>(Do4, CanDo4);
            _command5 = new RelayCommand<int, int, int, int>(Do5, CanDo5);
        }
        
        private void Do1() => Text = "Command1";

        private bool CanDo1() => true;

        private void Do2(int arg1) => Text = $"Command2 {arg1}";

        private bool CanDo2(int arg1) => true;
        
        private void Do3(int arg1, int arg2) => Text = $"Command3 {arg1}, {arg2}";

        private bool CanDo3(int arg1, int arg2) => true;
        
        private void Do4(int arg1, int arg2, int arg3) => Text = $"Command4 {arg1}, {arg2}, {arg3}";

        private bool CanDo4(int arg1, int arg2, int arg3) => true;
        
        private void Do5(int arg1, int arg2, int arg3, int arg4) => Text = $"Command5 {arg1}, {arg2}, {arg3}, {arg4}";

        private bool CanDo5(int arg1, int arg2, int arg3, int arg4) => true;
    }
}