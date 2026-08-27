using UnityEngine;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="StringMonoBinder"/> that binds the <see cref="Object.name"/> of a target <see cref="Object"/>.
    /// </summary>
    /// <remarks>
    /// A <see langword="null"/> name is written as an empty string, which is what Unity stores for it anyway.
    /// </remarks>
    [AddComponentMenu("Aspid/MVVM/Binders/Object/Object Binder – Name")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/Object/Object Binder – Name")]
    public sealed class ObjectNameMonoBinder : StringMonoBinder
    {
        [Tooltip("The target Object whose name property will be driven by the binding.")]
        [SerializeField] private Object _object;

        /// <inheritdoc/>
        protected override string Property
        {
            get => _object.name;
            set => _object.name = value ?? string.Empty;
        }

        private void OnValidate()
        {
            if (!_object)
                _object = gameObject;
        }
    }
}
