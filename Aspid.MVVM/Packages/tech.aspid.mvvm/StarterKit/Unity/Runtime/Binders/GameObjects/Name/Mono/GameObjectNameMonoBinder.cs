using System;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="MonoBinder"/> implementing <see cref="IBinder{T}">IBinder&lt;string&gt;</see> and
    /// <see cref="IReverseBinder{T}">IReverseBinder&lt;string&gt;</see> that sets the
    /// <see cref="UnityEngine.Object.name"/> of the object this component is attached to.
    /// </summary>
    /// <remarks>
    /// A <see langword="null"/> name is refused rather than written: Unity replaces it with an empty string and the
    /// object becomes unfindable by name.
    /// <para/>
    /// Supports <see cref="BindMode.OneWayToSource"/>: when binding is established, the current name is sent back to the
    /// ViewModel.
    /// </remarks>
    [BindModeOverride(BindMode.OneWay, BindMode.OneTime, BindMode.OneWayToSource)]
    [AddComponentMenu("Aspid/MVVM/Binders/GameObject/GameObject Binder – Name")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/GameObject/GameObject Binder – Name")]
    public sealed partial class GameObjectNameMonoBinder : MonoBinder, IBinder<string>, IReverseBinder<string>
    {
        /// <inheritdoc/>
        public event Action<string> ValueChanged;

        /// <summary>
        /// Sets the object's name to <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The name received from the ViewModel, or <see langword="null"/> to leave the name alone.</param>
        [BinderLog]
        public void SetValue(string value)
        {
            if (value is null) return;
            gameObject.name = value;
        }

        /// <summary>
        /// Called when the binder is bound. Sends the current name to the ViewModel when using
        /// <see cref="BindMode.OneWayToSource"/>.
        /// </summary>
        protected override void OnBound()
        {
            if (Mode is BindMode.OneWayToSource)
                ValueChanged?.Invoke(gameObject.name);
        }
    }
}
