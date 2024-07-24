#if ULTIMATE_UI_TEXT_MESH_PRO_INTEGRATION
using TMPro;
using UnityEngine;
using UltimateUI.MVVM.Unity;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Texts.Input
{
    public abstract class InputFieldBinderBase : MonoBinder
    {
        [Header("Component")]
        [SerializeField] private TMP_InputField _inputField;
        
        protected TMP_InputField CachedInputField => _inputField ? _inputField : _inputField = GetComponent<TMP_InputField>();
    }
}
#endif