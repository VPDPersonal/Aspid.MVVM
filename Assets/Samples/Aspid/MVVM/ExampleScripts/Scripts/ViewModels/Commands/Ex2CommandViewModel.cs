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

//  Generated Code:
//  public partial class Ex2CommandViewModel
//  {
//  	private RelayCommand __do1Command;
//  	private RelayCommand Do1Command => __do1Command ??= new RelayCommand(Do1, CanDo1);
//      
//  	private RelayCommand<int> __do2Command;
//  	private RelayCommand<int> Do2Command => __do2Command ??= new RelayCommand<int>(Do2, CanDo2);
//      
//  	private RelayCommand<int,int> __do3Command;
//  	private RelayCommand<int,int> Do3Command => __do3Command ??= new RelayCommand<int, int>(Do3, CanDo3);
//      
//  	private RelayCommand<int,int,int> __do4Command;
//  	private RelayCommand<int,int,int> Do4Command => __do4Command ??= new RelayCommand<int, int, int>(Do4, CanDo4);
//      
//  	private RelayCommand<int,int,int,int> __do5Command;
//  	private RelayCommand<int,int,int,int> Do5Command => __do5Command ??= new RelayCommand<int, int, int, int>(Do5, CanDo5);
//  }