using Aspid.UI.MVVM;
using UnityEngine.UI;
using Aspid.UI.MVVM.Commands;
using Aspid.UI.MVVM.ViewModels;

namespace MVVMGenerators.Sample.Binders;

public class SliderBinder : IBinder<IRelayCommand<int>>
{
    public SliderBinder(Slider slider) { }

    public void SetValue(IRelayCommand<int> value) { }

    public void Bind(IViewModel viewModel, string id) { }
    public void Unbind()
    {
        throw new System.NotImplementedException();
    }

    public void Unbind(IViewModel viewModel, string id) { }
}