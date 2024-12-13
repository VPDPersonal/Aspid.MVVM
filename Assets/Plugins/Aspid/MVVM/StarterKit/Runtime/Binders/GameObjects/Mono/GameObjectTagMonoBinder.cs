using UnityEngine;
using Aspid.MVVM.Mono;
using Aspid.MVVM.Mono.Generation;
using Aspid.MVVM.StarterKit.Converters;

namespace Aspid.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("Binders/GameObject/GameObject Binder - Tag")]
    public sealed partial class GameObjectTagMonoBinder : MonoBinder, IBinder<string>
    {
        [Header("Converter")]
        [SerializeReference]
        [SerializeReferenceDropdown]
#if UNITY_2023_1_OR_NEWER
         private IConverter<string, string> _converter;
#else
         private IConverterStringToString _converter;
#endif

        [BinderLog]
        public void SetValue(string value) =>
            gameObject.tag = _converter?.Convert(value) ?? value;
    }
}