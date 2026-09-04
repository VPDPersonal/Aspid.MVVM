using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="MonoBinder"/> that shows the object it is attached to while a binding exists and hides it otherwise.
    /// </summary>
    /// <remarks>
    /// The bound value is ignored; only the presence of a binding matters.
    /// </remarks>
    [BindModeOverride(BindMode.OneTime)]
    [AddComponentMenu("Aspid/MVVM/Binders/GameObject/GameObject Binder – Visible By Bind")]
    [AddBinderContextMenu(typeof(Component), Path = "Add General Binder/GameObject/GameObject Binder – Visible By Bind")]
    public sealed partial class GameObjectVisibleByBindMonoBinder : MonoBinder, IAnyBinder
    {
        [Tooltip("Hide while bound instead of showing.")]
        [SerializeField] private bool _isInvert;

        /// <inheritdoc/>
        protected override BindMode DefaultMode => BindMode.OneTime;

        private void OnEnable() =>
            SetVisible();

        /// <inheritdoc/>
        protected override void OnBound() =>
            SetVisible();

        /// <inheritdoc/>
        protected override void OnUnbound() =>
            SetVisible();

        /// <inheritdoc/>
        [BinderLog]
        public void SetValue<T>(T value) { }

        private void SetVisible() =>
            gameObject.SetActive(_isInvert ? !IsBound : IsBound);
    }
}
