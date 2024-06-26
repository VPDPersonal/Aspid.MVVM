using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Images
{
    public abstract class ImageBinderBase : MonoBinder
    {
        [Header("Component")]
        [SerializeField] private Image _image;
        
        protected Image CachedImage => _image ? _image : _image = GetComponent<Image>();
    }
}