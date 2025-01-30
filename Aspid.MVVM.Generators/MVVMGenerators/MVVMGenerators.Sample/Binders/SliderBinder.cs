using Aspid.MVVM;
using UnityEngine.UI;

namespace MVVMGenerators.Sample.Binders;

public class SliderBinder : IBinder<IRelayCommand<int>>
{
    public SliderBinder(Slider slider) { }

    public void SetValue(IRelayCommand<int> value) { }

    public void Bind(in BindParameters parameters) { }

    public void Unbind() { }
}