using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="EnumMonoBinder{string}"/> that sets the <see cref="GameObject.tag"/> property
    /// of the attached <see cref="GameObject"/> to a value resolved from the bound enum ViewModel value.
    /// </summary>
    [AddComponentMenu("Aspid/MVVM/Binders/GameObject/GameObject Binder – Tag Enum")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/GameObject/Enum/GameObject Binder – Tag Enum")]
    public sealed class GameObjectTagEnumMonoBinder : EnumMonoBinder<string>
    {
        /// <inheritdoc/>
        protected override void SetValue(string value) =>
            gameObject.tag = value;
    }
}