using UnityEngine.UI;

namespace Aspid.MVVM.Generators.Sample.Binders;

public class SliderBinder : IBinder<IRelayCommand<int>>
{
    public SliderBinder(Slider slider) { }

    public void SetValue(IRelayCommand<int> value) { }

    public void Bind() { }

    public void Bind(IBindableMemberEventAdder bindableMemberEventAdder)
    {
        
    }

    public void Unbind() { }
}