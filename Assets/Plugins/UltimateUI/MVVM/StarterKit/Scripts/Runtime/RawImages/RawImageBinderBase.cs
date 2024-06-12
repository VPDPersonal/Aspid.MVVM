using UnityEngine;
using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.RawImages
{
    public abstract class RawImageBinderBase : MonoBinder
    {
        [Header("Component")]
        [SerializeField] private RawImage _image;
        
        protected RawImage CachedImage => _image ? _image : _image = GetComponent<RawImage>();
    }
}