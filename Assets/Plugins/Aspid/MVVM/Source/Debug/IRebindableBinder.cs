#if UNITY_EDITOR || DEBUG
using System.ComponentModel;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Defines a method to rebind view-model bindings, used during debugging or in the Unity editor.
    /// </summary>
    public interface IRebindableBinder
    {
        /// <summary>
        /// Rebinds the current binders. Intended for internal or debug use.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public void Rebind();
    }
}
#endif