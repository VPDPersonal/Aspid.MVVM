using Aspid.MVVM;
using UnityEngine.UI;
using Aspid.MVVM.Mono.Generation;

namespace MVVMGenerators.Sample.Binders;

public sealed partial class ButtonBinder : IBinder<IRelayCommand>
{
    public ButtonBinder(Button? button) { }
    
    [BinderLog]
    public void SetValue(IRelayCommand value) { }

    public void Bind(in BindParameters parameters) { }

    public void Unbind() { }
}