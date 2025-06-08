namespace Aspid.MVVM.ExampleScripts.ViewModels.Commands
{
    [ViewModel]
    public partial class Ex3CommandViewModel
    {
        [Bind] private string _text;
        [Bind] private bool _canDo2;
        
        private bool CanDo3 { get; set; }

        [RelayCommand(CanExecute = nameof(CanDo1))]
        private void Do1(int a) => Text = "Command1";

        private bool CanDo1() => true;
        
        [RelayCommand(CanExecute = nameof(CanDo2))]
        private void Do2(int a) => Text = "Command2";
        
        [RelayCommand(CanExecute = nameof(CanDo3))]
        private void Do3(int a) => Text = "Command3";
    }
}