using AspidUI.MVVM;
using UnityEngine.UI;
using AspidUI.MVVM.Commands;
using AspidUI.MVVM.ViewModels;

namespace MVVMGenerators.Sample.Binders;

public class SliderBinder : IBinder<IRelayCommand<int>>
{
    public SliderBinder(Slider slider) { }

    public void SetValue(IRelayCommand<int> value) { }

    public void Bind(IViewModel viewModel, string id) { }

    public void Unbind(IViewModel viewModel, string id) { }
}