using UnityEngine;
using Aspid.UI.MVVM.Mono.Generation;
using Aspid.UI.MVVM.StarterKit.Converters;

namespace Aspid.UI.MVVM.StarterKit.Binders.Mono.GameObjects
{
    public partial class GameObjectTagMonoBinder : Aspid.UI.MVVM.Mono.MonoBinder, IBinder<string>
    {
        [field: Header("Converter")]
        [field: SerializeField]
#if ASPID_UI_SERIALIZE_REFERENCE_DROPDOWN_INTEGRATION
        [field: SerializeReferenceDropdown]
#endif
        protected IConverterStringToString Converter { get; private set; }

        [BinderLog]
        public void SetValue(string value) =>
            gameObject.tag = Converter?.Convert(value) ?? value;
    }
}