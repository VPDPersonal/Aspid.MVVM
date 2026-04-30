#if UNITY_EDITOR || DEBUG
using System.ComponentModel;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM
{
    /// <summary>
    /// Provides the ability to force a rebind, re-establishing the connection to the current ViewModel.
    /// </summary>
    /// <remarks>
    /// Only available when <c>UNITY_EDITOR</c> or <c>DEBUG</c> is defined.
    /// </remarks>
    public interface IRebindableBinder
    {
        /// <summary>
        /// Rebinds the binder, re-establishing its connection to the current ViewModel.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public void Rebind();
    }
}
#endif