using UnityEngine.UI;

// ReSharper disable once CheckNamespace
namespace UltimateUI.MVVM.StarterKit.Binders.Toggles
{
    public abstract class ToggleBinderBase : Binder
    {
        protected readonly Toggle Toggle;

        protected ToggleBinderBase(Toggle toggle)
        {
            Toggle = toggle;
        }
    }
}