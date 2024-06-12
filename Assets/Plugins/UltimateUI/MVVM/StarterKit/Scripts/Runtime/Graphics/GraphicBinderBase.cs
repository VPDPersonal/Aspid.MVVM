using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Graphics
{
    public abstract class GraphicBinderBase : MonoBinder
    {
        [Header("Component")]
        [SerializeField] private Graphic _graphic;
        
        protected Graphic CachedGraphic => _graphic ? _graphic : _graphic = GetComponent<Graphic>();
    }
}