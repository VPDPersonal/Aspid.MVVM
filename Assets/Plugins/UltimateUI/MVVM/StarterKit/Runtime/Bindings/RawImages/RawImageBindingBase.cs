using UnityEngine;
using UnityEngine.UI;
using UltimateUI.MVVM.Unity;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Bindings.RawImages
{
    public abstract class RawImageBindingBase : MonoBinding
    {
        [Header("Component")]
        [SerializeField] private RawImage _image;
        
        protected RawImage CachedImage => _image ? _image : _image = GetComponent<RawImage>();
    }
}