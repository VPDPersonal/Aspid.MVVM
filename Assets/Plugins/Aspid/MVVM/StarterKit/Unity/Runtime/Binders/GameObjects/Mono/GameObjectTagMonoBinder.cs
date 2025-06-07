using UnityEngine;
using Aspid.MVVM.Unity;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<string, string>;
#else
using Converter = Aspid.MVVM.StarterKit.Unity.IConverterString;
#endif

namespace Aspid.MVVM.StarterKit.Unity
{
    [AddComponentMenu("Aspid/MVVM/Binders/GameObject/GameObject Binder - Tag")]
    [AddComponentContextMenu(typeof(Component),"Add GameObject Binder/GameObject Binder - Tag")]
    public partial class GameObjectTagMonoBinder : MonoBinder, IBinder<string>
    {
        [Header("Converter")]
        [SerializeReferenceDropdown]
        [SerializeReference] private Converter _converter;
        
        [BinderLog]
        public void SetValue(string value) =>
            gameObject.tag = _converter?.Convert(value) ?? value;
    }
}