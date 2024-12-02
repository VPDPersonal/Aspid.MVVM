using UnityEngine;
using Aspid.UI.MVVM.Mono;
using Aspid.UI.MVVM.Mono.Generation;
using Aspid.UI.MVVM.StarterKit.Converters;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono
{
    [AddComponentMenu("UI/Binders/GameObject/GameObject Binder - Tag")]
    public partial class GameObjectTagMonoBinder : MonoBinder, IBinder<string>
    {
        [Header("Converter")]
#if ASPID_UI_SERIALIZE_REFERENCE_DROPDOWN_INTEGRATION
        [SerializeReferenceDropdown]
#endif
#if UNITY_2023_1_OR_NEWER
        [SerializeReference] private IConverter<string, string> _converter;
#else
        [SerializeReference] private IConverterStringToString _converter;
#endif

        [BinderLog]
        public void SetValue(string value) =>
            gameObject.tag = _converter?.Convert(value) ?? value;
    }
}