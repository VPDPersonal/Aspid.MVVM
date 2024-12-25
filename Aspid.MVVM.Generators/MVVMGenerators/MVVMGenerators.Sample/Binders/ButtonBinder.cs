using Aspid.MVVM;
using UnityEngine.UI;
using Aspid.MVVM.Commands;
using Aspid.MVVM.ViewModels;
using Aspid.MVVM.Mono.Generation;

namespace MVVMGenerators.Sample.Binders;

public sealed partial class ButtonBinder : IBinder<IRelayCommand>
{
    public ButtonBinder(Button? button) { }
    
    [BinderLog]
    public void SetValue(IRelayCommand value) { }

    public void Bind(IViewModel viewModel, string id) { }
    public void Unbind()
    {
        throw new System.NotImplementedException();
    }

    public void Unbind(IViewModel viewModel, string id) { }
}