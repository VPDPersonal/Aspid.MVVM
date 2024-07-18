using UnityEngine;
using UnityEngine.UI;
using UltimateUI.MVVM.Unity;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Bindings.Graphics
{
    public abstract class GraphicBinderBase : MonoBinding
    {
        [Header("Component")]
        [SerializeField] private Graphic _graphic;
        
        protected Graphic CachedGraphic => _graphic ? _graphic : _graphic = GetComponent<Graphic>();
    }
}