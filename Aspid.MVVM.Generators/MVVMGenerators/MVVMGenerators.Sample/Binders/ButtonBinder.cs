using AspidUI.MVVM;
using AspidUI.MVVM.Commands;
using AspidUI.MVVM.Unity.Generation;
using AspidUI.MVVM.ViewModels;
using UnityEngine.UI;

namespace MVVMGenerators.Sample.Binders;

public sealed partial class ButtonBinder : IBinder<IRelayCommand>
{
    public ButtonBinder(Button? button) { }
    
    [BinderLog]
    public void SetValue(IRelayCommand value) { }

    public void Bind(IViewModel viewModel, string id) { }

    public void Unbind(IViewModel viewModel, string id) { }
}