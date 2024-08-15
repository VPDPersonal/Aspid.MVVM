using UnityEngine.UI;
using UltimateUI.MVVM;
using UltimateUI.MVVM.Views;
using UltimateUI.MVVM.Unity.Views;
using MVVMGenerators.Sample.Binders;
using UltimateUI.MVVM.ViewModels;
using UnityEngine;

namespace MVVMGenerators.Sample.Views;

[View]
public partial class MonoViewTest3 : MonoView
{
    [AsBinder(typeof(SliderBinder))]
    [SerializeField] private Slider MinRange;
    
    [AsBinder(typeof(SliderBinder))]
    [SerializeField] private Slider[] MaxRange;

    [AsBinder(typeof(ButtonBinder))]
    private Button OkCommand => GetComponent<Button>();
    
    [AsBinder(typeof(ButtonBinder))]
    private Button[] CancelCommand => GetComponents<Button>();
    
    protected override void DeinitializeIternal(IViewModel viewModel)
    {
        throw new System.NotImplementedException();
    }
}