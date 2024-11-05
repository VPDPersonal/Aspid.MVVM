using Aspid.UI.MVVM;
using Aspid.UI.MVVM.Commands;
using Aspid.UI.MVVM.Mono.Views;
using MVVMGenerators.Sample.Binders;
using Aspid.UI.MVVM.Views.Generation;

namespace MVVMGenerators.Sample.Views;

[View]
public partial class MonoViewTest2 : MonoView
{
    private IReverseBinder<string> InputCommand =>
        GetComponent<IReverseBinder<string>>();
    
    private IBinder<IRelayCommand<int>>[] NumberCommand =>
        GetComponentsInChildren<IBinder<IRelayCommand<int>>>();

    private ButtonMonoBinder OkCommand =>
        GetComponent<ButtonMonoBinder>();
}