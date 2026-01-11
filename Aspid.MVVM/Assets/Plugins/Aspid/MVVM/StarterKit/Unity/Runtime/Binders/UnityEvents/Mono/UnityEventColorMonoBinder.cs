using UnityEngine;
using UnityEngine.Events;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Color, UnityEngine.Color>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterColor;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenuByType(typeof(Color))]
    [AddComponentMenu("Aspid/MVVM/Binders/UnityEvent/UnityEvent Binder – Color")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/UnityEvent/UnityEvent Binder – Color")]
    public sealed partial class UnityEventColorMonoBinder : MonoBinder, IColorBinder
    {
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;
        
        [SerializeField] private UnityEvent<Color> _set;
        
        [BinderLog]
        public void SetValue(Color value) =>
            _set?.Invoke(_converter?.Convert(value) ?? value);
    }
}