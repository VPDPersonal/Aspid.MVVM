using UnityEngine;
using Aspid.UI.MVVM.Mono.Generation;
using Aspid.UI.MVVM.StarterKit.Converters;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono.GameObjects
{
    public partial class GameObjectTagMonoBinder : Aspid.UI.MVVM.Mono.MonoBinder, IBinder<string>
    {
        [Header("Converter")]
#if ASPID_UI_SERIALIZE_REFERENCE_DROPDOWN_INTEGRATION
        [SerializeReferenceDropdown]
#endif
        [SerializeReference] private IConverterStringToString _converter;

        [BinderLog]
        public void SetValue(string value) =>
            gameObject.tag = _converter?.Convert(value) ?? value;
    }
}