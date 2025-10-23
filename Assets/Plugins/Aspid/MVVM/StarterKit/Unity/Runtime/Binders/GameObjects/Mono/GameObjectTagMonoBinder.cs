using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<string, string>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterString;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    [AddComponentMenu("Aspid/MVVM/Binders/GameObject/GameObject Binder - Tag")]
    [AddComponentContextMenu(typeof(Component),"Add General Binder/GameObject/GameObject Binder - Tag")]
    public partial class GameObjectTagMonoBinder : MonoBinder, IBinder<string>
    {
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;
        
        [BinderLog]
        public void SetValue(string value) =>
            gameObject.tag = _converter?.Convert(value) ?? value;
    }
}