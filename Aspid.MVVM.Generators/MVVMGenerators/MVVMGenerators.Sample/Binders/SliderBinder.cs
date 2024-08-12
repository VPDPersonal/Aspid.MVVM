using UltimateUI.MVVM;
using UltimateUI.MVVM.Commands;
using UltimateUI.MVVM.ViewModels;
using UnityEngine.UI;

namespace MVVMGenerators.Sample.Binders;

public class SliderBinder : IBinder<IRelayCommand<int>>
{
    public SliderBinder(Slider slider) { }

    public void SetValue(IRelayCommand<int> value) { }

    public void Bind(IViewModel viewModel, string id) { }

    public void Unbind(IViewModel viewModel, string id) { }
}