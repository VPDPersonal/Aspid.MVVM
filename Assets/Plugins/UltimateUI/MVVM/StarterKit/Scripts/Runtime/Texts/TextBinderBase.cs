using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Texts
{
    public abstract class TextBinderBase : MonoBinder
    {
        [Header("Component")]
        [SerializeField] private TextMeshProUGUI _text;
        
        protected TextMeshProUGUI CachedText => _text ? _text : _text = GetComponent<TextMeshProUGUI>();
    }
}