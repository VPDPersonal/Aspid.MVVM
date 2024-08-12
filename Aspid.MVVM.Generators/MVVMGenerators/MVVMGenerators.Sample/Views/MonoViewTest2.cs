using UltimateUI.MVVM;
using UltimateUI.MVVM.Views;
using UltimateUI.MVVM.Commands;
using UltimateUI.MVVM.Unity.Views;
using MVVMGenerators.Sample.Binders;

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