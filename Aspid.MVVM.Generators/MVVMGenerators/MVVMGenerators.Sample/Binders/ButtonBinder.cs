using Aspid.MVVM;
using UnityEngine.UI;

namespace MVVMGenerators.Sample.Binders;

public sealed partial class ButtonBinder : IBinder<IRelayCommand>
{
    public ButtonBinder(Button? button) { }
    
    [BinderLog]
    public void SetValue(IRelayCommand value) { }

    public void Bind() { }

    public void Bind<T>(in BindableMember<T> bindableMember)
    {
        throw new System.NotImplementedException();
    }

    public void Bind(IViewModelEventAdder viewModelEventAdder)
    {
        throw new System.NotImplementedException();
    }

    public void Unbind() { }
}