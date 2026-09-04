using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="MonoBinder"/> that hands the ViewModel the <see cref="GameObject"/> it is attached to.
    /// </summary>
    [BindModeOverride(BindMode.OneWayToSource)]
    [AddComponentMenu("Aspid/MVVM/Binders/GameObject/GameObject To Source Binder")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/GameObject/GameObject To Source Binder")]
    public sealed class GameObjectToSourceMonoBinder : MonoBinder, IReverseBinder<GameObject>
    {
        /// <inheritdoc/>
        public event Action<GameObject> ValueChanged;

        /// <inheritdoc/>
        protected override BindMode DefaultMode => BindMode.OneWayToSource;

        /// <inheritdoc/>
        protected override void OnBound() =>
            ValueChanged?.Invoke(gameObject);
    }
}
