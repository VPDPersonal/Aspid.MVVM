using AspidUI.MVVM.Unity.Views;
using AspidUI.MVVM.ViewModels;
using AspidUI.MVVM.Views.Generation;
using UnityEngine.UI;
using UnityEngine;
using MVVMGenerators.Sample.Binders;

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