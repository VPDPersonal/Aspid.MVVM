using UltimateUI.MVVM.ViewModels;

namespace MVVMGenerators.Sample;

[ViewModel]
public partial class PersonViewModel(int age, string name, string family)
{
    [Bind] private int _age = age;
    [Bind] private string _name = name;
    [Bind] private string _family = family;
}

[ViewModel]
public partial class WorkerViewModel(int age, string name, string family) 
    : PersonViewModel(age, name, family)
{
    [Bind] private int _experience;
}

[ViewModel]
public partial class BossViewModel(int age, string name, string family) 
    : WorkerViewModel(age, name, family)
{
}