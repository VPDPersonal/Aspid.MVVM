using UnityEngine;
#if UNITY_2023_1_OR_NEWER
using Converter = Aspid.MVVM.StarterKit.IConverter<UnityEngine.Vector3, UnityEngine.Vector3>;
#else
using Converter = Aspid.MVVM.StarterKit.IConverterVector3;
#endif

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// MonoBehaviour binder that sets the <see cref="BoxCollider.size"/> property on a group of <see cref="BoxCollider"/>
    /// components, applying the configured selected or default value to each entry based on the bound
    /// enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/Collider/Box/BoxCollider Binder – Size EnumGroup")]
    [AddBinderContextMenu(typeof(BoxCollider), serializePropertyNames: "m_Size", SubPath = "EnumGroup")]
    public sealed class BoxColliderSizeEnumGroupMonoBinder : EnumGroupMonoBinder<BoxCollider, Vector3, Converter>
    {
        protected override void SetValue(BoxCollider element, Vector3 value) =>
            element.size = value;
    }
}
