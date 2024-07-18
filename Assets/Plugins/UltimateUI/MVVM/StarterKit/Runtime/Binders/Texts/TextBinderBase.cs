#if ULTIMATE_UI_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;
using UltimateUI.MVVM.Unity;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Texts
{
    public abstract class TextBinderBase : MonoBinder
    {
        [Header("Component")]
        [SerializeField] private TextMeshProUGUI _text;
        
        protected TextMeshProUGUI CachedText => _text ? _text : _text = GetComponent<TextMeshProUGUI>();
    }
}
#endif