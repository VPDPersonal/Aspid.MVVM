namespace Aspid.MVVM.ExampleScripts.ViewModels.Commands
{
    [ViewModel]
    public partial class Ex2CommandViewModel
    {
        [Bind] private string _text;

        [RelayCommand(CanExecute = nameof(CanDo1))]
        private void Do1() => Text = "Command1";

        private bool CanDo1() => true;

        [RelayCommand(CanExecute = nameof(CanDo2))]
        private void Do2(int arg1) => Text = $"Command2 {arg1}";

        private bool CanDo2(int arg1) => true;
        
        [RelayCommand(CanExecute = nameof(CanDo3))]
        private void Do3(int arg1, int arg2) => Text = $"Command3 {arg1}, {arg2}";

        private bool CanDo3(int arg1, int arg2) => true;
        
        [RelayCommand(CanExecute = nameof(CanDo4))]
        private void Do4(int arg1, int arg2, int arg3) => Text = $"Command4 {arg1}, {arg2}, {arg3}";

        private bool CanDo4(int arg1, int arg2, int arg3) => true;
        
        [RelayCommand(CanExecute = nameof(CanDo5))]
        private void Do5(int arg1, int arg2, int arg3, int arg4) => Text = $"Command5 {arg1}, {arg2}, {arg3}, {arg4}";

        private bool CanDo5(int arg1, int arg2, int arg3, int arg4) => true;
    }
}