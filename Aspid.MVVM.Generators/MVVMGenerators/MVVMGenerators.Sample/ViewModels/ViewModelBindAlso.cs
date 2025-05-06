using Aspid.MVVM;

namespace MVVMGenerators.Sample.ViewModels;

[ViewModel]
public partial class ViewModelBindAlso
{
    [BindAlso(nameof(Nickname))]
    [BindAlso(nameof(FullName))]
    [Bind] private string _firstName;
    
    [BindAlso(nameof(FullName))]
    [Bind] private string _lastName;

    public string Nickname => _firstName + "Aspid";

    public string FullName => _firstName + " " + _lastName;
}