using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherMonoBinder{T}"/> that switches <see cref="GameObject.tag"/> of the object it is attached to.
    /// </summary>
    /// <remarks>
    /// Unity throws when the tag is not declared in Tags and Layers.
    /// </remarks>
    [AddComponentMenu("Aspid/MVVM/Binders/GameObject/GameObject Binder – Tag Switcher")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/GameObject/Switcher/GameObject Binder – Tag Switcher")]
    public sealed class GameObjectTagSwitcherMonoBinder : SwitcherMonoBinder<string>
    {
        /// <inheritdoc/>
        protected override void SetValue(string value) =>
            gameObject.tag = value;
    }
}
