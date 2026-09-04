using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="MonoBinder{TProperty}"/> that binds the name of the <see cref="GameObject"/> it is attached to.
    /// </summary>
    /// <remarks>
    /// <see langword="null"/> is written as an empty name.
    /// </remarks>
    [AddComponentMenu("Aspid/MVVM/Binders/GameObject/GameObject Binder – Name")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/GameObject/GameObject Binder – Name")]
    public sealed class GameObjectNameMonoBinder : MonoBinder<string>
    {
        /// <inheritdoc/>
        protected override string Property
        {
            get => gameObject.name;
            set => gameObject.name = value ?? string.Empty;
        }
    }
}
