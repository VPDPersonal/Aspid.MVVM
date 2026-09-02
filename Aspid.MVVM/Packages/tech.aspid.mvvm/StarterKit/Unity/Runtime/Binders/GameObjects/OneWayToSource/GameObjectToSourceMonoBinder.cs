using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="MonoBinder"/> implementing <see cref="IReverseBinder{T}">IReverseBinder&lt;GameObject&gt;</see> that
    /// hands the ViewModel the <see cref="GameObject"/> this binder is attached to.
    /// </summary>
    [BindModeOverride(modes: BindMode.OneWayToSource)]
    [AddComponentMenu("Aspid/MVVM/Binders/GameObject/GameObject To Source Binder")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/GameObject/GameObject To Source Binder")]
    public sealed partial class GameObjectToSourceMonoBinder : MonoBinder, IReverseBinder<GameObject>
    {
        /// <summary>
        /// Raised with the attached <see cref="GameObject"/> when binding is established.
        /// </summary>
        public event Action<GameObject> ValueChanged;

        /// <inheritdoc/>
        protected override BindMode DefaultMode => BindMode.OneWayToSource;

        /// <summary>
        /// Called after binding is established. Raises <see cref="ValueChanged"/> with the attached
        /// <see cref="GameObject"/>.
        /// </summary>
        protected override void OnBound() =>
            ValueChanged?.Invoke(gameObject);
    }
}
