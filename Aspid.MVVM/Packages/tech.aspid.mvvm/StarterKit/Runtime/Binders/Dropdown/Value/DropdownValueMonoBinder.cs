using TMPro;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.StarterKit
{
    /// <summary>
    /// <see cref="ComponentIntMonoBinder{TComponent}"/> that binds <see cref="TMP_Dropdown.value"/>.
    /// </summary>
    [BindModeOverride(IsAll = true)]
    [AddBinderContextMenu(
        type: typeof(TMP_Dropdown),
        serializePropertyNames: "m_Value")]
    [AddComponentMenu("Aspid/MVVM/Binders/UI/Dropdown/Dropdown Binder – Value")]
    public class DropdownValueMonoBinder : ComponentIntMonoBinder<TMP_Dropdown>
    {
        [Tooltip("When writes from the ViewModel raise onValueChanged; user input always does.")]
        [SerializeField] private NotifyMode _notifyMode = NotifyMode.Never;

        private bool _isSuppressingEcho;

        /// <inheritdoc/>
        protected sealed override int Property
        {
            get => CachedComponent.value;
            set
            {
                if (_notifyMode.ShouldNotify(IsBound))
                {
                    _isSuppressingEcho = true;

                    try
                    {
                        CachedComponent.value = value;
                    }
                    finally
                    {
                        _isSuppressingEcho = false;
                    }
                }
                else
                {
                    CachedComponent.SetValueWithoutNotify(value);
                }

                var applied = CachedComponent.value;
                
                if (applied != value) 
                    RaiseNumberValueChanged(applied);
            }
        }

        /// <inheritdoc/>
        protected override void OnBound()
        {
            base.OnBound();

            if (Mode.IsTwo())
                CachedComponent.onValueChanged.AddListener(OnValueChanged);
        }

        /// <inheritdoc/>
        protected override void OnUnbound()
        {
            base.OnUnbound();

            if (Mode.IsTwo())
                CachedComponent.onValueChanged.RemoveListener(OnValueChanged);
        }

        private void OnValueChanged(int value)
        {
            if (_isSuppressingEcho) return;
            RaiseNumberValueChanged(value);
        }
    }
}
