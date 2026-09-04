using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TValue}"/> that sets <see cref="GameObject.tag"/> of the object it is attached to.
    /// </summary>
    /// <remarks>
    /// Unity throws when the tag is not declared in Tags and Layers.
    /// </remarks>
    [AddComponentMenu("Aspid/MVVM/Binders/GameObject/GameObject Binder – Tag Enum")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/GameObject/Enum/GameObject Binder – Tag Enum")]
    public sealed class GameObjectTagEnumMonoBinder : EnumMonoBinder<string>
    {
        /// <inheritdoc/>
        protected override void SetValue(string value) =>
            gameObject.tag = value;
    }
}
