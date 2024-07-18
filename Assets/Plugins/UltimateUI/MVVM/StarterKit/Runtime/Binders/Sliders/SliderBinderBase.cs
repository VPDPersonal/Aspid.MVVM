using UnityEngine;
using UnityEngine.UI;
using UltimateUI.MVVM.Unity;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Sliders
{
    public abstract class SliderBinderBase : MonoBinder
    {
        [Header("Component")]
        [SerializeField] private Slider _slider;
        
        protected Slider CachedSlider => _slider ? _slider : _slider = GetComponent<Slider>();
    }
}