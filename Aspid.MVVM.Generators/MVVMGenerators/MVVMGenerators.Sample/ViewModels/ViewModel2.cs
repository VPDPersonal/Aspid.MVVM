using AspidUI.MVVM.ViewModels.Generation;

namespace MVVMGenerators.Sample.ViewModels;

[ViewModel]
public partial class ViewModel2
{
    [BindAlso(nameof(FullName))]
    [Bind] private string _name;
 
    [BindAlso(nameof(FullName))]
    [Bind] private string _family;
    
    public string FullName => Name + " " + _family;
}