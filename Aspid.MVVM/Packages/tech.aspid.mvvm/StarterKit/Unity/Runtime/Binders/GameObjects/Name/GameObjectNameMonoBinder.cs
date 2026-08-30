using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="MonoBinder{TProperty}">MonoBinder&lt;string&gt;</see> that binds the <see cref="UnityEngine.Object.name"/> of the
    /// <see cref="GameObject"/> this component is attached to.
    /// </summary>
    /// <remarks>
    /// A <see langword="null"/> name is refused rather than written: Unity replaces it with an empty string and the
    /// object becomes unfindable by name.
    /// </remarks>
    [AddComponentMenu("Aspid/MVVM/Binders/GameObject/GameObject Binder – Name")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/GameObject/GameObject Binder – Name")]
    public sealed class GameObjectNameMonoBinder : MonoBinder<string>
    {
        /// <inheritdoc/>
        protected override string Property
        {
            get => gameObject.name;
            set
            {
                if (value is null) return;
                gameObject.name = value;
            }
        }
    }
}
