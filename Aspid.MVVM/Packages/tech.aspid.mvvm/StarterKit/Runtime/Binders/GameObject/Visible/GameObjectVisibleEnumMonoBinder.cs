using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{TValue}"/> that sets the active state of the object it is attached to.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/GameObject/GameObject Binder – Visible Enum")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/GameObject/Enum/GameObject Binder – Visible Enum")]
    public sealed class GameObjectVisibleEnumMonoBinder : EnumMonoBinder<bool>
    {
        /// <inheritdoc/>
        protected override void SetValue(bool value) =>
            gameObject.SetActive(value);
    }
}
