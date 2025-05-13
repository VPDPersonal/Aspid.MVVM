using Aspid.MVVM;

namespace MVVMGenerators.Sample;

[ViewModel]
public partial class PersonViewModel(int age, string name, string family) 
{
    [Access(Get = Access.Public)]
    [Bind] private int _age = age;
    
    [Access(Get = Access.Public)]
    [Bind] private string _name = name;
    
    [Access(Get = Access.Public)]
    [Bind] private string _family = family;

    [Bind] private bool _canExecute;

    [RelayCommand(CanExecute = nameof(CanExecuteChangeAge))]
    private void ChangeAge(int age)
    {
        
    }

    private bool CanExecuteChangeAge() => false;

    // private bool CanExecuteChangeAge()
    // {
    //     return true;
    // }
    
    partial interface IBindableMembers : IPersonViewModel { }
}

public interface IPersonViewModel : IViewModel
{
    public IBindableMemberEventAdder Age { get; }
            
    public IBindableMemberEventAdder Name { get; }
            
    public IBindableMemberEventAdder Family { get; }
            
    public IBindableMemberEventAdder CanExecute { get; }
            
    public IBindableMemberEventAdder ChangeAgeCommand { get; }
}

[ViewModel]
public partial class WorkerViewModel1(int age, string name, string family) 
    : PersonViewModel(age, name, family)
{
    [Bind] private int _experience;
}

[ViewModel]
public partial class BossViewModel(int age, string name, string family) 
    : WorkerViewModel1(age, name, family)
{
}