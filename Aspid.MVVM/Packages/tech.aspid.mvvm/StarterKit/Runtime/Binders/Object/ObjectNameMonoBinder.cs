using UnityEngine;
using Object = UnityEngine.Object;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="MonoBinder{TProperty}">MonoBinder&lt;string&gt;</see> that binds <see cref="Object.name"/> of the target object.
    /// </summary>
    /// <remarks>
    /// A <see langword="null"/> name is written as an empty string, which is what Unity stores for it anyway.
    /// </remarks>
    [AddComponentMenu("Aspid/MVVM/Binders/Object/Object Binder – Name")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/Object/Object Binder – Name")]
    public sealed class ObjectNameMonoBinder : MonoBinder<string>
    {
        [Tooltip("Target object. This GameObject when empty.")]
        [SerializeField] private Object _object;

        /// <summary>
        /// Indicates whether binding is allowed: <see langword="false"/> when the target object is missing.
        /// </summary>
        public override bool CanBind => _object;

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
