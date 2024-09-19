using AspidUI.MVVM;
using AspidUI.MVVM.Commands;
using AspidUI.MVVM.Unity.Views;
using AspidUI.MVVM.ViewModels;
using AspidUI.MVVM.Views.Generation;
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
    
    protected override void DeinitializeIternal(IViewModel viewModel)
    {
        throw new System.NotImplementedException();
    }
}