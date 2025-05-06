using Aspid.MVVM;
using UnityEngine.UI;

namespace MVVMGenerators.Sample.Binders;

public class SliderBinder : IBinder<IRelayCommand<int>>
{
    public SliderBinder(Slider slider) { }

    public void SetValue(IRelayCommand<int> value) { }

    public void Bind() { }

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