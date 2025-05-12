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

//  Generated Code:
//  public partial class Ex3CommandViewModel
//  {
//      private RelayCommand<int> __do1Command;
//      private RelayCommand<int> Do1Command => __do1Command ??= new RelayCommand<int>(Do1, (_) => CanDo1());
//          
//      private RelayCommand<int> __do2Command;
//      private RelayCommand<int> Do2Command => __do2Command ??= new RelayCommand<int>(Do2, (_) => CanDo2);
//
//      private RelayCommand<int> __do3Command;
//      private RelayCommand<int> Do3Command => __do3Command ??= new RelayCommand<int>(Do3, (_) => CanDo3);
//  }