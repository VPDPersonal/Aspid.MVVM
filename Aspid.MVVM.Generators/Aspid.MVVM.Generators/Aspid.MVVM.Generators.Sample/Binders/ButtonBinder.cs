using UnityEngine.UI;

namespace Aspid.MVVM.Generators.Sample.Binders;

public sealed partial class ButtonBinder : IBinder<IRelayCommand>
{
    public ButtonBinder(Button? button) { }
    
    [BinderLog]
    public void SetValue(IRelayCommand value) { }

    public void Bind(IBindableMemberEventAdder bindableMemberEventAdder)
    {
        throw new System.NotImplementedException();
    }

    public void Unbind() { }
}