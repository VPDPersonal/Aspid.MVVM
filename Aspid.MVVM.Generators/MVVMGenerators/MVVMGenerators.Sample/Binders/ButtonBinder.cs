using Aspid.MVVM;
using UnityEngine.UI;

namespace MVVMGenerators.Sample.Binders;

public sealed partial class ButtonBinder : IBinder<IRelayCommand>
{
    public ButtonBinder(Button? button) { }
    
    [BinderLog]
    public void SetValue(IRelayCommand value) { }

    public void Bind(in BindParameters parameters) { }

    public void Bind<T>(BindableMember<T> bindableMember)
    {
        throw new System.NotImplementedException();
    }

    public void Bind(IViewModelEventAdder viewModelEventAdder)
    {
        throw new System.NotImplementedException();
    }

    public void Unbind() { }
}