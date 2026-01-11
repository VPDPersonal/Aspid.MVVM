using UnityEngine;
using UnityEngine.Events;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<long, long>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterLong;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddBinderContextMenuByType(typeof(long))]
    [AddComponentMenu("Aspid/MVVM/Binders/UnityEvent/UnityEvent Binder – Long")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/UnityEvent/UnityEvent Binder – Long")]
    public sealed partial class UnityEventLongMonoBinder : MonoBinder, INumberBinder
    {
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;
        
        [SerializeField] private UnityEvent<long> _set;
        
        [BinderLog]
        public void SetValue(int value) =>
            SetValue((long)value);
        
        [BinderLog]
        public void SetValue(long value) =>
            _set?.Invoke(_converter?.Convert(value) ?? value);

        [BinderLog]
        public void SetValue(float value) =>
            SetValue((long)value);

        [BinderLog]
        public void SetValue(double value) =>
            SetValue((long)value);
    }
}