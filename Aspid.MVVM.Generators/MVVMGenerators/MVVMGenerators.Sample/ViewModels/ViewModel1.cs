using Aspid.UI.MVVM;
using Aspid.UI.MVVM.Mono.ViewModels;
using Aspid.UI.MVVM.ViewModels;
using Aspid.UI.MVVM.ViewModels.Generation;
using Aspid.UI.MVVM.Views;

namespace MVVMGenerators.Sample.ViewModels;

[ViewModel]
public partial class ViewModel1
{
    [Bind] private int _age;
    [ReadOnlyBind] private readonly string _name;
}

[ViewModel]
public partial class Child1ViewModel1 : ViewModel1
{
    [Bind] private float _money;
    [ReadOnlyBind] private readonly string _what;
}