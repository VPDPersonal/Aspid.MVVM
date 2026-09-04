using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumGroupMonoBinder{TElement, TValue}"/> that sets <see cref="GameObject.tag"/> of each element.
    /// </summary>
    /// <remarks>
    /// Unity throws when the tag is not declared in Tags and Layers.
    /// </remarks>
    [AddComponentMenu("Aspid/MVVM/Binders/GameObject/GameObject Binder – Tag EnumGroup")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/GameObject/EnumGroup/GameObject Binder – Tag EnumGroup")]
    public sealed class GameObjectTagEnumGroupMonoBinder : EnumGroupMonoBinder<GameObject, string>
    {
        /// <inheritdoc/>
        protected override void SetValue(GameObject element, string value) =>
            element.tag = value;
    }
}
