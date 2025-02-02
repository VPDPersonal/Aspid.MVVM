using Aspid.MVVM;
using Aspid.MVVM.Generation;

namespace MVVMGenerators.Sample.ViewModels;

[ViewModel]
public partial class ViewModel1
{
    [Bind] private int _number1;
    [Bind] private int _number2;
    
    [Bind] private string _name;
    [Bind] private readonly string _id;
}

[ViewModel]
public partial class Child1ViewModel1 : ViewModel1
{
    [Bind] private int _number3;
    [Bind] private readonly IRelayCommand _do1Command;

    [RelayCommand]
    private void Do2()
    {
        
    }
}

[ViewModel]
public partial class ViewModel4
{
    [BindAlso(nameof(FullName))]
    [Bind] private string _firstName;
    
    [BindAlso(nameof(Nickname))]
    [BindAlso(nameof(FullName))]
    [Bind] private string _lastName;
    
    private string FullName => _firstName + " " + _lastName;
    
    private string Nickname => $"Nick: {_lastName}";
}

public interface IViewModel2 : IViewModel
{
    
}

[ViewModel]
public partial class ViewModel2 : IViewModel2
{
    
}