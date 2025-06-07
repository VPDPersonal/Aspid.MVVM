using System;
using Aspid.MVVM.Generators.Sample.Binders;
using Aspid.MVVM.Unity;
using UnityEngine;
using UnityEngine.UI;

namespace Aspid.MVVM.Generators.Sample.Views;

[View]
public partial class MonoViewTest3 : MonoView
{
    public const string a = "s";
    
    // TODO Error of generation
    [AsBinder(typeof(SliderBinder))]
    [SerializeField] private Slider _asBinder1;
    
    [AsBinder(typeof(NewSliderBinder), nameof(_asBinder1), 123, true, 1.1, 1.5f, 0x0A, 3.2e3, '\x78', 'd', 1.9f, null, typeof(Slider), T.A | T.B, nameof(a))]
    [SerializeField] private Slider[] _asBinder2;

    private NewSliderBinder slider1 => new(null);

    [AsBinder(typeof(ButtonBinder))]
    private Button AsBinderProperty1 => GetComponent<Button>();
    
    [AsBinder(typeof(ButtonBinder))]
    private Button[] AsBinder2Property => GetComponents<Button>();
    
    [AsBinder(typeof(SliderBinder))]
    public Slider AsBinder1 => _asBinder1;
}

[Flags]
public enum T
{
    A,
    B
}

public class NewSliderBinder : IBinder<Slider>
{
    public NewSliderBinder(Slider slider)
    {
        
    }
    
    public NewSliderBinder(Slider slider, Slider p1, int p2, bool p3, double p4, float p5, int p6, double p7, char p8, char p9, float p10, object p11, Type p12, T p13, string p14)
    {
    }

    public void SetValue(Slider? value)
    {
        throw new System.NotImplementedException();
    }
    public void Bind() { }

    public void Bind(IBindableMemberEventAdder bindableMemberEventAdder)
    {
        throw new NotImplementedException();
    }

    public void Unbind() { }
}