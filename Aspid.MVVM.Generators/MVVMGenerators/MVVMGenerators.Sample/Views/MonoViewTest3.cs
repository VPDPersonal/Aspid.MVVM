using UnityEngine;
using UnityEngine.UI;
using Aspid.MVVM.Mono.Views;
using Aspid.MVVM.Views.Generation;
using MVVMGenerators.Sample.Binders;

namespace MVVMGenerators.Sample.Views;

[View]
public partial class MonoViewTest3 : MonoView
{ 
    // TODO Error of generation
    [AsBinder(typeof(SliderBinder))]
    [SerializeField] private Slider _asBinder1;
    
    [AsBinder(typeof(SliderBinder))]
    [SerializeField] private Slider[] _asBinder2;

    [AsBinder(typeof(ButtonBinder))]
    private Button AsBinderProperty1 => GetComponent<Button>();
    
    [AsBinder(typeof(ButtonBinder))]
    private Button[] AsBinder2Property => GetComponents<Button>();
    
    [AsBinder(typeof(SliderBinder))]
    public Slider AsBinder1 => _asBinder1;
}