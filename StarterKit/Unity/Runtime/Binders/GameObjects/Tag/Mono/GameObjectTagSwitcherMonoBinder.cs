using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="SwitcherMonoBinder{string}"/> that switches the <see cref="GameObject.tag"/> property
    /// of the <see cref="GameObject"/> this component is attached to between two values
    /// based on the bound boolean ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/GameObject/GameObject Binder – Tag Switcher")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/GameObject/Switcher/GameObject Binder – Tag Switcher")]
    public sealed class GameObjectTagSwitcherMonoBinder : SwitcherMonoBinder<string>
    {
        /// <inheritdoc/>
        protected override void SetValue(string value) =>
            gameObject.tag = value;
    }
}