using UnityEngine;
using UnityEngine.UI;
using UltimateUI.MVVM.Unity;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Bindings.Images
{
    public abstract class ImageBindingBase : MonoBinding
    {
        [Header("Component")]
        [SerializeField] private Image _image;
        
        protected Image CachedImage => _image ? _image : _image = GetComponent<Image>();
    }
}