using UnityEngine;
using Aspid.MVVM.StarterKit;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Samples.CustomBinder
{
    /// <summary>
    /// <see cref="ComponentFloatMonoBinder{TComponent}"/> that binds <see cref="HealthBar.Value"/>.
    /// </summary>
    // ComponentFloatMonoBinder gives the binder the whole numeric family (int, long, double)
    // and a converter slot for free; only the property accessor is left to write.
    // GenerateSerializableBinder emits a HealthBarValueBinder twin for use inside [View] classes without a component.
    // AddBinderContextMenu adds "Add Binder" to the HealthBar component's context menu.
    [GenerateSerializableBinder]
    [AddBinderContextMenu(typeof(HealthBar))]
    [AddComponentMenu("Aspid/MVVM/Binders/Samples/Health Bar Binder – Value")]
    public class HealthBarValueMonoBinder : ComponentFloatMonoBinder<HealthBar>
    {
        protected sealed override float Property
        {
            get => CachedComponent.Value;
            set => CachedComponent.Value = this.SafeClamp01(value);
        }
    }
}
